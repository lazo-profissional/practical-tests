namespace GoodHamburger.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public List<OrderItemDto> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}