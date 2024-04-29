using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Services;

public class ReviewService : IReviewService<ReviewDto>
{
	private readonly HttpClient _httpClient;

	public ReviewService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("HagaDropsItAPI");
	}

	public async Task<IEnumerable<ReviewDto>> GetAllReviews()
	{
		var response = await _httpClient.GetAsync("api/reviews/");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ReviewDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ReviewDto>>();
		return result ?? Enumerable.Empty<ReviewDto>();
	}

	public async Task<ReviewDto?> GetReviewById(int id)
	{
		var response = await _httpClient.GetAsync($"api/reviews/{id}");
		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<ReviewDto>();
		return result;
	}

	public async Task<IEnumerable<ReviewDto>> GetReviewsByUserId(string userGuid)
	{
		var response = await _httpClient.GetAsync($"api/reviews/user/{userGuid}");
		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ReviewDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ReviewDto>>();
		return result ?? Enumerable.Empty<ReviewDto>();
	}

	public async Task<IEnumerable<ReviewDto>> GetReviewsByProductId(int productId)
	{
		var response = await _httpClient.GetAsync($"api/reviews/product/{productId}");
		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ReviewDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ReviewDto>>();
		return result ?? Enumerable.Empty<ReviewDto>();
	}

	public async Task AddReview(ReviewDto newReview)
	{
		var response = await _httpClient.PostAsJsonAsync("api/reviews", newReview);
		response.EnsureSuccessStatusCode();
	}

	public async Task UpdateReview(ReviewDto updatedReview)
	{
		var response = await _httpClient.PutAsJsonAsync("api/reviews", updatedReview);
		response.EnsureSuccessStatusCode();
	}

	public async Task DeleteReview(int id)
	{
		var response = await _httpClient.DeleteAsync($"api/reviews/{id}");
		response.EnsureSuccessStatusCode();
	}
}
