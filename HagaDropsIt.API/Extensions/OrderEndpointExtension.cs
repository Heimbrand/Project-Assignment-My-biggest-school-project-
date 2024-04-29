using HagaDropsIt.Mongo.Orders.Repositories;
using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using MongoDB.Bson;

namespace HagaDropsIt.API.Extensions;

public static class OrderEndpointExtension
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/order");

        group.MapGet("", async (IOrderService<Order> repo) => await GetAllOrders(repo));
        group.MapGet("{id}", async (IOrderService<Order> repo, ObjectId id) => await GetOrderById(repo, id));
        group.MapGet("ordernumber/{ordernumber}", async (IOrderService<Order> repo, string ordernumber) => await GetOrderByOrdernumber(repo, ordernumber));
        group.MapGet("customer/{customerGuid}", async (IOrderService<Order> repo, Guid customerGuid) => await GetOrdersByCustomerGuid(repo, customerGuid));
        group.MapGet("buyeremail/{buyerEmail}", async (IOrderService<Order> repo, string buyerEmail) => await GetOrdersByBuyerEmail(repo, buyerEmail));
        group.MapGet("event/{eventName}", async (IOrderService<Order> repo, string eventName) => await GetOrderBySpecificEvent(repo, eventName));
        group.MapGet("event/{customerGuid}/{eventName}", async (IOrderService<Order> repo, Guid customerGuid, string eventName) => await GetOrdersByEventsByCustomerGuid(repo, customerGuid, eventName));
        group.MapPost("", async (IOrderService<Order> repo, Order order) => await CreateOrder(repo, order));
        group.MapDelete("{id}", async (IOrderService<Order> repo, ObjectId id) => await DeleteOrder(repo, id));
        return app;
    }

    private async static Task<IResult> GetOrdersByEventsByCustomerGuid(IOrderService<Order> repo, Guid customerGuid, string eventName)
    {
        var orders = await repo.GetOrdersByEventsByCustomerGuid(customerGuid, eventName);
        
        return orders == null ? Results.NotFound() : Results.Ok(orders);
    }

    private static async Task<IResult> GetAllOrders(IOrderService<Order> repo)
    {
        var orders = await repo.GetAllOrders();

        return orders == null ? Results.NotFound() : Results.Ok(orders);
    }

   
    private static async Task<IResult> GetOrderById(IOrderService<Order> repo, ObjectId id)
    {
        var order = await repo.GetOrderById(id);

        return order == null ? Results.NotFound() : Results.Ok(order);
    }
    private static async Task<IResult> GetOrderByOrdernumber(IOrderService<Order> repo, string ordernumber)
    {
        var order = await repo.GetOrderByOrdernumber(ordernumber);

        return order == null ? Results.NotFound() : Results.Ok(order);
    }
    private static async Task<IResult> GetOrdersByCustomerGuid(IOrderService<Order> repo, Guid customerGuid)
    {
        var orders = await repo.GetOrdersByCustomerGuid(customerGuid);

        return orders == null ? Results.NotFound() : Results.Ok(orders);
    }
    private static async Task<IResult> GetOrdersByBuyerEmail(IOrderService<Order> repo, string buyerEmail)
    {
        var orders = await repo.GetOrdersByBuyerEmail(buyerEmail);

        return orders == null ? Results.NotFound() : Results.Ok(orders);
    }

    private static async Task<IResult> GetOrderBySpecificEvent(IOrderService<Order> repo, string eventName)
    {
        var orders = await repo.GetOrderBySpecificEvent(eventName);

        return orders == null ? Results.NotFound() : Results.Ok(orders);
    }
    private static async Task<IResult> CreateOrder(IOrderService<Order> repo, Order order)
    {
        await repo.AddOrder(order);

        return Results.Created($"/api/order/{order.Id}", order);
    }
    private static async Task<IResult> DeleteOrder(IOrderService<Order> repo, ObjectId id)
    {
        var order = await repo.GetOrderById(id);

        if (order == null)
        {
            return Results.NotFound();
        }

        await repo.DeleteOrder(id);

        return Results.NoContent();
    }
}
