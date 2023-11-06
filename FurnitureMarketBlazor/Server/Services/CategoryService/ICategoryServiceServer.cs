using FurnitureMarketBlazor.Shared.DTO;
using FurnitureMarketBlazor.Shared.ProductsFolder;

namespace FurnitureMarketBlazor.Server.Services.CategoryService
{
    public interface ICategoryServiceServer
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
    }
}
