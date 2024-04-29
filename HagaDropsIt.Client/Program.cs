using HagaDropsIt.Client.Components;
using HagaDropsIt.Client.Components.Account;
using HagaDropsIt.Client.Data;
using HagaDropsIt.Client.Services;
using HagaDropsIt.Shared.DTOs;
using HagaDropsIt.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using HagaDropsIt.Client.ChatBot.Plugins;
using HagaDropsIt.Client.ChatBot.Interfaces;
using HagaDropsIt.Client.ChatBot.Services;
using HagaDropsIt.Client.ChatBot.Entities;
using HagaDropsIt.Shared.Entities;
using HagaDropsIt.Client.Utils;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddHttpClient("HagaDropsItAPI",
	client =>
		client.BaseAddress = new Uri("http://localhost:5179")
);
   

builder.Services.AddSingleton<IEventService<EventDto>, EventService>();
builder.Services.AddSingleton<IOrderService<OrderDto>, OrderService>();
builder.Services.AddSingleton<IProductService<ProductDto>, ProductService>();
builder.Services.AddSingleton<IBlobStorageServices, BlobStorageServices>();
builder.Services.AddSingleton<ICategoryService<CategoryDto>, CategoryService>();
builder.Services.AddSingleton<IGenreService<GenreDto>, GenreService>();
builder.Services.AddSingleton<IReviewService<ReviewDto>, ReviewService>();
builder.Services.AddSingleton<ICartService<CartDto, CartItemDto>, CartService>();
builder.Services.AddSingleton<ICustomerService<CustomerDto>, CustomerService>();
builder.Services.AddSingleton<IBlobTools, BlobTools>();
builder.Services.AddCascadingAuthenticationState();
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

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, EmailSender>();


builder.Services.AddHttpClient("FileHandlerServices", client =>
{
    client.Timeout = TimeSpan.FromSeconds(30);  
                                                
});
builder.Services.AddSingleton<IFileHandlerServices, FileHandlerServices>();


//Chatbot
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAIOptions"));
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddSingleton<ProductPlugin>();
builder.Services.AddSingleton<EventPlugin>();
builder.Services.AddSingleton<OrderPlugin>();
builder.Services.AddSingleton<BlobStoragePlugin>();
builder.Services.AddSingleton<FileHandlerPlugin>();


builder.Services.AddSingleton<IDallE3Plugin>(sp =>
{
    var httpClient = new HttpClient();
    var openAIOptions = sp.GetRequiredService<IOptions<OpenAIOptions>>().Value;
    return new DallE3Plugin(httpClient, openAIOptions.ApiKey);
});

builder.Services.AddSingleton<ITextEmbeddingPlugin>(sp =>
{
    var httpClient = new HttpClient(); 
    var openAIOptions = sp.GetRequiredService<IOptions<OpenAIOptions>>().Value;
    return new TextEmbeddingPlugin(httpClient, openAIOptions.ApiKey);
});


builder.Services.AddOptions<OpenAIOptions>()
                       .Bind(builder.Configuration.GetSection(nameof(OpenAIOptions)))
                       .ValidateDataAnnotations()
                       .ValidateOnStart();

builder.Services.AddSingleton<IChatCompletionService>(sp =>
{
    OpenAIOptions options = sp.GetRequiredService<IOptions<OpenAIOptions>>().Value;

    return new OpenAIChatCompletionService(options.ChatModelId, options.ApiKey);
});


builder.Services.AddSingleton<Kernel>(sp =>
{
    var productPlugin = sp.GetRequiredService<ProductPlugin>();
    var orderPlugin = sp.GetRequiredService<OrderPlugin>();
    var eventPlugin = sp.GetRequiredService<EventPlugin>();
    var FileHandlerPlugin = sp.GetRequiredService<FileHandlerPlugin>();
    var dallE3Plugin = sp.GetRequiredService<IDallE3Plugin>();
    var blobStorageServicesPlugin = sp.GetRequiredService<BlobStoragePlugin>();
    var textEmbeddingPlugin = sp.GetRequiredService<ITextEmbeddingPlugin>();

    var pluginCollection = new KernelPluginCollection();
    pluginCollection.AddFromObject(productPlugin);
    pluginCollection.AddFromObject(orderPlugin);
    pluginCollection.AddFromObject(eventPlugin);
    pluginCollection.AddFromObject(dallE3Plugin);
    pluginCollection.AddFromObject(FileHandlerPlugin);
    pluginCollection.AddFromObject(textEmbeddingPlugin);
    pluginCollection.AddFromObject(blobStorageServicesPlugin);
    

    return new Kernel(sp, pluginCollection);

});
//End Chatbot

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
