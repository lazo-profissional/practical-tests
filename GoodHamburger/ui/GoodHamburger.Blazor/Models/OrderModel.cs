namespace GoodHamburger.Blazor.Models;

public class OrderModel
{
    public int Id { get; set; }
    public List<OrderItemModel> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}