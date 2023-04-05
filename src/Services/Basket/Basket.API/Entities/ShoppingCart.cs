using Basket.API.Entities.Interfaces;

namespace Basket.API.Entities {
  public class ShoppingCart : IShoppingCart
  {
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public ShoppingCart(string userName)
    {
      UserName = userName.ToLower();
    }
    public decimal TotalPrice {
      get
      {
        return Items.Sum(item => item.Price * item.Quantity);
      }
    }
  }
}