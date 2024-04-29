using HagaDropsIt.Mongo.Orders.Data;
using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Shared.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HagaDropsIt.Mongo.Orders.Repositories;

public class OrderRepository : IOrderService<Order>
{
    private readonly IMongoCollection<Order> _orders;
    public OrderRepository(MongoDbSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);  
        var database = client.GetDatabase(settings.DatabaseName);
        _orders = database.GetCollection<Order>(settings.CollectionName,
            new MongoCollectionSettings { AssignIdOnInsert = true });
        
    }
   
    public async Task<IEnumerable<Order>> GetAllOrders() 
    {
        var filter = Builders<Order>.Filter.Empty;
        return await _orders.Find(filter).ToListAsync();

    }

    public async Task<Order?> GetOrderById(ObjectId id) 
    {
        return await _orders.Find(x => x.Id == id).FirstOrDefaultAsync();
    }
    public async Task<Order?> GetOrderByOrdernumber(string orderNumber) 
    {
        return await _orders.Find(x => x.OrderNumber == orderNumber).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Order>> GetOrdersByCustomerGuid(Guid customerGuid) 
    {
        return await _orders.Find(x => x.CustomerGuid == customerGuid).ToListAsync();
    }
    public async Task<IEnumerable<Order>> GetOrdersByBuyerEmail(string buyerEmail) 
    {
        return await _orders.Find(x => x.BuyerEmail == buyerEmail).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrderBySpecificEvent(string eventName)
    {
        return await _orders.Find(x => x.Products.Any(x => x.Event.Name == eventName)).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByEventsByCustomerGuid(Guid customerGuid, string eventName)
    {
        return await _orders.Find(x => x.CustomerGuid == customerGuid && x.Products.Any(x => x.Event.Name == eventName)).ToListAsync();
    }
    public async Task AddOrder(Order order) 
    {
        await _orders.InsertOneAsync(order);
    }
    public async Task DeleteOrder(ObjectId id) 
    {
        await _orders.DeleteOneAsync(x => x.Id == id);
    }
}
