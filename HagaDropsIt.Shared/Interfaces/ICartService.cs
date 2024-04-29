namespace HagaDropsIt.Shared.Interfaces;

public interface ICartService<TCart, TCartItem> where TCart : class where TCartItem : class
{
	Task<IEnumerable<TCart>> GetAllCarts();
	Task<TCart?> GetCartById(int cartId);
	Task<TCart?> GetCartByUserGuid(Guid userGuid);
    Task<IEnumerable<TCartItem>?> GetCartItemsByCartId(int cartId);
    Task<TCart> CreateCart();
	Task UpdateCart(TCart cart);
	Task AddProductToCart(int cartId, TCartItem product);
	Task RemoveProductFromCart(int cartId, int productId);
	Task RemoveCartItemFromCart(int cartId, int cartItemId);
	Task AddEventToCart(int cartId, TCartItem addEvent);
	Task RemoveEventFromCart(int cartId, int eventId);
	Task DeleteCart(int cartId);
}
