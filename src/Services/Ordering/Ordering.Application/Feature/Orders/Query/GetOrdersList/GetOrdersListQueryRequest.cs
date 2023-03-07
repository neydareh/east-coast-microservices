using MediatR;

namespace Ordering.Application.Feature.Orders.Query.GetOrdersList {
  public class GetOrdersListQueryRequest : IRequest<List<OrdersVm>> {
    public string? UserName { get; set; }

    public GetOrdersListQueryRequest(string? userName) {
      UserName = userName ?? throw new ArgumentNullException(nameof(userName));
    }
  }
}