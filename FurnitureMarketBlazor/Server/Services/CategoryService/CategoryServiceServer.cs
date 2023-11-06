using FurnitureMarketBlazor.Shared.DTO;
using FurnitureMarketBlazor.Shared.ProductsFolder;

namespace FurnitureMarketBlazor.Server.Services.CategoryService
{
    public class CategoryServiceServer : ICategoryServiceServer
    {
        private readonly DataContext _context;

        public CategoryServiceServer(DataContext context) => _context = context;

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return new ServiceResponse<List<Category>>
            {
                Data = categories
            };
        }
    }
}
