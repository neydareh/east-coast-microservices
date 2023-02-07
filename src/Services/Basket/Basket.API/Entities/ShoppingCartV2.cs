using Basket.API.Entities.Interfaces;

namespace Basket.API.Entities
{
  public class ShoppingCartV2 : IShoppingCart
  {
    public string Description { get; set; }
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCartV2(string userName)
    {
      UserName = userName.ToLower();
    }
    public decimal TotalPrice {
      get {
        decimal totalPrice = 0;
        foreach(var item in Items) {
          totalPrice += item.Price * item.Quantity;
        }
        return totalPrice;
      }
    }
  }
}