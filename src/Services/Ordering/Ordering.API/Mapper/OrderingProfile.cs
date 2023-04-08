using AutoMapper;
using EventBus.Message.Event;
using Ordering.Application.Feature.Orders.Command.CheckoutOrder;

namespace Ordering.API.Mapper;

public class OrderingProfile : Profile
{
  public OrderingProfile()
  {
    CreateMap<CheckoutOrderCommandRequest, BasketCheckoutEvent>().ReverseMap();
  }
}