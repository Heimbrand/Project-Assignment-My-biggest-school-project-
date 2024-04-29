using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HagaDropsIt.Shared.Entities
{ 
    public class ApplicationUser : IdentityUser
    {
        public string? Nickname { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Adress { get; set; }
        public string? City { get; set; }
        public string? Zip { get; set; }
        public Cart? Cart { get; set; } 
        public Product? WishList { get; set; }
    }
}
