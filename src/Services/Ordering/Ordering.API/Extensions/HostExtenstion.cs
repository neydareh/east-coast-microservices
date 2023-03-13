using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ordering.API.Extensions;

public static class HostExtenstion {
  public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder,
    int? retry = 0) where TContext : DbContext {
    if (retry == null) return host;
    var retryForAvailability = retry.Value;

    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<TContext>>();
    var context = services.GetService<TContext>();

    try {
      logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
      InvokeSeeder(seeder, context, services);
      logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
    }
    catch (NpgsqlException e) {
      logger.LogError(e, $"An error occurred while migrating the database used on context {typeof(TContext).Name}");

      if (retryForAvailability < 5) {
        retryForAvailability++;
        Thread.Sleep(2000);
        MigrateDatabase(host, seeder, retryForAvailability);
      }
    }

    return host;
  }

  private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
    IServiceProvider services) where TContext : DbContext {
    context.Database.Migrate();
    seeder(context, services);
  }
}