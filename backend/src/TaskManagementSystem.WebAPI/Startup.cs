using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Security.Claims;
using System.Text;
using TaskManagementSystem.Application;
using TaskManagementSystem.Application.Common.Exceptions;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain;
using TaskManagementSystem.Infrastructure;
using TaskManagementSystem.Web.Helpers;
using TaskManagementSystem.Web.Services;
using TaskManagementSystem.WebAPI.Helpers;
using TaskManagementSystem.WebAPI.Services;

namespace TaskManagementSystem.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogger(Configuration);

            services.AddDomain();

            services.AddApplication();

            services.AddInfrastructure(Configuration);

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();


            services.AddHealthChecks();

            services.AddRouting(option =>
            {
                option.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration.GetValue<string>("JWT:ValidAudience"),
                        ValidIssuer = Configuration.GetValue<string>("JWT:ValidIssuer"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });             

            services.AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(typeof(ValidationException)))
              .AddNewtonsoftJson();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagementSystem.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHealthChecks("/health");

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCookiePolicy();

            app.UseSession();

            app.Use(async (context, next) =>
            {
                string JWTToken = context.Session.GetString("JWTToken");

                if (!string.IsNullOrEmpty(JWTToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWTToken);
                }
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagementSystem.WebAPI v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
