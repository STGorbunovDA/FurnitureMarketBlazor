﻿namespace FurnitureMarketBlazor.Server.Services.ProductService
{
    public interface IProductServiceServer
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<List<Product>>> SearchProducts(string searchText);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
    }
}