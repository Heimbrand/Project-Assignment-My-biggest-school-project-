using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HagaDropsIt.API.Extensions;

public static class CustomerEndpointExtensions
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customers");

        group.MapGet("/", GetAllCustomers);
        group.MapGet("/{customerId:int}", GetCustomerById);
        group.MapGet("/user/{userGuid:guid}", GetCustomerByUserGuid);
        group.MapGet("/email/{email}", GetCustomerByEmail);
        group.MapPost("/", CreateCustomer);
        group.MapPut("/", UpdateCustomer);
        group.MapDelete("/{customerId:int}", DeleteCustomer);

        return app;
    }

    private static async Task<IResult> GetAllCustomers(ICustomerService<Customer> repo)
    {
        var customers = await repo.GetAllCustomers();

        return customers.IsNullOrEmpty() ? Results.NotFound($"No customers exist yet...") : Results.Ok(customers);
    }

    private static async Task<IResult> GetCustomerById(ICustomerService<Customer> repo, int customerId)
    {
        var customer = await repo.GetCustomerById(customerId);

        return customer is null ? Results.NotFound($"No customer exist with id: {customerId}") : Results.Ok(customer);
    }

    private static async Task<IResult> GetCustomerByUserGuid(ICustomerService<Customer> repo, Guid userGuid)
    {
        var customer = await repo.GetCustomerByGuid(userGuid);

        return customer is null ? Results.NotFound($"No customer exist for user with guid: {userGuid}") : Results.Ok(customer);
    }

    private static async Task<IResult> GetCustomerByEmail(ICustomerService<Customer> repo, string email)
    {
        var customer = await repo.GetCustomerByEmail(email);

        return customer is null ? Results.NotFound($"No customer exist with email: {email}") : Results.Ok(customer);
    }

    private static async Task<IResult> CreateCustomer(ICustomerService<Customer> repo, Customer newCustomer)
    {
        await repo.AddCustomer(newCustomer);

        return Results.Created("/api/customers", newCustomer);
    }

    private static async Task<IResult> UpdateCustomer(ICustomerService<Customer> repo, Customer updatedCustomer)
    {
        var result = await repo.UpdateCustomer(updatedCustomer);

        return result ? Results.Ok(updatedCustomer) : Results.NotFound($"No customer exist with id: {updatedCustomer.Id}");
    }

    private static async Task<IResult> DeleteCustomer(ICustomerService<Customer> repo, int customerId)
    {
        await repo.DeleteCustomer(customerId);

        return Results.NoContent();
    }
}