using HagaDropsIt.Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace HagaDropsIt.Shared.DTOs;

public class CustomerDto
{
    public int Id { get; set; }
    public Guid CustomerGuid { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public Cart? Cart { get; set; } = new();
}