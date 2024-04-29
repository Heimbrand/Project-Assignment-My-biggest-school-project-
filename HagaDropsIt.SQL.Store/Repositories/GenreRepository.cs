using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store.Repositories;

public class GenreRepository(StoreDbContext context) : IGenreService<Genre>
{
	public async Task<IEnumerable<Genre>> GetAllGenres()
	{
		return await context.Genres.ToListAsync();
	}

	public async Task<Genre?> GetGenreById(int id)
	{
		return await context.Genres.FindAsync(id);
	}

	public async Task<IEnumerable<Genre>> GetGenresByName(string name)
	{
		return await context.Genres.Where(g => g.Name.Contains(name)).ToListAsync();
	}

	public async Task AddGenre(Genre newGenre)
	{
		await context.Genres.AddAsync(newGenre);
		await context.SaveChangesAsync();
	}

	public async Task UpdateGenre(Genre updatedGenre)
	{
		var oldGenre = await context.Genres.FindAsync(updatedGenre.Id);
		if (oldGenre == null)
		{
			return;
		}

		oldGenre.Name = updatedGenre.Name;

		await context.SaveChangesAsync();
	}

	public async Task DeleteGenre(int id)
	{
		var genre = await context.Genres.FindAsync(id);
		if (genre == null)
		{
			return;
		}

		context.Genres.Remove(genre);
		await context.SaveChangesAsync();
	}
}
