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
    *  В данном коде настраивается аутентификация с помощью JWT, где выполняется проверка подписи 
       и включение необходимых параметров проверки токена. Это позволяет ASP.NET Core приложению 
       проверять и использовать JWT для аутентификации запросов клиента.
    
    * С помощью метода AddAuthentication добавляется сервис аутентификации в контейнер зависимостей приложения. 
      В данном случае, указывается схема аутентификации JwtBearerDefaults.AuthenticationScheme, 
      которая предназначена для работы с JWT.

    * Метод AddJwtBearer добавляет JWT-аутентификацию к настройкам аутентификации. Ему передается настройка 
      options в виде лямбда-выражения.

    * Внутри лямбда-выражения настраиваются параметры проверки токена (TokenValidationParameters). 
      В данном случае:

        * ValidateIssuerSigningKey - устанавливается значение true, чтобы проверять, что ключ 
          подписи токена (IssuerSigningKey) является действительным и совпадает с ожидаемым.

        * IssuerSigningKey - устанавливается симметричный ключ подписи токена (SymmetricSecurityKey), 
          полученный из значения конфигурационного параметра "AppSettings:Token".

        * ValidateIssuer и ValidateAudience - устанавливаются значения false, чтобы отключить проверку 
          издателя токена (Issuer) и аудитории токена (Audience). То есть, эти параметры не будут 
          проверяться валидатором.
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
    * Добавляет службу IHttpContextAccessor в контейнер зависимостей приложения.

    * IHttpContextAccessor является интерфейсом, который предоставляет доступ к текущему объекту HttpContext. 
      Объект HttpContext представляет контекст выполнения для каждого запроса, и содержит информацию о запросе, 
      ответе и других сведениях, связанных с текущим HTTP-запросом.

    * Добавление IHttpContextAccessor в контейнер зависимостей позволяет внедрять его в другие компоненты 
      приложения, например, в сервисы или контроллеры. Это может быть полезно в ситуациях, когда вам может 
      понадобиться получить доступ к контексту HTTP-запроса внутри сервиса или компонента приложения, 
      например, для получения информации о текущем пользователе, обработке заголовков запроса и т.д.

    * В целом, IHttpContextAccessor предоставляет удобный способ получения доступа к контексту HTTP-запроса 
      внутри зависимых компонентов приложения.
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
