global using FurnitureMarketBlazor.Shared;
global using Microsoft.EntityFrameworkCore;
global using FurnitureMarketBlazor.Server.Data;
global using Microsoft.AspNetCore.Mvc;
global using FurnitureMarketBlazor.Server.Services.ProductService;
global using FurnitureMarketBlazor.Server.Services.CategoryService;
global using FurnitureMarketBlazor.Server.Services.CartService;
global using FurnitureMarketBlazor.Server.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductServiceServer, ProductServiceServer>();
builder.Services.AddScoped<ICategoryServiceServer, CategoryServiceServer>();
builder.Services.AddScoped<ICartServiceServer, CartServiceServer>();
builder.Services.AddScoped<IAuthServiceServer, AuthServiceServer>();

var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
