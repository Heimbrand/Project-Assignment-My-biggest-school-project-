namespace HagaDropsIt.Shared.DTOs;

public class CartDto
{
    public int Id { get; set; }
    public Guid UserGuid { get; set; }
    public Guid CustomerGuid { get; set; }
    public List<CartItemDto> CartItems { get; set; } = new ();
}
