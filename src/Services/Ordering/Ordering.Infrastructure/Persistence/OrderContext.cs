using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entity;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext : DbContext {
  public OrderContext(DbContextOptions<OrderContext> options) : base(options) {
  }

  public DbSet<Order>? Orders { get; set; }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken()) {
    foreach (var entry in ChangeTracker.Entries<EntityBase>()) {
      switch (entry.State) {
        case EntityState.Added:
          entry.Entity.CreatedDate = DateTime.UtcNow;
          entry.Entity.CreatedBy = "swn";
          break;
        case EntityState.Modified:
          entry.Entity.LastModifiedDate = DateTime.UtcNow;
          entry.Entity.LastModifiedBy = "swn";
          break;
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    return base.SaveChangesAsync(cancellationToken);
  }
}