namespace HagaDropsIt.Shared.Interfaces;

public interface IEventService<T> where T : class
{
	Task<IEnumerable<T>> GetAllEvents();
	Task<T> GetEventById(int id);
	Task<IEnumerable<T>> GetEventsByName(string name);
	Task AddEvent(T newEvent);
	Task<bool> UpdateEvent(T updateEvent);
	Task DeleteEvent(int id);
}
