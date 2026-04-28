using Xunit;
using System.Net;
using System.Net.Http.Json;
using GoodHamburger.Tests.Integration.TestDtos;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GoodHamburger.Tests.Integration;

public class OrdersApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public OrdersApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetMenu_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/menu");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_Valid_ReturnsCreated()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new { menuItemIds = new[] { 1, 4, 5 } });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_Duplicate_ReturnsBadRequest()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new { menuItemIds = new[] { 1, 1 } });
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetOrder_InvalidId_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/orders/99999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task FullOrderLifecycle_ShouldWorkCorrectly()
    {
        var createOrder = new { menuItemIds = new[] { 1, 4, 5 } };
        var createResponse = await _client.PostAsJsonAsync("/api/orders", createOrder);
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var order = await createResponse.Content.ReadFromJsonAsync<OrderTestDto>();
        Assert.NotNull(order);
        Assert.Equal(7.60m, order!.Total);

        var updateOrder = new { menuItemIds = new[] { 3, 5 } };
        var updateResponse = await _client.PutAsJsonAsync($"/api/orders/{order.Id}", updateOrder);
        Assert.True(updateResponse.IsSuccessStatusCode, $"Update failed: {updateResponse.StatusCode}");

        var updatedOrder = await updateResponse.Content.ReadFromJsonAsync<OrderTestDto>();
        Assert.NotNull(updatedOrder);
        Assert.Equal(8.075m, updatedOrder!.Total);

        var deleteResponse = await _client.DeleteAsync($"/api/orders/{updatedOrder.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);

        var getDeletedResponse = await _client.GetAsync($"/api/orders/{updatedOrder.Id}");
        Assert.Equal(HttpStatusCode.NotFound, getDeletedResponse.StatusCode);
    }
}