namespace FurnitureMarketBlazor.Client.Services.ProductTypeService
{
    public interface IProductTypeServiceClient
    {
        event Action OnChange;
        public List<ProductType> ProductTypes { get; set; }
        Task GetProductTypes();
        Task AddProductType(ProductType productType);
        Task UpdateProductType(ProductType productType);
        ProductType CreateNewProductType();
    }
}
