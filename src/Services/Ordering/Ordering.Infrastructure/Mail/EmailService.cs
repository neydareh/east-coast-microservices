using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contract.Infrastructure;
using Ordering.Application.Model;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Ordering.Infrastructure.Mail;

public class EmailService : IEmailService {
  private EmailSettings _emailSettings { get; }
  private ILogger<EmailService> _logger { get; }

  public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger) {
    _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task<bool> SendEmail(Email email) {
    var client = new SendGridClient(_emailSettings.ApiKey);

    var subject = email.Subject;
    var emailTo = new EmailAddress(email.To);
    var emailBody = email.Body;

    var from = new EmailAddress() {
      Email = _emailSettings.FromAddress,
      Name = _emailSettings.FromName
    };

    var sendGridMessage = MailHelper.CreateSingleEmail(from, emailTo, subject, emailBody, emailBody);
    var response = await client.SendEmailAsync(sendGridMessage);

    _logger.LogInformation("Email sent!");

    if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
      return true;

    _logger.LogError("Email sending failed!");
    return false;
  }
}