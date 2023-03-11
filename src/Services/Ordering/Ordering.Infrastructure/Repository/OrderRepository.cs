using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contract.Persistence;
using Ordering.Domain.Entity;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repository;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository {
  public OrderRepository(OrderContext dbContext) : base(dbContext) {
  }

  public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName) {
    return await _dbContext.Orders.Where(o => o.UserName == userName).ToListAsync();
  }
}