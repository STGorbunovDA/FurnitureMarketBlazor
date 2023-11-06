global using FurnitureMarketBlazor.Shared.DTO;
global using FurnitureMarketBlazor.Shared.ProductsFolder;
global using FurnitureMarketBlazor.Shared.UserFolder;
global using FurnitureMarketBlazor.Shared.OrderFolder;
global using FurnitureMarketBlazor.Shared.CartFolder;
global using Microsoft.EntityFrameworkCore;
global using FurnitureMarketBlazor.Server.Data;
global using Microsoft.AspNetCore.Mvc;
global using FurnitureMarketBlazor.Server.Services.ProductService;
global using FurnitureMarketBlazor.Server.Services.CategoryService;
global using FurnitureMarketBlazor.Server.Services.CartService;
global using FurnitureMarketBlazor.Server.Services.AuthService;
global using FurnitureMarketBlazor.Server.Services.OrderService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
builder.Services.AddScoped<IOrderServiceServer, OrderServiceServer>();

/*
    *  � ������ ���� ������������� �������������� � ������� JWT, ��� ����������� �������� ������� 
       � ��������� ����������� ���������� �������� ������. ��� ��������� ASP.NET Core ���������� 
       ��������� � ������������ JWT ��� �������������� �������� �������.
    
    * � ������� ������ AddAuthentication ����������� ������ �������������� � ��������� ������������ ����������. 
      � ������ ������, ����������� ����� �������������� JwtBearerDefaults.AuthenticationScheme, 
      ������� ������������� ��� ������ � JWT.

    * ����� AddJwtBearer ��������� JWT-�������������� � ���������� ��������������. ��� ���������� ��������� 
      options � ���� ������-���������.

    * ������ ������-��������� ������������� ��������� �������� ������ (TokenValidationParameters). 
      � ������ ������:

        * ValidateIssuerSigningKey - ��������������� �������� true, ����� ���������, ��� ���� 
          ������� ������ (IssuerSigningKey) �������� �������������� � ��������� � ���������.

        * IssuerSigningKey - ��������������� ������������ ���� ������� ������ (SymmetricSecurityKey), 
          ���������� �� �������� ����������������� ��������� "AppSettings:Token".

        * ValidateIssuer � ValidateAudience - ��������������� �������� false, ����� ��������� �������� 
          �������� ������ (Issuer) � ��������� ������ (Audience). �� ����, ��� ��������� �� ����� 
          ����������� �����������.
*/
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

/*
    * ��������� ������ IHttpContextAccessor � ��������� ������������ ����������.

    * IHttpContextAccessor �������� �����������, ������� ������������� ������ � �������� ������� HttpContext. 
      ������ HttpContext ������������ �������� ���������� ��� ������� �������, � �������� ���������� � �������, 
      ������ � ������ ���������, ��������� � ������� HTTP-��������.

    * ���������� IHttpContextAccessor � ��������� ������������ ��������� �������� ��� � ������ ���������� 
      ����������, ��������, � ������� ��� �����������. ��� ����� ���� ������� � ���������, ����� ��� ����� 
      ������������ �������� ������ � ��������� HTTP-������� ������ ������� ��� ���������� ����������, 
      ��������, ��� ��������� ���������� � ������� ������������, ��������� ���������� ������� � �.�.

    * � �����, IHttpContextAccessor ������������� ������� ������ ��������� ������� � ��������� HTTP-������� 
      ������ ��������� ����������� ����������.
*/
builder.Services.AddHttpContextAccessor();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
