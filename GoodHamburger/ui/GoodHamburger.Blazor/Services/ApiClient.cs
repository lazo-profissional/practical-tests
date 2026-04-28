using System.Net.Http.Json;
using GoodHamburger.Blazor.Models;

namespace GoodHamburger.Blazor.Services;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MenuItemModel>> GetMenuAsync()
    {
        var response = await _httpClient.GetAsync("api/menu");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<MenuItemModel>>() ?? new();
    }

    public async Task<List<OrderModel>> GetOrdersAsync()
    {
        var response = await _httpClient.GetAsync("api/orders");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<OrderModel>>() ?? new();
    }

    public async Task<OrderModel?> GetOrderByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/orders/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OrderModel>();
    }

    public async Task<OrderModel?> CreateOrderAsync(CreateOrderModel order)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders", order);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<OrderModel>();
        
        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        throw new InvalidOperationException(error?.Error ?? "Failed to create order.");
    }

    public async Task<OrderModel?> UpdateOrderAsync(int id, CreateOrderModel order)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/orders/{id}", order);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<OrderModel>();
        
        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        throw new InvalidOperationException(error?.Error ?? "Failed to update order.");
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/orders/{id}");
        return response.IsSuccessStatusCode;
    }
}