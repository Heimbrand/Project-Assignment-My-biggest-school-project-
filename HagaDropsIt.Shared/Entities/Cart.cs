namespace HagaDropsIt.Shared.Entities;

public class Cart
{
    public int Id { get; set; }
    public Guid UserGuid { get; set; }
    public Guid CustomerGuid { get; set; }
    public List<CartItem> CartItems { get; set; } = new ();
}
