using MediatR;

namespace Ordering.Application.Feature.Orders.Query.GetOrdersList {
  public class GetOrdersListQuery : IRequest<List<OrdersVm>> {
    public string? UserName { get; set; }

    public GetOrdersListQuery(string? userName)
    {
      UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }
  }
}
