using AutoMapper;
using Discount.GRPC.Entities;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;

namespace Discount.GRPC.Services {
  public class DiscountService : DiscountProtoService.DiscountProtoServiceBase {
    private readonly IDiscountRepository _repository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
      var coupon = await _repository.GetDiscount(request.ProductName);
      if (coupon == null)
      {
        throw new RpcException(
          new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} wasn't found!"));
      }
      _logger.LogInformation("Discount was retrieved for [name: {ProductName}, amount: {Amount}]", coupon.ProductName, coupon.Amount);
      var couponModel = _mapper.Map<CouponModel>(coupon);
      return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
      var coupon = _mapper.Map<Coupon>(request.Coupon);
      await _repository.CreateDiscount(coupon);
      _logger.LogInformation("A discount for {ProductName} was successfully created", coupon.ProductName);

      var couponModel = _mapper.Map<CouponModel>(coupon);
      return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
      var coupon = _mapper.Map<Coupon>(request.Coupon);
      await _repository.UpdateDiscount(coupon);
      _logger.LogInformation("A discount for {ProductName} was successfully updated", coupon.ProductName);

      var couponModel = _mapper.Map<CouponModel>(coupon);
      return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
      var isDeleted = await _repository.DeleteDiscount(request.ProductName);
      var response = new DeleteDiscountResponse
      {
        Success = isDeleted
      };
      return response;
    }
  }
}
