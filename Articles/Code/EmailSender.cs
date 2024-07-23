using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace ArticleProject.Code
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient smtpClient = new SmtpClient
            {
                Port = 587,
                Host = "localhost\\SQLEXPRESS",
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("articledot8@gmail.com", "vvvvvvvvv")
            };
            return smtpClient.SendMailAsync("articledot8@gmail.com", email, subject, htmlMessage);
        }
    }
}
