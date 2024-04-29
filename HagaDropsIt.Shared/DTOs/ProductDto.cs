using HagaDropsIt.Shared.Entities;
using System.Text.Json.Serialization;



namespace HagaDropsIt.Shared.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int GenreId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string ImageURL { get; set; }
    public int PgRating { get; set; }
    public bool IsOnSale { get; set; }
    public bool IsNew { get; set; }
    public bool IsInvisible { get; set; }
    public double Discount { get; set; }
    public int Stock { get; set; }
    public DateTime RealeaseDate { get; set; }
    [JsonIgnore]
    public List<Cart> Carts { get; set; }
}