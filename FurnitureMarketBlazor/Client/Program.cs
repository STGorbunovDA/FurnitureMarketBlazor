global using FurnitureMarketBlazor.Shared.DTO;
global using FurnitureMarketBlazor.Shared.ProductsFolder;
global using FurnitureMarketBlazor.Shared.UserFolder;
global using FurnitureMarketBlazor.Shared.OrderFolder;
global using FurnitureMarketBlazor.Shared.CartFolder;
global using System.Net.Http.Json;
global using FurnitureMarketBlazor.Client.Services.ProductService;
global using FurnitureMarketBlazor.Client.Services.CategoryService;
global using FurnitureMarketBlazor.Client.Services.CartService;
global using FurnitureMarketBlazor.Client.Services.AuthService;
global using FurnitureMarketBlazor.Client.Services.OrderService;
global using Microsoft.AspNetCore.Components.Authorization;
using FurnitureMarketBlazor.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductServiceClient, ProductServiceClient>();
builder.Services.AddScoped<ICategoryServiceClient, CategoryServiceClient>();
builder.Services.AddScoped<ICartServiceClient, CartServiceClient>();
builder.Services.AddScoped<IAuthServiceClient, AuthServiceClient>();
builder.Services.AddScoped<IOrderServiceClient, OrderServiceClient>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
