using Entities.Models;

namespace EmailService
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
        Task SendEmailAsync(Message message);

        string BuildOrderConfirmationEmail(Order order);
    }
}
