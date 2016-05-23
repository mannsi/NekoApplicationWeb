using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using NekoApplicationWeb.ServiceInterfaces;

namespace NekoApplicationWeb.Services
{
    public class EmailService:IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private MailOptions MailOptions { get; set; }

        public EmailService(IOptions<MailOptions> optionsAccessor,
            ILogger<EmailService> logger)
        {
            _logger = logger;
            MailOptions = optionsAccessor.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogInformation($"Email sent to {email} with subject '{subject}'");

            // Plug in your email service here to send an email.
            var myMessage = new SendGrid.SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new System.Net.Mail.MailAddress("neko@neko.is", "Neko");
            myMessage.Subject = subject;
            myMessage.Text = message;
            myMessage.Html = message;
            //var credentials = new System.Net.NetworkCredential(
            //    Options.SendGridUser,
            //    Options.SendGridKey);
            // Create a Web transport for sending email.
            var transportWeb = new SendGrid.Web(MailOptions.SendGridApiKey);
            // Send the email.
            return transportWeb.DeliverAsync(myMessage);
        }
    }
}
