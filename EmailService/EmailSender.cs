using Entities.Models;
using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
           
        }


        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);

        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        public string BuildOrderConfirmationEmail(Order order)
        {
            var cartItemsHtml = string.Join("", order.CartItems.Select(item =>
                  $"<li>{item.Name} - Số lượng: {item.Quantity} - Price: {item.Price}</li>"
            ));

            return $@" 
                  <h2>Order Confirmation</h2>
                  <p>Thank you for your purchase! Here are the details of your order:</p>
                  <ul>
                      {cartItemsHtml}
                  </ul>
                  <p>Giá vận chuyển: {order.ShippingCost}</p>
                  <p>Giá Giảm: {order.Discount}</p>
                  <p>Tổng Giá: {order.TotalAmount}</p>

                  <p>Shipping Address: {order.AddressLine}</p>        
            ";
        }

        public string OrderInfomationEmail(Checkout checkout)
        {
            return $@" 
                  <h2>Thông báo</h2>
                  <p>Đơn hàng của bạn đã được phê duyệt và đang trong quá trình vận chuyển!</p>
                       
            ";
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h3 style='color:red;'>{0}</h3>", message.Content) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var item in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        item.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(item.FileName, fileBytes, ContentType.Parse(item.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

    }
}
