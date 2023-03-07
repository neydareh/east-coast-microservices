using AutoMapper;
using MediatR;
using Ordering.Application.Contract.Persistence;

namespace Ordering.Application.Feature.Orders.Query.GetOrdersList {
  public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQueryRequest, List<OrdersVm>> {
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper) {
      _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<OrdersVm>> Handle(GetOrdersListQueryRequest request, CancellationToken cancellationToken) {
      var orderList = await _orderRepository.GetOrdersByUserName(request.UserName!);
      return _mapper.Map<List<OrdersVm>>(orderList);
    }
  }
}