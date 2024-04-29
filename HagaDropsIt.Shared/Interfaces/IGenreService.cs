namespace HagaDropsIt.Shared.Interfaces;

public interface IGenreService<T> where T : class
{
	Task<IEnumerable<T>> GetAllGenres();
	Task<T?> GetGenreById(int id);
	Task<IEnumerable<T>> GetGenresByName(string name);
	Task AddGenre(T newGenre);
	Task UpdateGenre(T updatedGenre);
	Task DeleteGenre(int id);
}
