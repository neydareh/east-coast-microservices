using MediatR;

namespace Ordering.Application.Feature.Orders.Command.DeleteOrder;

public class DeleteOrderCommandRequest : IRequest {
  public int Id { get; set; }
}