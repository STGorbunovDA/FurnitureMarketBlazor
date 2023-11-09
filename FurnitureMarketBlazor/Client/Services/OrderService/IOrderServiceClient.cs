namespace FurnitureMarketBlazor.Client.Services.OrderService
{
    public interface IOrderServiceClient
    {
        Task<string> PlaceOrder();
        Task<List<OrderOverviewResponse>> GetOrders();
        Task<OrderDetailsResponse> GetOrderDetails(int orderId);
    }
}
