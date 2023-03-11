using Microsoft.Extensions.Logging;
using Ordering.Domain.Entity;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed {
  public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger) {
    if (!orderContext.Orders.Any()) {
      // if the db is empty
      orderContext.Orders.AddRange(GetPreconfiguredOrders());
      await orderContext.SaveChangesAsync();
      logger.LogInformation($"DB seeded with context {nameof(OrderContext)}");
    }
  }

  private static IEnumerable<Order> GetPreconfiguredOrders() {
    return new List<Order> {
      new Order() {
        UserName = "swn", FirstName = "John", LastName = "Doe", EmailAddress = "johndoe@email.com",
        Address = "123 Some Address", Country = "United States", TotalPrice = 350
      }
    };
  }
}