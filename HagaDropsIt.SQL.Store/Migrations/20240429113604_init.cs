using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HagaDropsIt.SQL.Store.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    AgeRestriction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PgRating = table.Column<int>(type: "int", nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    IsInvisible = table.Column<bool>(type: "bit", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    RealeaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Carts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Spel" },
                    { 2, "Tangentbord" },
                    { 3, "Datormus" },
                    { 4, "Router" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "AgeRestriction", "Description", "EndDate", "ImageURL", "Location", "Name", "Price", "StartDate" },
                values: new object[,]
                {
                    { 1, 0, "Välkommen till Haga It Drops lanseringsfest!", new DateTime(2024, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assets/images/Esport.webp", "Haga Drops It, Hagavägen 1337", "Lanseringsfest", 0.0, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 12, "Kom och mys med oss under bar himmel, och varför inte passa på och gamea ihop?", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assets/images/oldtown.webp", "Haga Drops It, Hagavägen 1337", "Mys och spel under hagas bara himmel", 299.0, new DateTime(2024, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 12, "Välkommen till hagas årliga julbord!", new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assets/images/extraterrestrial.webp", "Haga Drops It, Hagavägen 1337", "Julfest!", 299.0, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Äventyr" },
                    { 3, "RPG" },
                    { 4, "MMORPG" },
                    { 5, "Platform" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Discount", "GenreId", "ImageURL", "IsInvisible", "IsNew", "IsOnSale", "Name", "PgRating", "Price", "RealeaseDate", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "World of Warcraft är ett massivt onlinespel som utspelar sig i en fantasivärld känd som Azeroth. Spelet, utvecklat av Blizzard Entertainment, bjuder spelare in i en rik och levande värld fylld med äventyr, strider och möjligheter till samarbete.", 0.10000000000000001, 4, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "World Of Warcraft", 1, 499.0, new DateTime(2004, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 2, 1, "The Witcher 3: Wild Hunt är ett actionrollspel som utspelar sig i en öppen värld. Spelet, utvecklat av CD Projekt Red, är den tredje delen i The Witcher-serien och följer monsterjägaren Geralt av Rivia i hans strävan att hitta sin adoptivdotter Ciri.", 0.10000000000000001, 3, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "The Witcher 3", 18, 399.0, new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 3, 1, "Doom Eternal är ett förstapersonsskjutarspel som utvecklats av id Software och utgivits av Bethesda Softworks. Spelet är den femte huvuddelen i Doom-serien och en uppföljare till Doom från 2016.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Doom Eternal", 18, 599.0, new DateTime(2020, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 4, 1, "The Legend of Zelda: Breath of the Wild är ett actionäventyrsspel som utvecklats och publicerats av Nintendo. Spelet är den artonde huvuddelen i The Legend of Zelda-serien och släpptes till Nintendo Switch och Wii U i mars 2017.", 0.10000000000000001, 2, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "The Legend of Zelda: Breath of the Wild", 12, 499.0, new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 5, 1, "Super Mario Odyssey är ett plattformsspel som utvecklats och publicerats av Nintendo. Spelet släpptes till Nintendo Switch i oktober 2017 och är den sextonde huvuddelen i Super Mario-serien.", 0.10000000000000001, 5, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Super Mario Odyssey", 7, 499.0, new DateTime(2017, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 6, 2, "Razer BlackWidow Elite är ett mekaniskt tangentbord som är utformat för att ge spelare en överlägsen spelupplevelse. Tangentbordet har Razers mekaniska brytare, Chroma RGB-bakgrundsbelysning och en avtagbar handledsstöd.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Razer BlackWidow Elite", 0, 1499.0, new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 7, 2, "Corsair K95 RGB Platinum är ett mekaniskt tangentbord som är utformat för att ge spelare en överlägsen spelupplevelse. Tangentbordet har Cherry MX-brytare, RGB-bakgrundsbelysning och en avtagbar handledsstöd.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Corsair K95 RGB Platinum", 0, 1799.0, new DateTime(2017, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 8, 3, "Razer DeathAdder Elite är en optisk spel-mus som är utformad för att ge spelare en överlägsen spelupplevelse. Musen har en 16 000 DPI-sensor, Chroma RGB-belysning och sju programmerbara knappar.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Razer DeathAdder Elite", 0, 699.0, new DateTime(2016, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 9, 3, "Logitech G502 Hero är en optisk spel-mus som är utformad för att ge spelare en överlägsen spelupplevelse. Musen har en 16 000 DPI-sensor, RGB-belysning och elva programmerbara knappar.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Logitech G502 Hero", 0, 799.0, new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 10, 4, "Asus RT-AX88U är en trådlös router som är utformad för att ge spelare en överlägsen spelupplevelse. Routern har stöd för Wi-Fi 6, dubbla band, 8 LAN-portar och en 1,8 GHz quad-core processor.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Asus RT-AX88U", 0, 2999.0, new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 11, 4, "Netgear Nighthawk XR500 är en trådlös router som är utformad för att ge spelare en överlägsen spelupplevelse. Routern har stöd för dubbla band, 4 LAN-portar och en 1,7 GHz dual-core processor.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Netgear Nighthawk XR500", 0, 1999.0, new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 12, 4, "TP-Link Archer C5400X är en trådlös router som är utformad för att ge spelare en överlägsen spelupplevelse. Routern har stöd för trippel band, 8 LAN-portar och en 1,8 GHz quad-core processor.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "TP-Link Archer C5400X", 0, 2499.0, new DateTime(2018, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 13, 1, "Cyberpunk 2077 är ett actionrollspel som utspelar sig i en öppen värld. Spelet, utvecklat av CD Projekt Red, är baserat på Cyberpunk 2020-bordsspelet och utspelar sig i den dystopiska staden Night City.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Cyberpunk 2077", 18, 599.0, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 14, 1, "Halo Infinite är ett förstapersonsskjutarspel som utvecklats av 343 Industries och publicerats av Xbox Game Studios. Spelet är den sjätte huvuddelen i Halo-serien och en uppföljare till Halo 5: Guardians.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Halo Infinite", 18, 599.0, new DateTime(2021, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 15, 1, "Call of Duty: Warzone är ett förstapersonsskjutarspel som utvecklats av Infinity Ward och Raven Software och publicerats av Activision. Spelet är en del av Call of Duty: Modern Warfare och släpptes i mars 2020.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Call of Duty: Warzone", 18, 0.0, new DateTime(2020, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 16, 1, "Fortnite är ett överlevnadsspel som utvecklats av Epic Games och släpptes som en del av Fortnite: Save the World. Spelet är en del av Fortnite-serien och släpptes i juli 2017.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Fortnite", 12, 0.0, new DateTime(2017, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 17, 1, "Apex Legends är ett förstapersonsskjutarspel som utvecklats av Respawn Entertainment och publicerats av Electronic Arts. Spelet är en del av Titanfall-serien och släpptes i februari 2019.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Apex Legends", 18, 0.0, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 18, 1, "Valorant är ett förstapersonsskjutarspel som utvecklats av Riot Games och släpptes i juni 2020. Spelet är en del av Valorant-serien och har en unik karaktär och spellista.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Valorant", 16, 0.0, new DateTime(2020, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 19, 1, "League of Legends är ett multiplayer online battle arena-spel som utvecklats och publicerats av Riot Games. Spelet släpptes i oktober 2009 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "League of Legends", 12, 0.0, new DateTime(2009, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 20, 1, "Counter-Strike: Global Offensive är ett förstapersonsskjutarspel som utvecklats av Valve Corporation och Hidden Path Entertainment. Spelet är den fjärde huvuddelen i Counter-Strike-serien och släpptes i augusti 2012.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Counter-Strike: Global Offensive", 16, 0.0, new DateTime(2012, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 21, 1, "Minecraft är ett sandbox-spel som utvecklats och publicerats av Mojang Studios. Spelet släpptes i november 2011 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Minecraft", 7, 0.0, new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 22, 1, "Among Us är ett onlinespel som utvecklats och publicerats av InnerSloth. Spelet släpptes i juni 2018 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Among Us", 7, 0.0, new DateTime(2018, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 23, 1, "Genshin Impact är ett actionrollspel som utvecklats och publicerats av miHoYo. Spelet släpptes i september 2020 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Genshin Impact", 12, 0.0, new DateTime(2020, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 24, 1, "Overwatch är ett förstapersonsskjutarspel som utvecklats och publicerats av Blizzard Entertainment. Spelet släpptes i maj 2016 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Overwatch", 12, 0.0, new DateTime(2016, 5, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 25, 1, "Rocket League är ett sportspel som utvecklats och publicerats av Psyonix. Spelet släpptes i juli 2015 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Rocket League", 3, 0.0, new DateTime(2015, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 26, 1, "FIFA 21 är ett sportspel som utvecklats och publicerats av Electronic Arts. Spelet släpptes i oktober 2020 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "FIFA 21", 3, 0.0, new DateTime(2020, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 27, 1, "NBA 2K21 är ett sportspel som utvecklats och publicerats av 2K Sports. Spelet släpptes i september 2020 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "NBA 2K21", 3, 0.0, new DateTime(2020, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 28, 1, "Madden NFL 21 är ett sportspel som utvecklats och publicerats av Electronic Arts. Spelet släpptes i augusti 2020 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Madden NFL 21", 3, 0.0, new DateTime(2020, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 29, 1, "Grand Theft Auto V är ett actionäventyrsspel som utvecklats och publicerats av Rockstar Games. Spelet släpptes i september 2013 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Grand Theft Auto V", 18, 0.0, new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 30, 1, "Red Dead Redemption 2 är ett actionäventyrsspel som utvecklats och publicerats av Rockstar Games. Spelet släpptes i oktober 2018 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Red Dead Redemption 2", 18, 0.0, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 31, 1, "The Elder Scrolls V: Skyrim är ett actionrollspel som utvecklats och publicerats av Bethesda Game Studios. Spelet släpptes i november 2011 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "The Elder Scrolls V: Skyrim", 18, 0.0, new DateTime(2011, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 32, 1, "The Legend of Zelda: Ocarina of Time är ett actionäventyrsspel som utvecklats och publicerats av Nintendo. Spelet släpptes i november 1998 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "The Legend of Zelda: Ocarina of Time", 7, 0.0, new DateTime(1998, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 },
                    { 33, 1, "Super Mario 64 är ett plattformsspel som utvecklats och publicerats av Nintendo. Spelet släpptes i juni 1996 och har blivit ett av de mest populära spelen i världen.", 0.10000000000000001, 1, "Assets/Products/Phoenix Stream Console.webp", false, false, false, "Super Mario 64", 3, 0.0, new DateTime(1996, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_EventId",
                table: "CartItems",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_EventId",
                table: "Carts",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ProductId",
                table: "Carts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CartId",
                table: "Customers",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenreId",
                table: "Products",
                column: "GenreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
