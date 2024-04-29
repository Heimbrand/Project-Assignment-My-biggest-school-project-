using HagaDropsIt.Client.Data;
using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Services;

public class CartService : ICartService<CartDto, CartItemDto>
{
	private readonly HttpClient _httpClient;

	public CartService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("HagaDropsItAPI");
	}

	public async Task<IEnumerable<CartDto>> GetAllCarts()
	{
		var response = await _httpClient.GetAsync("api/carts/");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<CartDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<CartDto>>();
		return result ?? Enumerable.Empty<CartDto>();
	}

	public async Task<CartDto?> GetCartById(int id)
	{
		var response = await _httpClient.GetAsync($"api/carts/{id}");
		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<CartDto>();
		return result;
	}

	public async Task<CartDto?> GetCartByUserGuid(Guid userGuid)
	{
		var response = await _httpClient.GetAsync($"api/carts/user/{userGuid}");
		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<CartDto>();
		return result;
	}

    public async Task<IEnumerable<CartItemDto>?> GetCartItemsByCartId(int cartId)
    {
        var response = await _httpClient.GetAsync($"api/carts/items/{cartId}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
        return result;
    }

    public async Task<CartDto> CreateCart()
	{
		var response = await _httpClient.PostAsync("api/carts", null);
		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<CartDto>();
		return result;
    }

    public async Task UpdateCart(CartDto cart)
    {
		var response = await _httpClient.PutAsJsonAsync($"api/carts/", cart);
    }

    public async Task AddProductToCart(int cartId, CartItemDto product)
	{
		var response = await _httpClient.PostAsJsonAsync($"api/carts/product/{cartId}", product);
		response.EnsureSuccessStatusCode();
	}

	public async Task RemoveProductFromCart(int cartId, int productId)
	{
		var response = await _httpClient.DeleteAsync($"api/carts/product/{cartId}/{productId}");
		response.EnsureSuccessStatusCode();
	}

    public async Task RemoveCartItemFromCart(int cartId, int cartItemId)
    {
        var response = await _httpClient.DeleteAsync($"api/carts/items/{cartId}/{cartItemId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task AddEventToCart(int cartId, CartItemDto addEvent)
	{
		var response = await _httpClient.PostAsJsonAsync($"api/carts/event/{cartId}", addEvent);
		response.EnsureSuccessStatusCode();
	}

	public async Task RemoveEventFromCart(int cartId, int eventId)
	{
		var response = await _httpClient.DeleteAsync($"api/carts/event/{cartId}/{eventId}");
		response.EnsureSuccessStatusCode();
	}

	public async Task DeleteCart(int cartId)
	{
		var response = await _httpClient.DeleteAsync($"api/carts/{cartId}");
		response.EnsureSuccessStatusCode();
	}
}
