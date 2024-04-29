using HagaDropsIt.Client.Services;
using HagaDropsIt.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.Client.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {

                    Name = "Admin",
                    NormalizedName = "ADMIN"

                });
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {

                    Name = "User",
                    NormalizedName = "USER"

                });


            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {

                    UserName = "admin.admin@admin.se",
                    NormalizedUserName = "ADMIN.ADMIN@ADMIN.SE",
                    Email = "admin.admin@admin.se",
                    NormalizedEmail = "ADMIN.ADMIN@ADMIN.SE",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "YourPassword123!")


                });







        }



    }
}
