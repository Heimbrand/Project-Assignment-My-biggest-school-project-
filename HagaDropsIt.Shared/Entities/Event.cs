using System.Text.Json.Serialization;

namespace HagaDropsIt.Shared.Entities;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? ImageURL { get; set; }
    public string Location { get; set; }
    public double Price { get; set; }
    public int AgeRestriction { get; set; }
    [JsonIgnore]
    public List<Cart> Carts { get; set; }
}