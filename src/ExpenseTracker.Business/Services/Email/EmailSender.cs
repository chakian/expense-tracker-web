using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace ExpenseTracker.Business.Services.Email
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message) => Execute(Options, email, subject, message);

        public Task Execute(AuthMessageSenderOptions options, string email, string subject, string message)
        {
            var mailMessage = new MimeMessage
            {
                Sender = MailboxAddress.Parse(options.MailAccount)
            };
            mailMessage.To.Add(MailboxAddress.Parse(email));
            mailMessage.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = message
            };

            mailMessage.Body = builder.ToMessageBody();

#if DEBUG
            string activationLink = message;
#else
            using var smtp = new SmtpClient();
            smtp.Connect(options.MailServer, options.MailServerPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(options.MailAccount, options.MailPasswrd);
            smtp.Send(mailMessage);
            smtp.Disconnect(true);
#endif
            return Task.CompletedTask;
        }
    }
}
