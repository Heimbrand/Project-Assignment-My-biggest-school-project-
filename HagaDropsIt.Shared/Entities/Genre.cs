using System.Text.Json.Serialization;

namespace HagaDropsIt.Shared.Entities;

public class Genre
{
    public int Id { get; set; } 
    public string Name { get; set; }
    [JsonIgnore]
    public ICollection<Product> Collection { get; set; }
}
