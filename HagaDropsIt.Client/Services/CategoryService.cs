using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Services;

public class CategoryService : ICategoryService<CategoryDto>
{
    public readonly HttpClient _httpClient;

    public CategoryService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("HagaDropsItAPI");
    }

    public async Task AddCategory(CategoryDto newCategory)
    {
        var response = await _httpClient.PostAsJsonAsync("api/categories", newCategory);
    }

    public async Task DeleteCategory(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/categories/{id}");
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategories()
    {
        var response = await _httpClient.GetAsync("api/categories");
        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<CategoryDto>();
        }

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
        return result ?? Enumerable.Empty<CategoryDto>();
    }

    public async Task<IEnumerable<CategoryDto>> GetCategoriesByName(string name)
    {
        var response = await _httpClient.GetAsync($"api/categories/search/{name}");
        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<CategoryDto>();
        }
        var result = await response.Content.ReadFromJsonAsync<IEnumerable<CategoryDto>>();
        return result;
    }

    public async Task<CategoryDto?> GetCategoryById(int id)
    {
        var response = await _httpClient.GetAsync($"api/categories/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var result = await response.Content.ReadFromJsonAsync<CategoryDto>();
        return result;
    }

    public async Task UpdateCategory(CategoryDto updatedCategory)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/categories/", updatedCategory);
        response.EnsureSuccessStatusCode();
    }
}