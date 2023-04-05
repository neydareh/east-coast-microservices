﻿using Basket.API.Entities;
using Basket.API.GRPC.Services;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers {
  [ApiController]
  [Route("api/v1/[controller]")]
  public class BasketController : ControllerBase {
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;

    public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
      _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
      _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
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
  }
}
