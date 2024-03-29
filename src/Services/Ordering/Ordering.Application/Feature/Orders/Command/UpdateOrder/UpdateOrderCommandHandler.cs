﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contract.Persistence;
using Ordering.Application.Exception;
using Ordering.Domain.Entity;

namespace Ordering.Application.Feature.Orders.Command.UpdateOrder {
  public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommandRequest> {

    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
    {
      _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(UpdateOrderCommandRequest request, CancellationToken cancellationToken)
    {
      var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
      if (orderToUpdate == null) {
        throw new NotFoundException(nameof(Order), request.Id);
      }
      _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommandRequest), typeof(Order));
      await _orderRepository.UpdateAsync(orderToUpdate!);
      _logger.LogInformation($"Order {orderToUpdate!.Id} was successfully updated");
    }
  }
}
