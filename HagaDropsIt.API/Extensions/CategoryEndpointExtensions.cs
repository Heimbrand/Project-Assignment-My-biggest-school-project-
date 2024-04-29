using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;

namespace HagaDropsIt.API.Extensions;

public static class CategoryEndpointExtensions
{

    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories");

        group.MapGet("/", GetAllCategories);
        group.MapGet("/{id}", GetCategoryById);
        group.MapGet("/search/{name}", GetCategoryByName);
        group.MapPost("/", AddCategory);
        group.MapDelete("/{id}", DeleteCategory);
        group.MapPut("/", UpdateCategory);

        return app;
    }

    private static async Task<IResult> GetAllCategories(ICategoryService<Category> cRepo)
    {
        var getCategories = await cRepo.GetAllCategories();

        return Results.Ok(getCategories);
    }

    private static async Task<IResult> GetCategoryById(ICategoryService<Category> cRepo, int id)
    {
        var getCategory = await cRepo.GetCategoryById(id);
        if (getCategory == null)
        {
            return Results.NotFound($"No category exists with Id: {id}");
        }

        return Results.Ok(getCategory);
    }

    private static async Task<IResult> GetCategoryByName(ICategoryService<Category> cRepo, string name)
    {
        var getCategory = await cRepo.GetCategoriesByName(name);

        if (getCategory == null)
        {
            return Results.NotFound($"No category exists with the name: {name}");
        }

        return Results.Ok(getCategory);
    }

    private static async Task<IResult> AddCategory(ICategoryService<Category> cRepo, Category newCategory)
    {
        if (newCategory == null)
        {
            return Results.BadRequest();
        }

        var createCategory = new Category
        {
            Name = newCategory.Name,
        };
        await cRepo.AddCategory(createCategory);
        return Results.Ok(createCategory);
        
    }

    private static async Task<IResult> UpdateCategory(ICategoryService<Category> cRepo, Category updatedCategory)
    {

        if (updatedCategory == null)
        {
            return Results.BadRequest();
        }

        await cRepo.UpdateCategory(updatedCategory);

        return Results.Ok(updatedCategory);
    }

    private static async Task<IResult> DeleteCategory(ICategoryService<Category> cRepo, int id)
    {
        var deleteCategory = await cRepo.GetCategoryById(id);

        if (deleteCategory == null)
        {
            return Results.NotFound($"No category exists with Id: {id}");
        }

        await cRepo.DeleteCategory(id);
        return Results.Ok();
    }
}