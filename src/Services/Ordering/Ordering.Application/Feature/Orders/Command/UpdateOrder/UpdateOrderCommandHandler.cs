﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contract.Persistence;
using Ordering.Application.Feature.Orders.Command.CheckoutOrder;
using Ordering.Domain.Entity;

namespace Ordering.Application.Feature.Orders.Command.UpdateOrder {
  public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand> {

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ChecoutOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<ChecoutOrderCommandHandler> logger)
    {
      _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
      var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
      if (orderToUpdate == null)
      {
        _logger.LogError("Order doesn't exist in the database!");
      }
      _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
      await _orderRepository.UpdateAsync(orderToUpdate!);
      _logger.LogInformation($"Order {orderToUpdate!.Id} was successfully updated");
    }
  }
}