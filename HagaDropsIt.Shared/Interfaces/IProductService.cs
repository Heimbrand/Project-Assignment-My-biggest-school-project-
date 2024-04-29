namespace HagaDropsIt.Shared.Interfaces;

public interface IProductService<T> where T : class
{
	Task<IEnumerable<T>> GetAllProducts();
	Task<T?> GetProductById(int id);
	Task<IEnumerable<T>> GetProductsByName(string name);
	Task AddProduct(T newProduct);
	Task UpdateProduct(T updatedProduct);
	Task DeleteProduct(int id);
	Task<IEnumerable<T>> GetProductsByCategory(int categoryId);
	Task<IEnumerable<T>> GetProductsByGenre(int genreId);
}
