using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.API.Extensions;

public static class ProductEndpointExtensions
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("api/products");

		group.MapGet("/", GetAllProducts);
		group.MapGet("/{id:int}", GetProductById);
		group.MapGet("/search/{name}", GetProductsByName);
		group.MapGet("/category/{categoryId:int}", GetProductsByCategory);
		group.MapGet("/genre/{genreId:int}", GetProductsByGenre);
		group.MapPost("/", AddProduct);
		group.MapPut("/", UpdateProduct);
		group.MapDelete("/{id:int}", DeleteProduct);

		return app;
	}

    private static async Task<IResult> GetAllProducts(IProductService<Product> repo)
	{
		var products = await repo.GetAllProducts();

		return Results.Ok(products);
	}

	private static async Task<IResult> GetProductById(IProductService<Product> repo, int id)
	{
		var product = await repo.GetProductById(id);

		if (product is null)
		{
			return Results.NotFound($"No products exist with Id: {id}");
		}

		return Results.Ok(product);
	}

	private static async Task<IResult> GetProductsByName(IProductService<Product> repo, string name)
	{
		var products = await repo.GetProductsByName(name);

		if (!products.Any())
		{
			return Results.NotFound($"No products exist with the name: {name}");
		}

		return Results.Ok(products);
	}

    private static async Task<IResult> GetProductsByCategory(IProductService<Product> repo, int categoryId)
    {
        var products = await repo.GetProductsByCategory(categoryId);

        if (!products.Any())
        {
            return Results.NotFound($"No products exist with the category Id: {categoryId}");
        }

        return Results.Ok(products);
    }

    private static async Task<IResult> GetProductsByGenre(IProductService<Product> repo, int genreId)
    {
        var products = await repo.GetProductsByGenre(genreId);

        if (!products.Any())
        {
            return Results.NotFound($"No products exist with the genre Id: {genreId}");
        }

        return Results.Ok(products);
    }

	private static async Task<IResult> AddProduct(IProductService<Product> repo, Product newProduct)
	{
		if (newProduct is null)
		{
			return Results.BadRequest("Product cannot be null!");
		}

		var createProduct = new Product
		{
			Name = newProduct.Name,
			Description = newProduct.Description,
			Price = newProduct.Price,
			ImageURL = newProduct.ImageURL,
			GenreId = newProduct.GenreId,
			CategoryId = newProduct.CategoryId,
			PgRating = newProduct.PgRating,
			IsOnSale = newProduct.IsOnSale,
			IsNew = newProduct.IsNew,
			Discount = newProduct.Discount,
			Stock = newProduct.Stock,
			RealeaseDate = newProduct.RealeaseDate
		};

		await repo.AddProduct(createProduct);

		return Results.Ok(createProduct);
	}

	private static async Task<IResult> UpdateProduct(IProductService<Product> repo, Product updatedProduct)
	{
		if (updatedProduct is null)
		{
			return Results.BadRequest("Product cannot be null!");
		}

		await repo.UpdateProduct(updatedProduct);

		return Results.Ok(updatedProduct);
	}

	private static async Task<IResult> DeleteProduct(IProductService<Product> repo, int id)
	{
		var product = await repo.GetProductById(id);

		if (product is null)
		{
			return Results.NotFound($"No products exist with Id: {id}");
		}

		await repo.DeleteProduct(id);

		return Results.Ok();
	}
}
