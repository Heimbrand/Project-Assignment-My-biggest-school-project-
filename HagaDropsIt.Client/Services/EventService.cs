using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.Client.Services;

public class EventService : IEventService<EventDto>
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<EventService> _logger;

    public EventService(IHttpClientFactory factory, ILogger<EventService> logger)
    {
        _logger = logger;
    
        _httpClient = factory.CreateClient("HagaDropsItAPI");
    }

    public async Task<IEnumerable<EventDto>> GetAllEvents()
    {
        var response = await _httpClient.GetAsync("api/events");
        _logger.LogInformation("Getting all events", response);

        if (!response.IsSuccessStatusCode)
        {
            return Enumerable.Empty<EventDto>();
        }

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<EventDto>>();
        return result ?? Enumerable.Empty<EventDto>();
    }

    public async Task<EventDto> GetEventById(int id)
    {
        var response = await _httpClient.GetAsync($"api/events/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<EventDto>();
        return result;
    }

    public async Task<IEnumerable<EventDto>> GetEventsByName(string name)
    {
        var response = await _httpClient.GetAsync($"api/events/search/{name}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadFromJsonAsync<IEnumerable<EventDto>>();
        return result ?? Enumerable.Empty<EventDto>();
    }

    public async Task AddEvent(EventDto newEvent)
    {
        var response = await _httpClient.PostAsJsonAsync("api/events", newEvent);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteEvent(int id)
    {
        var respone = await _httpClient.DeleteAsync($"api/events/{id}");
        respone.EnsureSuccessStatusCode();
    }

    public async Task<bool> UpdateEvent(EventDto updateEvent)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/events/{updateEvent.Id}", updateEvent);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return true;
    }
}