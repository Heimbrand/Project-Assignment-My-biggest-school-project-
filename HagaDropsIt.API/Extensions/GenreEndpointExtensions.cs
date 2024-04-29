using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.API.Extensions;

public static class GenreEndpointExtensions
{
	public static IEndpointRouteBuilder MapGenreEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("api/genres");

		group.MapGet("/", GetAllGenres);
		group.MapGet("/{genreId}", GetGenreById);
		group.MapGet("/name/{name}", GetGenresByName);
		group.MapPost("/", CreateGenre);
		group.MapPut("/", UpdateGenre);
		group.MapDelete("/{genreId}", DeleteGenre);

		return app;
	}

	private static async Task<IResult> GetAllGenres(IGenreService<Genre> repo)
	{
		var genres = await repo.GetAllGenres();

		return genres == null ? Results.NotFound() : Results.Ok(genres);
	}

	private static async Task<IResult> GetGenreById(IGenreService<Genre> repo, int genreId)
	{
		var genre = await repo.GetGenreById(genreId);

		return genre == null ? Results.NotFound($"No genres exist with Id: {genreId}") : Results.Ok(genre);
	}

	private static async Task<IResult> GetGenresByName(IGenreService<Genre> repo, string name)
	{
		var genres = await repo.GetGenresByName(name);

		return genres == null ? Results.NotFound($"No genres exist with name: {name}") : Results.Ok(genres);
	}

	private static async Task<IResult> CreateGenre(IGenreService<Genre> repo, Genre newGenre)
	{
		if (newGenre is null)
		{
			return Results.BadRequest("Genre body cannot be null!");
		}

		await repo.AddGenre(newGenre);

		return Results.Created($"/api/genre/{newGenre.Id}", newGenre);
	}

	private static async Task<IResult> UpdateGenre(IGenreService<Genre> repo, Genre updatedGenre)
	{
		if (updatedGenre is null)
		{
			return Results.BadRequest("Genre cannot be null!");
		}

		await repo.UpdateGenre(updatedGenre);

		return Results.Ok(updatedGenre);
	}

	private static async Task<IResult> DeleteGenre(IGenreService<Genre> repo, int genreId)
	{
		var genre = await repo.GetGenreById(genreId);

		if (genre is null)
		{
			return Results.NotFound($"No genres exist with Id: {genreId}");
		}

		await repo.DeleteGenre(genreId);

		return Results.NoContent();
	}
}
