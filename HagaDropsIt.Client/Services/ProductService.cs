using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Services;

public class ProductService : IProductService<ProductDto>
{
	private readonly HttpClient _httpClient;

	public ProductService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("HagaDropsItAPI");
	}

	public async Task<IEnumerable<ProductDto>> GetAllProducts()
	{
		var response = await _httpClient.GetAsync("api/products/");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ProductDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
		return result ?? Enumerable.Empty<ProductDto>();
	}

	public async Task<ProductDto?> GetProductById(int id)
	{
		var response = await _httpClient.GetAsync($"api/products/{id}");
		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<ProductDto>();
		return result;
	}

	public async Task<IEnumerable<ProductDto>> GetProductsByName(string name)
	{
		var response = await _httpClient.GetAsync($"api/products/search/{name}");
		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ProductDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
		return result ?? Enumerable.Empty<ProductDto>();
	}

	public async Task AddProduct(ProductDto newProduct)
	{
		var response = await _httpClient.PostAsJsonAsync("api/products", newProduct);
		response.EnsureSuccessStatusCode();
	}

	public async Task UpdateProduct(ProductDto updatedProduct)
	{
		var response = await _httpClient.PutAsJsonAsync("api/products", updatedProduct);
		response.EnsureSuccessStatusCode();
	}

	public async Task DeleteProduct(int id)
	{
		var response = await _httpClient.DeleteAsync($"api/products/{id}");
		response.EnsureSuccessStatusCode();
	}

    public async Task<IEnumerable<ProductDto>> GetProductsByCategory(int categoryId)
    {
        var response = await _httpClient.GetAsync($"api/products/category/{categoryId}");

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<ProductDto>();
        }
		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        return result;
    }

    public async Task<IEnumerable<ProductDto>> GetProductsByGenre(int genreId)
    {
        var response = await _httpClient.GetAsync($"api/products/genre/{genreId}");
        if (!response.IsSuccessStatusCode)
        {
			return Enumerable.Empty<ProductDto>();
        }
		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
        return result;
    }
}
