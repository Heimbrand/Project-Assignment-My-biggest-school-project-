using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store.Repositories;

public class CategoryRepository(StoreDbContext context) : ICategoryService<Category>
{
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryById(int id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task<IEnumerable<Category>> GetCategoriesByName(string name)
    {
        return await context.Categories.Where(n => n.Name.Contains(name)).ToListAsync();
    }

    public async Task AddCategory(Category newCategory)
    {
        await context.Categories.AddAsync(newCategory);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategory(int id)
    {
        var dCategory = await context.Categories.FindAsync(id);
        if (dCategory == null)
        {
            return;
        }

        context.Categories.Remove(dCategory);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCategory(Category updatedCategory)
    {
        var categoryToUpdate = await context.Categories.FindAsync(updatedCategory.Id);
        if (categoryToUpdate == null)
        {
            return;
        }

        categoryToUpdate.Name = updatedCategory.Name;
        await context.SaveChangesAsync();
    }
}


    
