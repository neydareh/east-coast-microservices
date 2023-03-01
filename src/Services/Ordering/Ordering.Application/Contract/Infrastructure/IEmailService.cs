using Ordering.Application.Model;

namespace Ordering.Application.Contract.Infrastructure {
  internal interface IEmailService {
    Task<bool> SendEmail(Email email);
  }
}
