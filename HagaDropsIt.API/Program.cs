using HagaDropsIt.API.Extensions;
using HagaDropsIt.Mongo.Orders.Repositories;
using HagaDropsIt.SQL.Store;
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.SQL.Store.Repositories;
using Microsoft.EntityFrameworkCore;
using HagaDropsIt.Shared.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configure SQL database context
var connectionString = builder.Configuration.GetConnectionString("HagaDropsItStore");
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IOrderService<Order>, OrderRepository>();
builder.Services.AddScoped<IProductService<Product>, ProductRepository>();
builder.Services.AddScoped<IEventService<Event>, EventRepository>();
builder.Services.AddScoped<ICartService<Cart, CartItem>, CartRepository>();
builder.Services.AddScoped<ICustomerService<Customer>, CustomerRepository>();

builder.Services.AddScoped<IReviewService<Review>, ReviewRepository>();
builder.Services.AddScoped<IGenreService<Genre>, GenreRepository>();
builder.Services.AddScoped<ICategoryService<Category>, CategoryRepository>();

var app = builder.Build();
app.MapOrderEndpoints();
app.MapProductEndpoints();
app.MapEventEndpoints();
app.MapReviewEndpoints();
app.MapGenreEndpoints();
app.MapCategoryEndpoints();
app.MapCartEndpoints();
app.MapCustomerEndpoints();

app.Run();
