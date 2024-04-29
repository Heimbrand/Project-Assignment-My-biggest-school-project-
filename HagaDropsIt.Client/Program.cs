using HagaDropsIt.Client.Components;
using HagaDropsIt.Client.Components.Account;
using HagaDropsIt.Client.Data;
using HagaDropsIt.Client.Services;
using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HagaDropsIt.Shared.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient("HagaDropsItAPI",
	client =>
		client.BaseAddress = new Uri("http://localhost:5118")
);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IEventService<EventDto>, EventService>();
builder.Services.AddScoped<IOrderService<OrderDto>, OrderService>();
builder.Services.AddScoped<IProductService<ProductDto>, ProductService>();
builder.Services.AddScoped<ICategoryService<CategoryDto>, CategoryService>();
builder.Services.AddScoped<IGenreService<GenreDto>, GenreService>();
builder.Services.AddScoped<IReviewService<ReviewDto>, ReviewService>();
builder.Services.AddScoped<ICartService<CartDto, CartItemDto>, CartService>();
builder.Services.AddScoped<ICustomerService<CustomerDto>, CustomerService>();

builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
