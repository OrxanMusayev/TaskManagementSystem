using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Emailing
{
    public interface IEmailSender
    {
        Task SendAsync(List<string> to, string subject, string body, bool isBodyHtml = true);
    }
}
