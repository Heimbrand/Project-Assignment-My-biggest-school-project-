using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HagaDropsIt.Shared.Entities;

public class Order
{

    [BsonId]
    public ObjectId Id { get; set; }
    public string OrderNumber { get; set; }
    public Guid CustomerGuid { get; set; }
    [EmailAddress]
    public string  BuyerEmail { get; set; }
    public List<OrderItem> Products { get; set; }
    public double TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}