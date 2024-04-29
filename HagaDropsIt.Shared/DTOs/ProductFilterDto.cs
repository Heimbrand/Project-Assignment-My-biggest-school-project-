namespace HagaDropsIt.Shared.DTOs;

public class ProductFilterDto
{
	public int? CategoryId { get; set; }
	public List<int>? GenreIds { get; set; }
	public double? MinPrice { get; set; }
	public double? MaxPrice { get; set; }
	public int? AgeRestriction { get; set; }
	public bool? IsOnSale { get; set; }
	public bool? IsNew { get; set; }

	// maybe maybe maybe
}
