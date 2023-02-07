using Basket.API.Entities;

namespace Basket.API.Repositories {
  public interface IBasketRepository {
    Task<ShoppingCart?> GetBasketAsync(string userName);
    Task<ShoppingCartV2?> GetShoppingCartAsync(string userName);
    Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket);
    Task<ShoppingCartV2?> UpdateShoppingCartAsync(ShoppingCartV2 shoppingCart);
    Task DeleteBasketAsync(string userName);
  }
}
