using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Castle.Core.Internal;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;

namespace GoedBezigWebApp.Services
{
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public async Task SendEmailAsync(string email, string subject, string messageText)
        {
            await SendEmailAsync(email, subject, messageText, null);
        }

        public async Task SendEmailAsync(List<string> emailList, string subject, string messageText)
        {
            await SendEmailAsync(emailList, subject, messageText, null);
        }

        public async Task SendEmailAsync(string email, string subject, string messageText, string messageHtml)
        {
            var emailList = new List<string> {email};
            await SendEmailAsync(emailList, subject, messageText, messageHtml);
        }

        public async Task SendEmailAsync(List<string> emailList, string subject, string messageText, string messageHtml)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("GiveADay", "giveaday@mdware.org"));
            foreach (var email in emailList)
            {
                emailMessage.To.Add(new MailboxAddress("", email));
            }
            emailMessage.Subject = subject;
            var builder = new BodyBuilder();

            // Plain-text version
            builder.TextBody = messageText;

            // HTML-version if provided
            if (!messageHtml.IsNullOrEmpty()) builder.HtmlBody = messageHtml;

            emailMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                var credentials = new NetworkCredential
                {
                    UserName = "giveaday@mdware.org",
                    Password = "qbsdsl2Y1Kr4"
                };

                client.LocalDomain = "mdware.org";
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.Auto).ConfigureAwait(false);
                await client.AuthenticateAsync(credentials);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
