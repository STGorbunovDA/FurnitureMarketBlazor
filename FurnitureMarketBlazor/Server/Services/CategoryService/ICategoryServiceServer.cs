namespace FurnitureMarketBlazor.Server.Services.CategoryService
{
    public interface ICategoryServiceServer
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
    }
}
