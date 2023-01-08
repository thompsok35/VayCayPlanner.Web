using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace VayCayPlanner.Web.Services
{
    public class EmailSender : IEmailSender
    {
        private string smtpServer;
        private int smtpPort;
        private string fromEmailAddress;
        private readonly IConfiguration _configuration;

        public EmailSender(string smtpServer, int smtpPort, 
            string fromEmailAddress, IConfiguration configuration)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.fromEmailAddress = fromEmailAddress;
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage
            {
                From = new MailAddress(fromEmailAddress),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };
            
            message.To.Add(new MailAddress(email));
            NetworkCredential networkCredential = new NetworkCredential(
                _configuration.GetValue<string>("SmtpConfig:Login"),
                _configuration.GetValue<string>("SmtpConfig:Password")
                );
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _configuration.GetValue<string>("SmtpConfig:SMTP_Server"),
                Port = _configuration.GetValue<int>("SmtpConfig:Port"),
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = networkCredential,
            };

            try
            {
                smtpClient.Send(message);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                //log this error
                return Task.CompletedTask;
            }
        }
    }
}
