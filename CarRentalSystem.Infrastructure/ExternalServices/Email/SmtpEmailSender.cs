using CarRentalSystem.Application.Interfaces.Notifications;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalSystem.Infrastructure.Notifications.Email
{
    /// <summary>
    /// SMTP-based implementation of IEmailSender.
    /// Uses System.Net.Mail.SmtpClient (recommended for simple use).
    /// </summary>
    public sealed class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public SmtpEmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default)
        {
            var section = _config.GetSection("Email");
            var smtpSection = section.GetSection("Smtp");

            var from = section["From"];
            var displayName = section["DisplayName"];
            var host = smtpSection["Host"];
            var port = int.Parse(smtpSection["Port"] ?? "587");
            var enableSsl = bool.Parse(smtpSection["EnableSsl"] ?? "true");
            var user = smtpSection["User"];
            var password = smtpSection["Password"];

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, password),
                EnableSsl = enableSsl
            };

            using var message = new MailMessage
            {
                From = new MailAddress(from, displayName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(to);

            await client.SendMailAsync(message, ct);
        }
    }
}