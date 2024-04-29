using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using MongoDB.Bson;

namespace HagaDropsIt.Client.Services;

public class OrderService : IOrderService<OrderDto>
{
    private readonly HttpClient _httpClient;

    public OrderService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("HagaDropsItAPI");
    }
    public async Task<IEnumerable<OrderDto>> GetAllOrders()
    {
        var response = await _httpClient.GetAsync("api/order");

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<OrderDto>();
        }

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
        return result;
    }
    public async Task<OrderDto?> GetOrderById(ObjectId id)
    {
        var response = await _httpClient.GetAsync($"api/order/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var result = await response.Content.ReadFromJsonAsync<OrderDto>();
        return result;
    }
    public async Task<OrderDto?> GetOrderByOrdernumber(string ordernumber)
    {
        var response = await _httpClient.GetAsync($"api/order/ordernumber/{ordernumber}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var result = await response.Content.ReadFromJsonAsync<OrderDto>();
        return result;
    }
    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerGuid(Guid customerGuid)
    {
        var response = await _httpClient.GetAsync($"api/order/customer/{customerGuid}");
        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<OrderDto>();
        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
        return result;
    }
    public async Task<IEnumerable<OrderDto>> GetOrdersByBuyerEmail(string buyerEmail)
    {
        var response = await _httpClient.GetAsync($"api/order/buyeremail/{buyerEmail}");
        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<OrderDto>();
        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
        return result;
    }
    public async Task AddOrder(OrderDto newOrder)
    {
        var response = await _httpClient.PostAsJsonAsync("api/order", newOrder);
        response.EnsureSuccessStatusCode();
    }
    public async Task DeleteOrder(ObjectId id)
    {
        var response = await _httpClient.DeleteAsync($"api/order/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<OrderDto>> GetOrderBySpecificEvent(string eventName)
    {
        var response = await _httpClient.GetAsync($"api/order/event/{eventName}");
        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<OrderDto>();
        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<Order>>();
        return (IEnumerable<OrderDto>)result;
        
    }

    public Task<IEnumerable<OrderDto>> GetOrdersByEventsByCustomerGuid(Guid customerGuid, string eventName)
    {
        var response = _httpClient.GetAsync($"api/order/event/{customerGuid}/{eventName}");
        if (!response.Result.IsSuccessStatusCode)
        {
            return Task.FromResult(Enumerable.Empty<OrderDto>());
        }
        var result = response.Result.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();
        return result;
    }
}
