namespace GoodHamburger.Application.DTOs;

public class OrderItemDto
{
    public int MenuItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Type { get; set; } = string.Empty;
}