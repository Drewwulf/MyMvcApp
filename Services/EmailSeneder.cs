using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;

namespace MyMvcApp.Services
{
    public class EmailSender
    {
        public EmailSender()
        {
        }

        public async Task SendEmailAsync(string username, string email, string subject, string text)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("School", "cobtahacademy10@gmail.com"));
            message.To.Add(new MailboxAddress(username, email));

            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = text };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                await client.AuthenticateAsync("cobtahacademy10@gmail.com", "ljzs fzih ekan kene");

                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
