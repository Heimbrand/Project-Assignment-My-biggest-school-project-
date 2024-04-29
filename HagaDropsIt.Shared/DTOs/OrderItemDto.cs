using HagaDropsIt.Shared.Entities;

namespace HagaDropsIt.Shared.DTOs;

public class OrderItemDto
{
    public Product? Product { get; set; }
    public Event? Event { get; set; }
    public int EventQuantity { get; set; }
    public int ProductQuantity { get; set; }
}