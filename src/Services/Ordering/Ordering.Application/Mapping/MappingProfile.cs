using AutoMapper;
using Ordering.Application.Feature.Orders.Command.CheckoutOrder;
using Ordering.Application.Feature.Orders.Command.UpdateOrder;
using Ordering.Application.Feature.Orders.Query.GetOrdersList;
using Ordering.Domain.Entity;

namespace Ordering.Application.Mapping {
  public class MappingProfile : Profile {
    public MappingProfile()
    {
      CreateMap<Order, OrdersVm>().ReverseMap();
      CreateMap<Order, CheckoutOrderCommandRequest>().ReverseMap();
      CreateMap<Order, UpdateOrderCommandRequest>().ReverseMap();
    }
  }
}
