﻿namespace HagaDropsIt.Shared.Entities;

public class Review
{
	public int Id { get; set; }
	public Guid UserGuid { get; set; }
	public int ProductId { get; set; }
	public string? Title { get; set; }
	public string? Content { get; set; }
	public int Rating { get; set; }
	public DateTime DateCreated { get; set; }
}
