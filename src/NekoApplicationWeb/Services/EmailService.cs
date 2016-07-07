using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NekoApplicationWeb.ServiceInterfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

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

        public void SendEmailAsync(string emailAddress, string subject, string message)
        {
            _logger.LogInformation($"Email sent to {emailAddress} with subject '{subject}'");

            String apiKey = MailOptions.SendGridApiKey;
            dynamic sg = new SendGridAPIClient(apiKey);

            Email fromEmail = new Email("neko@neko.is");
            Email toEmail = new Email(emailAddress);
            Content content = new Content("text/html", message);
            Mail mail = new Mail(fromEmail, subject, toEmail, content);

            dynamic response = sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}
