using Npgsql;

namespace Discount.API.Extensions {
  public static class HostExtensions {
    //
    // Summary:
    //     This is an extension method that creates a scoped database connection seeds and allow seed action to retry 
    //
    // Returns:
    //     Host 
    public static IHost MigrateDB<TContext>(this IHost host, int? retry = 0)
    {
      int retryForAvailability = retry!.Value;

      // create scoped service section
      using (var scope = host.Services.CreateScope()) {
        // create a service provider
        var services = scope.ServiceProvider;
        // create configuration instance
        var configuration = services.GetRequiredService<IConfiguration>();
        // create logger instance
        var logger = services.GetRequiredService<ILogger<TContext>>();


        try
        {
          logger.LogInformation("Migrating postgresql database");
          // Open DB connection
          using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
          connection.Open();

          // create DB command objects
          using var command = new NpgsqlCommand
          {
            Connection = connection
          };
          // seed DB commands
          command.CommandText = "DROP TABLE IF EXISTS Coupon";
          command.ExecuteNonQuery();

          command.CommandText = @"CREATE TABLE Coupon (Id SERIAL PRIMARY KEY NOT NULL, ProductName VARCHAR(24) NOT NULL, Description TEXT, Amount INT)";
          command.ExecuteNonQuery();

          command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('IPhone X', 'IPhone Discount', 150)";
          command.ExecuteNonQuery();
          
          command.CommandText = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Samsung', 'Samsung Discount', 100)";
          command.ExecuteNonQuery();

          logger.LogInformation("Migrated completed!");
        } catch (NpgsqlException ex)
        {
          logger.LogError(ex, "An error occurred while migrating to DB!");
          // retry migration
          if(retryForAvailability < 2)
          {
            retryForAvailability++;
            Thread.Sleep(2000);
            MigrateDB<TContext>(host, retryForAvailability);
          }
        }

        return host;
      }
    }
  }
}
