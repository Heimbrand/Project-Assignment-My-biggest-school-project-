using HagaDropsIt.API.Extensions;
using HagaDropsIt.SQL.Store;
using HagaDropsIt.Mongo.Orders.Repositories;
using HagaDropsIt.Shared.Interfaces;
using HagaDropsIt.SQL.Store.Repositories;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;
using HagaDropsIt.Mongo.Orders.Data;
using MongoDB.Driver;
using HagaDropsIt.BlobStorage;
using HagaDropsIt.Shared.Entities;

var builder = WebApplication.CreateBuilder(args);

// Configure SQL database context
var connectionString = builder.Configuration.GetConnectionString("HagaDropsItStore");
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Blob Storage
var blobConnectionString = builder.Configuration.GetConnectionString("HagaBlobStorage");
if (string.IsNullOrEmpty(blobConnectionString))
{
    throw new InvalidOperationException("Blob Storage connection string is missing in configuration.");
}
builder.Services.AddSingleton(new BlobServiceClient(blobConnectionString));

// Configure HttpClient
builder.Services.AddHttpClient("GeneralAPI", client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("BlobStorageClient");


// Register Blob Storage Services
builder.Services.AddScoped<IBlobStorageServices>(provider =>
{
    var blobServiceClient = provider.GetRequiredService<BlobServiceClient>();
    var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient("BlobStorageClient");
    return new BlobStorageRepository(httpClient, blobServiceClient, provider.GetRequiredService<ILogger<BlobStorageRepository>>());
});

// Configure MongoDB
var mongoSettings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
if (mongoSettings is null)
{
    throw new InvalidOperationException("MongoDbSettings is missing in configuration.");
}
builder.Services.AddScoped<IOrderService<Order>>(provider => new OrderRepository(mongoSettings));
builder.Services.AddScoped<IProductService<Product>, ProductRepository>();
builder.Services.AddScoped<IEventService<Event>, EventRepository>();
builder.Services.AddScoped<ICartService<Cart, CartItem>, CartRepository>();
builder.Services.AddScoped<ICustomerService<Customer>, CustomerRepository>();

builder.Services.AddScoped<IReviewService<Review>, ReviewRepository>();
builder.Services.AddScoped<IGenreService<Genre>, GenreRepository>();
builder.Services.AddScoped<ICategoryService<Category>, CategoryRepository>();



// Build and configure the application
var app = builder.Build();
app.MapOrderEndpoints();
app.MapProductEndpoints();
app.MapEventEndpoints();
app.MapReviewEndpoints();
app.MapGenreEndpoints();
app.MapCategoryEndpoints();
app.MapBlobStorageEndpoints();
app.MapCartEndpoints();
app.MapCustomerEndpoints();

app.Run();
