using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Photosoil.Service.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mailSettings = _configuration.GetSection("MailSettings");
                var smtpServer = mailSettings["SmtpServer"];
                var smtpPort = int.Parse(mailSettings["SmtpPort"]);
                var smtpUsername = mailSettings["SmtpUsername"];
                var smtpPassword = mailSettings["SmtpPassword"];
                var senderEmail = mailSettings["SenderEmail"];
                var senderName = mailSettings["SenderName"];

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail, senderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(new MailAddress(email));

                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                throw new Exception($"Failed to send email: {ex.Message}");
            }
        }
    }
}