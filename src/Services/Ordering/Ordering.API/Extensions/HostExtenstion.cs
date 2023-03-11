using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Ordering.API.Extensions;

public static class HostExtenstion {
  public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder,
    int? retry = 0) where TContext : DbContext {
    var retryForAvailability = retry.Value;

    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<TContext>>();
    var context = services.GetServices<TContext>();

    try {
      logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
      InvokeSeeder<>(seeder, context, services);
      logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
    }
    catch (SqlException e) {
      logger.LogError(e, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");

      if (retryForAvailability >= 5) throw;

      retryForAvailability++;
      Thread.Sleep(2000);
      MigrateDatabase<TContext>(host, seeder, retryForAvailability);

      throw;
    }

    return host;
  }

  private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
    IServiceProvider services) where TContext : DbContext {
    context.Database.Migrate();
    seeder(context, services);
  }
}