﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contract.Infrastructure;
using Ordering.Application.Contract.Persistence;
using Ordering.Application.Model;

namespace Ordering.Application.Feature.Orders.Command.CheckoutOrder {
  public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int> {
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CheckoutOrderCommandHandler> _logger;

    public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
    {
      _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
      var orderEntity = _mapper.Map<Domain.Entity.Order>(request);
      var newOrder = await _orderRepository.AddAsync(orderEntity);

      _logger.LogInformation($"Order {newOrder.Id} was successfully created!");

      await SendMail(newOrder);

      return newOrder.Id;
    }

    private async Task SendMail(Domain.Entity.Order order)
    {
      var email = new Email()
      {
        To = "emmanuelneye@gmail.com",
        Body = $"Order was created",
        Subject = "Your order was successfully created"
      };

      try
      {
        await _emailService.SendEmail(email);
      } catch (System.Exception ex)
      {
        _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
      }
    }
  }
}
