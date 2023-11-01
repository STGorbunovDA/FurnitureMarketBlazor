﻿namespace FurnitureMarketBlazor.Client.Services.ProductService
{
    public interface IProductServiceClient
    {
        event Action ProductsChanged;
        string Message { get; set; }
        List<Product> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetProduct(int productId);
        Task SearchProducts(string searchText);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
    }
}
