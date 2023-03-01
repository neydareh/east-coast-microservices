using Ordering.Domain.Entity;

namespace Ordering.Application.Contract.Persistence {
  public interface IOrderRepository : IAsyncRepository<Order> {
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
  }
}
