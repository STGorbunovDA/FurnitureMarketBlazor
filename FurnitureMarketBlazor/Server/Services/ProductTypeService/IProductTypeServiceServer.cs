namespace FurnitureMarketBlazor.Server.Services.ProductTypeService
{
    public interface IProductTypeServiceServer
    {
        Task<ServiceResponse<List<ProductType>>> GetProductTypes();
        Task<ServiceResponse<List<ProductType>>> AddProductType(ProductType productType);
        Task<ServiceResponse<List<ProductType>>> UpdateProductType(ProductType productType);
    }
}
