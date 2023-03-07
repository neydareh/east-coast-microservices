using System;
using System.Collections.Generic;
using Basket.API.Entities;
using Basket.API.Repositories;
using NSubstitute;
using Xunit;

namespace Basket.API.Test;

public class BasketServiceTest {
  private readonly IBasketRepository? _mockedBasketRepo;
  private readonly ShoppingCart _mockedShoppingCart;

  public BasketServiceTest() {
    _mockedBasketRepo = Substitute.For<IBasketRepository>();
    _mockedShoppingCart = new("mockedUsername") {
      Items = new List<ShoppingCartItem>() {
        new ShoppingCartItem() {
          Quantity = 1,
          Color = "Blue",
          Price = 150M,
          ProductName = "Sample Product One",
          ProductId = Guid.NewGuid().ToString()
        },
        new ShoppingCartItem() {
          Quantity = 2,
          Color = "Green",
          Price = 250M,
          ProductName = "Sample Product Two",
          ProductId = Guid.NewGuid().ToString()
        }
      }
    };
  }

  [Fact]
  public async void GetBasketAsync_ShouldReturnDeserializedCart_WhenCartExists() {
    _mockedBasketRepo!.GetBasketAsync("mockedUsername").Returns(_mockedShoppingCart);
    var shoppingCart = await _mockedBasketRepo!.GetBasketAsync("mockedUsername");
    Assert.NotNull(shoppingCart);
  }

  [Fact]
  public async void UpdateBasketAsync_ShouldReturnBasket_WhenCartExists() {
    var mockedShoppingCartRequest = new ShoppingCart("mockedUsername") {
      Items = new List<ShoppingCartItem>() {
        new ShoppingCartItem() {
          Quantity = 3,
          Color = "Green",
          Price = 250M,
          ProductName = "Sample Product Two",
          ProductId = Guid.NewGuid().ToString()
        }
      }
    };
    var updatedShoppingCart = new ShoppingCart("mockedUsername") {
      Items = new List<ShoppingCartItem>() {
        new ShoppingCartItem() {
          Quantity = 1,
          Color = "Blue",
          Price = 150M,
          ProductName = "Sample Product One",
          ProductId = Guid.NewGuid().ToString()
        },
        new ShoppingCartItem() {
          Quantity = 2,
          Color = "Green",
          Price = 250M,
          ProductName = "Sample Product Two",
          ProductId = Guid.NewGuid().ToString()
        },
        new ShoppingCartItem() {
          Quantity = 3,
          Color = "Green",
          Price = 250M,
          ProductName = "Sample Product Two",
          ProductId = Guid.NewGuid().ToString()
        }
      }
    };
    _mockedBasketRepo!.UpdateBasketAsync(mockedShoppingCartRequest).Returns(updatedShoppingCart);
    var updated = await _mockedBasketRepo.UpdateBasketAsync(mockedShoppingCartRequest);
    Assert.NotNull(updated);
    Assert.Equal(updatedShoppingCart, updated);
  }
}