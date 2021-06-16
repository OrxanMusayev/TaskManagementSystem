using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common.Exceptions;

namespace TaskManagementSystem.Domain.Emailing
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendAsync(List<string> to, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mailMessage = new() { Subject = subject, Body = body, IsBodyHtml = isBodyHtml };
            mailMessage.From = new(GetSmtpConfiguration<string>(EmailSettingNames.DefaultFromAddress));
            to.ForEach(address =>
            {
                mailMessage.To.Add(address);
            });
            await SendAsync(mailMessage);
        }

        private async Task SendAsync(MailMessage mail)
        {
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mail);
            }
        }

        private SmtpClient BuildClient()
        {
            var host = GetSmtpConfiguration<string>(EmailSettingNames.Host);
            var port = GetSmtpConfiguration<int>(EmailSettingNames.Port);

            var smtpClient = new SmtpClient(host, port);

            try
            {
                if (GetSmtpConfiguration<bool>(EmailSettingNames.UseDefaultCredentials))
                {
                    smtpClient.UseDefaultCredentials = true;
                }
                else
                {
                    var userName = GetSmtpConfiguration<string>(EmailSettingNames.UserName);
                    if (userName != null)
                    {
                        var password = GetSmtpConfiguration<string>(EmailSettingNames.Password);
                        smtpClient.Credentials = new NetworkCredential(userName, password);
                        smtpClient.EnableSsl = GetSmtpConfiguration<bool>(EmailSettingNames.EnableSsl);
                    }
                }

                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        private TType GetSmtpConfiguration<TType>(string configurationName) => (TType)Convert.ChangeType(_configuration.GetSection("Settings:" + configurationName).Value, typeof(TType));

        public Task SendAsync(MailAddressCollection to, string subject, string body, bool isBodyHtml = true)
        {
            throw new NotImplementedException();
        }
    }
}
