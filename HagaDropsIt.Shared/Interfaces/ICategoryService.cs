namespace HagaDropsIt.Shared.Interfaces;

public interface ICategoryService<T> where T : class
{
	
    Task<IEnumerable<T>> GetAllCategories();

    Task<T?> GetCategoryById(int id);

    Task<IEnumerable<T>> GetCategoriesByName(string name);

    Task AddCategory(T newCategory);

    Task UpdateCategory(T updatedCategory);

    Task DeleteCategory(int id);
    
}