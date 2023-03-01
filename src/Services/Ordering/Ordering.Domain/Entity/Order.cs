using Ordering.Domain.Common;

namespace Ordering.Domain.Entity {
  public class Order : EntityBase {
    public string? UserName { get; set; }
    public decimal TotalPrice { get; set; }

    public Billing? Billing { get; set; }
    public Payment? Payment { get; set; }
  }
}
