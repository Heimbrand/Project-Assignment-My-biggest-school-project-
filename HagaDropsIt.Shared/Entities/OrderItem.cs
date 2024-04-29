using MongoDB.Bson;

namespace HagaDropsIt.Shared.Entities;

public class OrderItem
{
    public ObjectId id { get; set; }

    public Product? Product { get; set; }
    public Event? Event { get; set; }
    public int EventQuantity { get; set; }
    public int ProductQuantity  { get; set; }
}