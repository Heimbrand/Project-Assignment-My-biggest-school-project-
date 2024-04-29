namespace HagaDropsIt.Shared.Entities;

public class CartItem
{
    public int Id { get; set; }
    public Product? Product { get; set; } 
    public Event? Event { get; set; } 
    public int Quantity { get; set; }
}
