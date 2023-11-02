namespace FurnitureMarketBlazor.Server.Services.CartService
{
    public interface ICartServiceServer
    {
        Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
    }
}
