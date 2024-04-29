using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Services;

public class GenreService : IGenreService<GenreDto>
{
	private readonly HttpClient _httpClient;

	public GenreService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("HagaDropsItAPI");
	}

	public async Task<IEnumerable<GenreDto>> GetAllGenres()
	{
		var response = await _httpClient.GetAsync("api/genres/");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<GenreDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<GenreDto>>();
		return result ?? Enumerable.Empty<GenreDto>();
	}

	public async Task<GenreDto?> GetGenreById(int id)
	{
		var response = await _httpClient.GetAsync($"api/genres/{id}");
		if (!response.IsSuccessStatusCode)
		{
			return null;
		}

		var result = await response.Content.ReadFromJsonAsync<GenreDto>();
		return result;
	}

	public async Task<IEnumerable<GenreDto>> GetGenresByName(string name)
	{
		var response = await _httpClient.GetAsync($"api/genres/name/{name}");
		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<GenreDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<GenreDto>>();
		return result ?? Enumerable.Empty<GenreDto>();
	}

	public async Task AddGenre(GenreDto newGenre)
	{
		var response = await _httpClient.PostAsJsonAsync("api/genres/", newGenre);
		response.EnsureSuccessStatusCode();
	}

	public async Task UpdateGenre(GenreDto updatedGenre)
	{
		var response = await _httpClient.PutAsJsonAsync($"api/genres/", updatedGenre);
		response.EnsureSuccessStatusCode();
	}

	public async Task DeleteGenre(int id)
	{
		var response = await _httpClient.DeleteAsync($"api/genres/{id}");
		response.EnsureSuccessStatusCode();
	}
}
