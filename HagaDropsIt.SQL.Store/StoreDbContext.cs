using System.ComponentModel.DataAnnotations.Schema;
using HagaDropsIt.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace HagaDropsIt.SQL.Store;

public class StoreDbContext : DbContext
{

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Event> Events { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Cart> Carts { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;


    public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = 1,
            Name = "Spel"
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = 2,
            Name = "Tangentbord"
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = 3,
            Name = "Datormus"
        });

        modelBuilder.Entity<Category>().HasData(new Category
        {
            Id = 4,
            Name = "Router"
        });

        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            Id = 1,
            Name = "Action"
        });

        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            Id = 2,
            Name = "Äventyr"
        });

        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            Id = 3,
            Name = "RPG"
        });

        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            Id = 4,
            Name = "MMORPG"
        });

        modelBuilder.Entity<Genre>().HasData(new Genre
        {
            Id = 5,
            Name = "Platform"
        });

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                CategoryId = 1,
                GenreId = 4,
                Name = "World Of Warcraft",
                Description =
                    "World of Warcraft är ett massivt onlinespel som utspelar sig i en fantasivärld känd som Azeroth. Spelet, utvecklat av Blizzard Entertainment, bjuder spelare in i en rik och levande värld fylld med äventyr, strider och möjligheter till samarbete.",
                Price = 499,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 1,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2004, 11, 23),
                IsInvisible = false
            },
            new Product
            {
                Id = 2,
                CategoryId = 1,
                GenreId = 3,
                Name = "The Witcher 3",
                Description =
                    "The Witcher 3: Wild Hunt är ett actionrollspel som utspelar sig i en öppen värld. Spelet, utvecklat av CD Projekt Red, är den tredje delen i The Witcher-serien och följer monsterjägaren Geralt av Rivia i hans strävan att hitta sin adoptivdotter Ciri.",
                Price = 399,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2015, 5, 19),
                IsInvisible = false
            },
            new Product
            {
                Id = 3,
                CategoryId = 1,
                GenreId = 1,
                Name = "Doom Eternal",
                Description =
                    "Doom Eternal är ett förstapersonsskjutarspel som utvecklats av id Software och utgivits av Bethesda Softworks. Spelet är den femte huvuddelen i Doom-serien och en uppföljare till Doom från 2016.",
                Price = 599,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 3, 20),
                IsInvisible = false
            },
            new Product
            {
                Id = 4,
                CategoryId = 1,
                GenreId = 2,
                Name = "The Legend of Zelda: Breath of the Wild",
                Description =
                    "The Legend of Zelda: Breath of the Wild är ett actionäventyrsspel som utvecklats och publicerats av Nintendo. Spelet är den artonde huvuddelen i The Legend of Zelda-serien och släpptes till Nintendo Switch och Wii U i mars 2017.",
                Price = 499,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 12,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2017, 3, 3),
                IsInvisible = false
            },
            new Product
            {
                Id = 5,
                CategoryId = 1,
                GenreId = 5,
                Name = "Super Mario Odyssey",
                Description =
                    "Super Mario Odyssey är ett plattformsspel som utvecklats och publicerats av Nintendo. Spelet släpptes till Nintendo Switch i oktober 2017 och är den sextonde huvuddelen i Super Mario-serien.",
                Price = 499,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 7,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2017, 10, 27),
                IsInvisible = false
            },
            new Product
            {
                Id = 6,
                CategoryId = 2,
                GenreId = 1,
                Name = "Razer BlackWidow Elite",
                Description =
                    "Razer BlackWidow Elite är ett mekaniskt tangentbord som är utformat för att ge spelare en överlägsen spelupplevelse. Tangentbordet har Razers mekaniska brytare, Chroma RGB-bakgrundsbelysning och en avtagbar handledsstöd.",
                Price = 1499,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 0,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2018, 9, 1),
                IsInvisible = false
            },
            new Product
            {
                Id = 7,
                CategoryId = 2,
                GenreId = 1,
                Name = "Corsair K95 RGB Platinum",
                Description =
                    "Corsair K95 RGB Platinum är ett mekaniskt tangentbord som är utformat för att ge spelare en överlägsen spelupplevelse. Tangentbordet har Cherry MX-brytare, RGB-bakgrundsbelysning och en avtagbar handledsstöd.",
                Price = 1799,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 0,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2017, 1, 1),
                IsInvisible = false
            },
            new Product
            {
                Id = 8,
                CategoryId = 3,
                GenreId = 1,
                Name = "Razer DeathAdder Elite",
                Description =
                    "Razer DeathAdder Elite är en optisk spel-mus som är utformad för att ge spelare en överlägsen spelupplevelse. Musen har en 16 000 DPI-sensor, Chroma RGB-belysning och sju programmerbara knappar.",
                Price = 699,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 0,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2016, 9, 27),
                IsInvisible = false
            },
            new Product
            {
                Id = 9,
                CategoryId = 3,
                GenreId = 1,
                Name = "Logitech G502 Hero",
                Description =
                    "Logitech G502 Hero är en optisk spel-mus som är utformad för att ge spelare en överlägsen spelupplevelse. Musen har en 16 000 DPI-sensor, RGB-belysning och elva programmerbara knappar.",
                Price = 799,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 0,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2018, 11, 1),
                IsInvisible = false
            },
            new Product
            {
                Id = 10,
                CategoryId = 4,
                GenreId = 1,
                Name = "Asus RT-AX88U",
                Description =
                    "Asus RT-AX88U är en trådlös router som är utformad för att ge spelare en överlägsen spelupplevelse. Routern har stöd för Wi-Fi 6, dubbla band, 8 LAN-portar och en 1,8 GHz quad-core processor.",
                Price = 2999,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 0,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2018, 11, 1),
                IsInvisible = false
            },
            new Product
            {
                Id = 11,
                CategoryId = 4,
                GenreId = 1,
                Name = "Netgear Nighthawk XR500",
                Description =
                    "Netgear Nighthawk XR500 är en trådlös router som är utformad för att ge spelare en överlägsen spelupplevelse. Routern har stöd för dubbla band, 4 LAN-portar och en 1,7 GHz dual-core processor.",
                Price = 1999,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 0,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2018, 11, 1),
                IsInvisible = false
            },
            new Product
            {
                Id = 12,
                CategoryId = 4,
                GenreId = 1,
                Name = "TP-Link Archer C5400X",
                Description =
                    "TP-Link Archer C5400X är en trådlös router som är utformad för att ge spelare en överlägsen spelupplevelse. Routern har stöd för trippel band, 8 LAN-portar och en 1,8 GHz quad-core processor.",
                Price = 2499,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 0,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2018, 11, 1),
                IsInvisible = false
            },
            new Product
            {
                Id = 13,
                CategoryId = 1,
                GenreId = 1,
                Name = "Cyberpunk 2077",
                Description =
                    "Cyberpunk 2077 är ett actionrollspel som utspelar sig i en öppen värld. Spelet, utvecklat av CD Projekt Red, är baserat på Cyberpunk 2020-bordsspelet och utspelar sig i den dystopiska staden Night City.",
                Price = 599,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 12, 10),
                IsInvisible = false
            },
            new Product
            {
                Id = 14,
                CategoryId = 1,
                GenreId = 1,
                Name = "Halo Infinite",
                Description =
                    "Halo Infinite är ett förstapersonsskjutarspel som utvecklats av 343 Industries och publicerats av Xbox Game Studios. Spelet är den sjätte huvuddelen i Halo-serien och en uppföljare till Halo 5: Guardians.",
                Price = 599,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2021, 12, 8),
                IsInvisible = false
            },
            new Product
            {
                Id = 15,
                CategoryId = 1,
                GenreId = 1,
                Name = "Call of Duty: Warzone",
                Description =
                    "Call of Duty: Warzone är ett förstapersonsskjutarspel som utvecklats av Infinity Ward och Raven Software och publicerats av Activision. Spelet är en del av Call of Duty: Modern Warfare och släpptes i mars 2020.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 3, 10),
                IsInvisible = false
            },
            new Product
            {
                Id = 16,
                CategoryId = 1,
                GenreId = 1,
                Name = "Fortnite",
                Description =
                    "Fortnite är ett överlevnadsspel som utvecklats av Epic Games och släpptes som en del av Fortnite: Save the World. Spelet är en del av Fortnite-serien och släpptes i juli 2017.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 12,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2017, 7, 25),
                IsInvisible = false
            },
            new Product
            {
                Id = 17,
                CategoryId = 1,
                GenreId = 1,
                Name = "Apex Legends",
                Description =
                    "Apex Legends är ett förstapersonsskjutarspel som utvecklats av Respawn Entertainment och publicerats av Electronic Arts. Spelet är en del av Titanfall-serien och släpptes i februari 2019.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2019, 2, 4),
                IsInvisible = false
            },
            new Product
            {
                Id = 18,
                CategoryId = 1,
                GenreId = 1,
                Name = "Valorant",
                Description =
                    "Valorant är ett förstapersonsskjutarspel som utvecklats av Riot Games och släpptes i juni 2020. Spelet är en del av Valorant-serien och har en unik karaktär och spellista.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 16,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 6, 2),
                IsInvisible = false
            },
            new Product
            {
                Id = 19,
                CategoryId = 1,
                GenreId = 1,
                Name = "League of Legends",
                Description =
                    "League of Legends är ett multiplayer online battle arena-spel som utvecklats och publicerats av Riot Games. Spelet släpptes i oktober 2009 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 12,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2009, 10, 27),
                IsInvisible = false
            },
            new Product
            {
                Id = 20,
                CategoryId = 1,
                GenreId = 1,
                Name = "Counter-Strike: Global Offensive",
                Description =
                    "Counter-Strike: Global Offensive är ett förstapersonsskjutarspel som utvecklats av Valve Corporation och Hidden Path Entertainment. Spelet är den fjärde huvuddelen i Counter-Strike-serien och släpptes i augusti 2012.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 16,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2012, 8, 21),
                IsInvisible = false
            },
            new Product
            {
                Id = 21,
                CategoryId = 1,
                GenreId = 1,
                Name = "Minecraft",
                Description =
                    "Minecraft är ett sandbox-spel som utvecklats och publicerats av Mojang Studios. Spelet släpptes i november 2011 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 7,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2011, 11, 18),
                IsInvisible = false
            },
            new Product
            {
                Id = 22,
                CategoryId = 1,
                GenreId = 1,
                Name = "Among Us",
                Description =
                    "Among Us är ett onlinespel som utvecklats och publicerats av InnerSloth. Spelet släpptes i juni 2018 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 7,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2018, 6, 15),
                IsInvisible = false
            },
            new Product
            {
                Id = 23,
                CategoryId = 1,
                GenreId = 1,
                Name = "Genshin Impact",
                Description =
                    "Genshin Impact är ett actionrollspel som utvecklats och publicerats av miHoYo. Spelet släpptes i september 2020 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 12,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 9, 28),
                IsInvisible = false
            },
            new Product
            {
                Id = 24,
                CategoryId = 1,
                GenreId = 1,
                Name = "Overwatch",
                Description =
                    "Overwatch är ett förstapersonsskjutarspel som utvecklats och publicerats av Blizzard Entertainment. Spelet släpptes i maj 2016 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 12,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2016, 5, 24),
                IsInvisible = false
            },
            new Product
            {
                Id = 25,
                CategoryId = 1,
                GenreId = 1,
                Name = "Rocket League",
                Description =
                    "Rocket League är ett sportspel som utvecklats och publicerats av Psyonix. Spelet släpptes i juli 2015 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 3,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2015, 7, 7),
                IsInvisible = false
            },
            new Product
            {
                Id = 26,
                CategoryId = 1,
                GenreId = 1,
                Name = "FIFA 21",
                Description =
                    "FIFA 21 är ett sportspel som utvecklats och publicerats av Electronic Arts. Spelet släpptes i oktober 2020 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 3,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 10, 9),
                IsInvisible = false
            },
            new Product
            {
                Id = 27,
                CategoryId = 1,
                GenreId = 1,
                Name = "NBA 2K21",
                Description =
                    "NBA 2K21 är ett sportspel som utvecklats och publicerats av 2K Sports. Spelet släpptes i september 2020 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 3,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 9, 4),
                IsInvisible = false
            },
            new Product
            {
                Id = 28,
                CategoryId = 1,
                GenreId = 1,
                Name = "Madden NFL 21",
                Description =
                    "Madden NFL 21 är ett sportspel som utvecklats och publicerats av Electronic Arts. Spelet släpptes i augusti 2020 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 3,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2020, 8, 28),
                IsInvisible = false
            },
            new Product
            {
                Id = 29,
                CategoryId = 1,
                GenreId = 1,
                Name = "Grand Theft Auto V",
                Description =
                    "Grand Theft Auto V är ett actionäventyrsspel som utvecklats och publicerats av Rockstar Games. Spelet släpptes i september 2013 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2013, 9, 17),
                IsInvisible = false
            },
            new Product
            {
                Id = 30,
                CategoryId = 1,
                GenreId = 1,
                Name = "Red Dead Redemption 2",
                Description =
                    "Red Dead Redemption 2 är ett actionäventyrsspel som utvecklats och publicerats av Rockstar Games. Spelet släpptes i oktober 2018 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2018, 10, 26),
                IsInvisible = false
            },
            new Product
            {
                Id = 31,
                CategoryId = 1,
                GenreId = 1,
                Name = "The Elder Scrolls V: Skyrim",
                Description =
                    "The Elder Scrolls V: Skyrim är ett actionrollspel som utvecklats och publicerats av Bethesda Game Studios. Spelet släpptes i november 2011 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 18,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(2011, 11, 11),
                IsInvisible = false
            },
            new Product
            {
                Id = 32,
                CategoryId = 1,
                GenreId = 1,
                Name = "The Legend of Zelda: Ocarina of Time",
                Description =
                    "The Legend of Zelda: Ocarina of Time är ett actionäventyrsspel som utvecklats och publicerats av Nintendo. Spelet släpptes i november 1998 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 7,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(1998, 11, 21),
                IsInvisible = false
            },
            new Product()
            {
                Id = 33,
                CategoryId = 1,
                GenreId = 1,
                Name = "Super Mario 64",
                Description =
                    "Super Mario 64 är ett plattformsspel som utvecklats och publicerats av Nintendo. Spelet släpptes i juni 1996 och har blivit ett av de mest populära spelen i världen.",
                Price = 0,
                ImageURL = "Assets/Products/Phoenix Stream Console.webp",
                PgRating = 3,
                IsOnSale = false,
                IsNew = false,
                Discount = 0.1,
                Stock = 100,
                RealeaseDate = new DateTime(1996, 6, 23),
                IsInvisible = false
            });



            modelBuilder.Entity<Event>().HasData(new Event
            {
                Id = 1,
                Name = "Lanseringsfest",
                Description = "Välkommen till Haga It Drops lanseringsfest!",
                StartDate = new DateTime(2024, 6, 1),
                EndDate = new DateTime(2024, 6, 3),
                ImageURL = "Assets/images/Esport.webp",
                Location = "Haga Drops It, Hagavägen 1337",
                Price = 0,
                AgeRestriction = 0

            });

        modelBuilder.Entity<Event>().HasData(new Event
        {
            Id = 2,
            Name = "Mys och spel under hagas bara himmel",
            Description = "Kom och mys med oss under bar himmel, och varför inte passa på och gamea ihop?",
            StartDate = new DateTime(2024, 10, 31),
            EndDate = new DateTime(2024, 11, 1),
            ImageURL = "Assets/images/oldtown.webp",
            Location = "Haga Drops It, Hagavägen 1337",
            Price = 299,
            AgeRestriction = 12
        });

        modelBuilder.Entity<Event>().HasData(new Event
        {
            Id = 3,
            Name = "Julfest!",
            Description = "Välkommen till hagas årliga julbord!",
            StartDate = new DateTime(2024, 6, 20),
            EndDate = new DateTime(2024, 6, 22),
            ImageURL = "Assets/images/extraterrestrial.webp",
            Location = "Haga Drops It, Hagavägen 1337",
            Price = 299,
            AgeRestriction = 12
        });





        base.OnModelCreating(modelBuilder);

    }
}



