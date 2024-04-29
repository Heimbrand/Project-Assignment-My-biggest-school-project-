using FuzzySharp;
using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store.Repositories;

public class ProductRepository(StoreDbContext context) : IProductService<Product>
{
	public async Task<IEnumerable<Product>> GetAllProducts()
	{
		return await context.Products.ToListAsync();
	}

	public async Task<Product?> GetProductById(int id)
	{
		return await context.Products.FindAsync(id);
	}

	public async Task<IEnumerable<Product>> GetProductsByName(string searchName)
	{
		var products = await context.Products.ToListAsync();

		var scoredProducts = products.Select(p => new
		{
			Product = p,
			Score = Fuzz.Ratio(p.Name.ToLower(), searchName.ToLower())
		});

		return scoredProducts
			.Where(p => p.Score >= 50 || p.Product.Name.ToLower().Contains(searchName.ToLower()))
			.Select(p => p.Product);
	}

    public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
    {
        return await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByGenre(int genreId)
    {
        return await context.Products.Where(p => p.GenreId == genreId).ToListAsync();
    }

	public async Task AddProduct(Product newProduct)
	{
		await context.Products.AddAsync(newProduct);
		await context.SaveChangesAsync();
	}

	public async Task UpdateProduct(Product updatedProduct)
	{
		var oldProduct = await context.Products.FindAsync(updatedProduct.Id);
		if (oldProduct == null)
		{
			return;
		}

		oldProduct.Name = updatedProduct.Name;
		oldProduct.Description = updatedProduct.Description;
		oldProduct.Price = updatedProduct.Price;
		oldProduct.ImageURL = updatedProduct.ImageURL;
		oldProduct.GenreId = updatedProduct.GenreId;
		oldProduct.CategoryId = updatedProduct.CategoryId;
		oldProduct.PgRating = updatedProduct.PgRating;
		oldProduct.IsOnSale = updatedProduct.IsOnSale;
		oldProduct.IsNew = updatedProduct.IsNew;
		oldProduct.Discount = updatedProduct.Discount;

		await context.SaveChangesAsync();
	}

	public async Task DeleteProduct(int id)
	{
		var product = await context.Products.FindAsync(id);
		if (product == null)
		{
			return;
		}

		context.Products.Remove(product);
		await context.SaveChangesAsync();
	}
}
