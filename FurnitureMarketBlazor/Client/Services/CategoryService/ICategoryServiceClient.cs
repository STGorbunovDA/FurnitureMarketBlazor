namespace FurnitureMarketBlazor.Client.Services.CategoryService
{
    public interface ICategoryServiceClient
    {
        List<Category> Categories { get; set; }
        Task GetCategories();
    }
}
