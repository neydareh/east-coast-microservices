using AutoMapper;
using EventBus.Message.Event;
using MassTransit;
using MediatR;
using Ordering.Application.Feature.Orders.Command.CheckoutOrder;

namespace Ordering.API.EventBusConsumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
  private readonly IMapper _mapper;
  private readonly ILogger<BasketCheckoutConsumer> _logger;
  private readonly IMediator _mediator;

  public BasketCheckoutConsumer(IMapper mapper, ILogger<BasketCheckoutConsumer> logger, IMediator mediator)
  {
    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
  }

  public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
  {
    var command = _mapper.Map<CheckoutOrderCommandRequest>(context.Message);
    var result = await _mediator.Send(command);
    _logger.LogInformation("BasketCheckoutEvent consumed successfully. Created Order Id: {NewOrderId}", result);
  }
}