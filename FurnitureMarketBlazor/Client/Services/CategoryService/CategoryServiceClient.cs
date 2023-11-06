using FurnitureMarketBlazor.Shared.DTO;
using FurnitureMarketBlazor.Shared.ProductsFolder;

namespace FurnitureMarketBlazor.Client.Services.CategoryService
{
    public class CategoryServiceClient : ICategoryServiceClient
    {
        private readonly HttpClient _http;

        public CategoryServiceClient(HttpClient http) => _http = http;

        public List<Category> Categories { get; set; } = new List<Category>();

        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");
            if (response != null && response.Data != null)
                Categories = response.Data;
        }
    }
}
