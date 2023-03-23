using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Feature.Orders.Command.CheckoutOrder;
using Ordering.Application.Feature.Orders.Command.DeleteOrder;
using Ordering.Application.Feature.Orders.Command.UpdateOrder;
using Ordering.Application.Feature.Orders.Query.GetOrdersList;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase {
  private readonly IMediator _mediator;

  public OrderController(IMediator mediator) {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  [HttpGet("{username}", Name = "GetOrder")]
  [ProducesResponseType(typeof(IEnumerable<OrdersVm>), StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<OrdersVm>>> GetOrdersByUsername(string username) {
    var query = new GetOrdersListQueryRequest(username);
    var orders = await _mediator.Send(query);
    return Ok(orders);
  }

  [HttpPost(Name = "CheckoutOrder")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommandRequest command) {
    var result = await _mediator.Send(command);
    return Ok(result);
  }

  [HttpPut(Name = "UpdateOrder")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesDefaultResponseType]
  public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderCommandRequest command) {
    await _mediator.Send(command);
    return NoContent();
  }

  [HttpDelete("{id:int}", Name = "DeleteOrder")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesDefaultResponseType]
  public async Task<ActionResult> DeleteOrder(int id) {
    var command = new DeleteOrderCommandRequest() { Id = id };
    await _mediator.Send(command);
    return NoContent();
  }
}