using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contract.Infrastructure;
using Ordering.Application.Contract.Persistence;
using Ordering.Application.Model;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repository;

namespace Ordering.Infrastructure;

public static class InfrastructureServiceRegistration {
  public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
    IConfiguration configuration) {
    services.AddDbContext<OrderContext>(opt =>
      opt.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));

    services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
    services.AddScoped<IOrderRepository, OrderRepository>();

    // email configuration
    services.Configure<EmailSettings>(cfg => configuration.GetSection("EmailSettings"));
    services.AddTransient<IEmailService, EmailService>();

    return services;
  }
}