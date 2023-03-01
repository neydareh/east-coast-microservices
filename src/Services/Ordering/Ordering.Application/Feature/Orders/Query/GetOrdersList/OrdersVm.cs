using Ordering.Domain.Entity;
namespace Ordering.Application.Feature.Orders.Query.GetOrdersList {
  public class OrdersVm {
    public string? UserName { get; set; }
    public decimal TotalPrice { get; set; }

    public Billing? Billing { get; set; }
    public Payment? Payment { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmailAddress { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
  }
}
