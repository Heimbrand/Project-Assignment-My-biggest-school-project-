using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Services;

public class CustomerService : ICustomerService<CustomerDto>
{
    private readonly HttpClient _httpClient;
    public CustomerService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("HagaDropsItAPI");
    }


    public async Task AddCustomer(CustomerDto newCustomer)
    {
        var response = await _httpClient.PostAsJsonAsync("api/customers", newCustomer);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCustomer(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/customers/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
    {
        var response = await _httpClient.GetAsync("api/customers");
        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<CustomerDto>();
        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CustomerDto>>();
        return result ?? Enumerable.Empty<CustomerDto>();
    }

    public async Task<CustomerDto?> GetCustomerByEmail(string email)
    {
        var response = await _httpClient.GetAsync($"api/customers/search/{email}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
        return result;
    }

    public async Task<CustomerDto?> GetCustomerByGuid(Guid guid)
    {
        var response = await _httpClient.GetAsync($"api/customers/search/{guid}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
        return result;
    }

    public async Task<CustomerDto?> GetCustomerById(int id)
    {
        var response = await _httpClient.GetAsync($"api/customers/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
        return result;
    }

    public async Task<bool> UpdateCustomer(CustomerDto updatedCustomer)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/customers/{updatedCustomer.Id}", updatedCustomer);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }
}
