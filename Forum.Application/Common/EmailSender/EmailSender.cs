using Forum.Application.Common.EmailSender.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Forum.Application.Common.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSenderOptions _options;

        public EmailSender(IOptions<EmailSenderOptions> emailSender)
        {
            _options = emailSender.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(
                _options.SenderName,
                _options.SenderAddress));
            emailMessage.To.Add(MailboxAddress.Parse(email));

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain")
            {
                Text = message
            };
            using var client = new MailKit.Net.Smtp.SmtpClient();
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            var host = _options.SmtpServer;
            var port = _options.Port;
            var username = _options.UserName;
            var password = _options.Password;
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(username, password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}

