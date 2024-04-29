using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace HagaDropsIt.API.Extensions;

public static class CartEndpointExtensions
{
	public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("api/carts");

		group.MapGet("/", GetAllCarts);
		group.MapGet("/{cartId:int}", GetCartById);
		group.MapGet("/user/{userGuid:guid}", GetCartByUserGuid);
        group.MapGet("/items/{cartId:int}", GetCartItemsByCartId);
        group.MapPost("/", CreateCart);
        group.MapPut("/", UpdateCart);
		group.MapPost("/product/{cartId:int}", AddProductToCart);
		group.MapPost("/event/{cartId:int}", AddEventToCart);
		group.MapDelete("/product/{cartId:int}/{productId:int}", RemoveProductFromCart);
		group.MapDelete("/event/{cartId:int}/{eventId:int}", RemoveEventFromCart);
		group.MapDelete("/items/{cartId}/{cartItemId}", RemoveCartItemFromCart);
		group.MapDelete("/{cartId:int}", DeleteCart);

		return app;
	}

    private static async Task<IResult> GetAllCarts(ICartService<Cart, CartItem> repo)
	{
		var carts = await repo.GetAllCarts();

		return carts.IsNullOrEmpty() ? Results.NotFound($"No carts exist yet...") : Results.Ok(carts);
	}

	private static async Task<IResult> GetCartById(ICartService<Cart, CartItem> repo, int cartId)
	{
		var cart = await repo.GetCartById(cartId);

		return cart is null ? Results.NotFound($"No cart exist with id: {cartId}") : Results.Ok(cart);
	}

    private static async Task<IResult> GetCartByUserGuid(ICartService<Cart, CartItem> repo, Guid userGuid)
    {
        var cart = await repo.GetCartByUserGuid(userGuid);

        return cart is null ? Results.NotFound($"No cart exist for user with guid: {userGuid}") : Results.Ok(cart);
    }

    private static async Task<IResult> GetCartItemsByCartId(ICartService<Cart, CartItem> repo, int cartId)
    {
		var cartItems = await repo.GetCartItemsByCartId(cartId);

        return cartItems.IsNullOrEmpty() ? Results.NotFound($"No cart items exist for cart with id: {cartId}") : Results.Ok(cartItems);
    }

    private static async Task<IResult> CreateCart(ICartService<Cart, CartItem> repo)
	{
		var cart = await repo.CreateCart();

		return Results.Created("/api/carts", cart);
    }

    private static async Task<IResult> UpdateCart(ICartService<Cart, CartItem> repo, Cart cart)
    {
		await repo.UpdateCart(cart);
        return Results.Ok($"Cart with id: {cart.Id} has been updated");
    }

    private static async Task<IResult> AddProductToCart(ICartService<Cart, CartItem> repo, int cartId, CartItem product)
	{
		await repo.AddProductToCart(cartId, product);

		return Results.Created($"/api/carts/{cartId}", product);
	}

	private static async Task<IResult> RemoveProductFromCart(ICartService<Cart, CartItem> repo, int cartId, int productId)
	{
		await repo.RemoveProductFromCart(cartId, productId);

		return Results.Ok($"Product with id: {productId} has been removed from cart with id: {cartId}");
	}

	private static async Task<IResult> AddEventToCart(ICartService<Cart, CartItem> repo, int cartId, CartItem addEvent)
	{
		await repo.AddEventToCart(cartId, addEvent);

		return Results.Created($"/api/carts/{cartId}", addEvent);
	}

	private static async Task<IResult> RemoveEventFromCart(ICartService<Cart, CartItem> repo, int cartId, int eventId)
	{
		await repo.RemoveEventFromCart(cartId, eventId);

		return Results.Ok($"Event with id: {eventId} has been removed from cart with id: {cartId}");
    }

    private static async Task<IResult> RemoveCartItemFromCart(ICartService<Cart, CartItem> repo, int cartId, int cartItemId)
    {
        await repo.RemoveCartItemFromCart(cartId, cartItemId);

        return Results.Ok($"CartItem with id: {cartItemId} has been removed from cart with id: {cartId}");
    }

    private static async Task<IResult> DeleteCart(ICartService<Cart, CartItem> repo, int cartId)
	{
		await repo.DeleteCart(cartId);

		return Results.Ok($"Cart with id: {cartId} has been deleted");
	}
}
