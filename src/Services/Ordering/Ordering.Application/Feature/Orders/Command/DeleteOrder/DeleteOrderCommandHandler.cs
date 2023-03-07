using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contract.Persistence;
using Ordering.Application.Exception;
using Ordering.Domain.Entity;

namespace Ordering.Application.Feature.Orders.Command.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommandRequest> {
  private readonly IOrderRepository _orderRepository;
  private readonly IMapper _mapper;
  private readonly ILogger<DeleteOrderCommandHandler> _logger;

  public DeleteOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
    ILogger<DeleteOrderCommandHandler> logger) {
    _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  public async Task Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken) {
    var orderToDelete = await _orderRepository.GetByIdAsync(request.Id);
    if (orderToDelete == null) {
      _logger.LogError("Order doesn't exist in the database!");
      throw new NotFoundException(nameof(Order), request.Id);
    }
    await _orderRepository.DeleteAsync(orderToDelete!);
    _logger.LogInformation($"Order {orderToDelete!.Id} was successfully deleted");
  }
}