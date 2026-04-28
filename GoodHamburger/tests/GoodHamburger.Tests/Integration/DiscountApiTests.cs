using Xunit;
using System.Net;
using System.Net.Http.Json;
using GoodHamburger.Tests.Integration.TestDtos;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GoodHamburger.Tests.Integration;

public class DiscountApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public DiscountApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task CreateOrder_SandwichSideDrink_20Percent()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new { menuItemIds = new[] { 1, 4, 5 } });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var order = await response.Content.ReadFromJsonAsync<OrderTestDto>();
        Assert.NotNull(order);
        Assert.Equal(7.60m, order!.Total);
    }

    [Fact]
    public async Task CreateOrder_SandwichDrink_15Percent()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new { menuItemIds = new[] { 3, 5 } });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var order = await response.Content.ReadFromJsonAsync<OrderTestDto>();
        Assert.NotNull(order);
        Assert.Equal(8.075m, order!.Total);
    }

    [Fact]
    public async Task CreateOrder_SandwichSide_10Percent()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new { menuItemIds = new[] { 2, 4 } });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var order = await response.Content.ReadFromJsonAsync<OrderTestDto>();
        Assert.NotNull(order);
        Assert.Equal(5.85m, order!.Total);
    }

    [Fact]
    public async Task CreateOrder_SandwichOnly_0Percent()
    {
        var response = await _client.PostAsJsonAsync("/api/orders", new { menuItemIds = new[] { 3 } });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var order = await response.Content.ReadFromJsonAsync<OrderTestDto>();
        Assert.NotNull(order);
        Assert.Equal(7.00m, order!.Total);
    }
}