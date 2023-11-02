global using FurnitureMarketBlazor.Shared;
global using System.Net.Http.Json;
global using FurnitureMarketBlazor.Client.Services.ProductService;
global using FurnitureMarketBlazor.Client.Services.CategoryService;
global using FurnitureMarketBlazor.Client.Services.CartService;
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


await builder.Build().RunAsync();
