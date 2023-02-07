namespace Basket.API.Entities.Interfaces {
  public interface IShoppingCart {
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; }
    public decimal TotalPrice { get; }
  }
}
