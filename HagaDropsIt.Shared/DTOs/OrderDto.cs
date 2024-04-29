using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using HagaDropsIt.Shared.Entities;

namespace HagaDropsIt.Shared.DTOs;

public class OrderDto
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string OrderNumber { get; set; }
    public Guid CustomerGuid { get; set; }
    [EmailAddress]
    public string BuyerEmail { get; set; }
    public List<OrderItem> Products { get; set; }
    public double TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}