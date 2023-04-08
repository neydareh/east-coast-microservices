using AutoMapper;
using Basket.API.Entities;
using Basket.API.GRPC.Services;
using Basket.API.Repositories;
using EventBus.Message.Event;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
  [ApiController]
  [Route("api/v1/[controller]")]
  public class BasketController : ControllerBase
  {
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IMapper mapper,
      IPublishEndpoint publishEndpoint)
    {
      _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
      _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
      var username = userName.ToLower();
      var basket = await _basketRepository.GetBasketAsync(username);
      return Ok(basket ?? new ShoppingCart(username));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
      foreach (var item in basket.Items.Where(item => item.ProductName != null))
      {
        var coupon = await _discountGrpcService.GetDiscount(item.ProductName!);
        item.Price -= coupon.Amount;
      }

      return Ok(await _basketRepository.UpdateBasketAsync(basket));
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
      await _basketRepository.DeleteBasketAsync(userName.ToLower());
      return Ok();
    }

    [Route("[action]")]
    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
    {
      var basket = await _basketRepository.GetBasketAsync(basketCheckout.UserName!.ToLower());
      if (basket == null) return BadRequest();

      var basketCheckoutEvent = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
      basketCheckoutEvent.TotalPrice = basket.TotalPrice;
      await _publishEndpoint.Publish(basketCheckoutEvent);

      await _basketRepository.DeleteBasketAsync(basket.UserName);
      return Accepted();
    }
  }
}