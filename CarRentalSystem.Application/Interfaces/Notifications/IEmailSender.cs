using System.Threading;
using System.Threading.Tasks;

namespace CarRentalSystem.Application.Interfaces.Notifications
{
    /// <summary>
    /// Contract for sending emails. 
    /// Application layer only depends on this interface.
    /// Infrastructure layer will implement actual email provider (SMTP, SendGrid, etc.).
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="to">Recipient email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="body">Email body (can be plain text or HTML).</param>
        /// <param name="ct">Cancellation token.</param>
        Task SendEmailAsync(string to, string subject, string body, CancellationToken ct = default);
    }
}