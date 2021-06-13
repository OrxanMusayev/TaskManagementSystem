using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Application.Common.Interfaces;
using TaskManagementSystem.Domain.Common.Repositories;
using TaskManagementSystem.Infrastructure.Persistence;
using TaskManagementSystem.Infrastructure.Services;
using TaskManagementSystem.Web.Helpers;
using TaskManagementSystem.Web.Services;

namespace TaskManagementSystem.WebAPI
{
    public class Startup
    {
        private const string DefaultCorsPolicyName = "Default";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogger(Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy(name: DefaultCorsPolicyName,
                    builder => builder.WithOrigins(Configuration["App:CorsOrigins"]
                                                    .Split(",", StringSplitOptions.RemoveEmptyEntries))
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod());
            });
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("Default"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));

            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks();

            services.AddControllers()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining(typeof(ValidationException)))
                    .AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagementSystem.Web", Version = "v1" });
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
