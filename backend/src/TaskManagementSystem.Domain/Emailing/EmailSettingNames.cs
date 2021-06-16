using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Emailing
{
    public static class EmailSettingNames
    {
        public const string DefaultFromAddress = "Mailing.DefaultFromAddress";

        public const string DefaultFromDisplayName = "Mailing.DefaultFromDisplayName";

        public const string Host = "Mailing.Smtp.Host";

        public const string Port = "Mailing.Smtp.Port";

        public const string UserName = "Mailing.Smtp.UserName";

        public const string Password = "Mailing.Smtp.Password";

        public const string Domain = "Mailing.Smtp.Domain";

        public const string EnableSsl = "Mailing.Smtp.EnableSsl";

        public const string UseDefaultCredentials = "Mailing.Smtp.UseDefaultCredentials";

    }
}