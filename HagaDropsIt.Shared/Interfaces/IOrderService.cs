using MongoDB.Bson;

namespace HagaDropsIt.Shared.Interfaces;

public interface IOrderService<T> where T : class
{
	Task<IEnumerable<T>> GetAllOrders();
	Task<T?> GetOrderById(ObjectId id);
	Task<T?> GetOrderByOrdernumber(string ordernumber);
	Task<IEnumerable<T>> GetOrdersByCustomerGuid(Guid customerGuid);
	Task<IEnumerable<T>> GetOrdersByBuyerEmail(string buyerEmail);
	Task AddOrder(T newOrder);
	Task DeleteOrder(ObjectId id);
    Task<IEnumerable<T>> GetOrderBySpecificEvent(string eventName);
	Task<IEnumerable<T>> GetOrdersByEventsByCustomerGuid(Guid customerGuid, string eventName);

}
