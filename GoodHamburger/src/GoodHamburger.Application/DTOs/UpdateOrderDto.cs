namespace GoodHamburger.Application.DTOs;

public class UpdateOrderDto
{
    public List<int> MenuItemIds { get; set; } = new();
}