using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store.Repositories;

public class CartRepository(StoreDbContext context) : ICartService<Cart, CartItem>
{
    public async Task<IEnumerable<Cart>> GetAllCarts()
    {
        return await context.Carts.ToListAsync();
    }

    public async Task<Cart?> GetCartById(int cartId)
    {
        return await context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
    }

    public async Task<Cart?> GetCartByUserGuid(Guid userGuid)
    {
        return await context.Carts
            .Include(p => p.CartItems)
            .FirstOrDefaultAsync(c => c.UserGuid == userGuid || c.CustomerGuid == userGuid);
    }

    public async Task<IEnumerable<CartItem>?> GetCartItemsByCartId(int cartId)
    {
        var cart = await context.Carts
            .Include(ci => ci.CartItems)
            .ThenInclude(p => p.Product)
            .Include(ci => ci.CartItems)
            .ThenInclude(e => e.Event)
            .FirstOrDefaultAsync(c => c.Id == cartId);
        var cartItems = cart?.CartItems;

        return cartItems;
    }

    public async Task<Cart> CreateCart()
    {
        await context.Carts.AddAsync(new Cart());
        await context.SaveChangesAsync();
        return await context.Carts.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
    }

    public Task UpdateCart(Cart cart)
    {
        context.Carts.Update(cart);
        return context.SaveChangesAsync();
    }

    public async Task AddProductToCart(int cartId, CartItem product)
    {
        var cart = await context.Carts.Include(cart => cart.CartItems).FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is null)
        {
            return;
        }

        //cart.Products ??= new List<Product>();
        cart.CartItems ??= new List<CartItem>();

        cart.CartItems.Add(product);
        await context.SaveChangesAsync();
    }

    public async Task RemoveProductFromCart(int cartId, int productId)
    {
        var cart = await context.Carts
            .Include(cart => cart.CartItems)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is null || cart.CartItems is null)
        {
            return;
        }

        var cartItemToRemove = cart.CartItems.Find(p => p.Product.Id == productId);

        if (cartItemToRemove is null)
        {
            return;
        }

        cart.CartItems.Remove(cartItemToRemove);
        await context.SaveChangesAsync();
    }

    public async Task RemoveCartItemFromCart(int cartId, int cartItemId)
    {
        var cart = await context.Carts
            .Include(cart => cart.CartItems)
            .ThenInclude(p => p.Product)
            .Include(cart => cart.CartItems)
            .ThenInclude(e => e.Event)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is null || cart.CartItems is null)
        {
            return;
        }

        var cartItemToRemove = cart.CartItems.Find(ci => ci.Id == cartItemId);

        if (cartItemToRemove is null)
        {
            return;
        }

        cart.CartItems.Remove(cartItemToRemove);
        context.CartItems.Remove(cartItemToRemove);
        await context.SaveChangesAsync();
    }

    public async Task AddEventToCart(int cartId, CartItem addEvent)
    {
        var cart = await context.Carts
            .Include(cart => cart.CartItems)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart == null)
        {
            return;
        }

        //cart.Events ??= new List<Event>();
        cart.CartItems ??= new List<CartItem>();

        cart.CartItems.Add(addEvent);
        await context.SaveChangesAsync();
    }

    public async Task RemoveEventFromCart(int cartId, int eventId)
    {
        var cart = await context.Carts
            .Include(cart => cart.CartItems)
            .ThenInclude(cartItem => cartItem.Event)
            .FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is null || cart.CartItems is null)
        {
            return;
        }

        var cartEvent = cart.CartItems.Find(ci => ci.Event.Id == eventId);

        if (cartEvent is null)
        {
            return;
        }

        cart.CartItems.Remove(cartEvent);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCart(int cartId)
    {
        var cart = await context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);

        if (cart is null)
        {
            return;
        }

        context.Carts.Remove(cart);
        await context.SaveChangesAsync();
    }
}
