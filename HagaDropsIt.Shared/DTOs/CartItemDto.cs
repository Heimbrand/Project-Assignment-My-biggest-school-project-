using HagaDropsIt.Shared.Entities;

namespace HagaDropsIt.Shared.DTOs;

public class CartItemDto
{
    public int Id { get; set; }
    public Product? Product { get; set; } 
    public Event? Event { get; set; } 
    public int Quantity { get; set; }
}
