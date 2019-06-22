using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Browser_RPG_Game.Utils
{
    public static class MailUtils
    {
        public static void SendMail(string to, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage(ConfigurationManager.AppSettings["sender"], to)
            {
                Subject = subject,
                Body = body
            };

            SmtpClient smtpClient = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["smtpHost"],
                Credentials = new NetworkCredential(ConfigurationManager.AppSettings["sender"], ConfigurationManager.AppSettings["password"]),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch { }
        }
    }
}