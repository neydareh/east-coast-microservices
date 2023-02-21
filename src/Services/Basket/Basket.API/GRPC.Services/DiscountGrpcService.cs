using Discount.GRPC.Protos;

namespace Basket.API.GRPC.Services {
  public class DiscountGrpcService {
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
      _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
      // create a grpc discount request
      var discountRequest = new GetDiscountRequest { ProductName = productName };
      // call the grpc service to get discount
      return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
  }
}
