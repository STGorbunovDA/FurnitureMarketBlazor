using Microsoft.AspNetCore.Components;

namespace FurnitureMarketBlazor.Client.Services.OrderService
{
    public class OrderServiceClient : IOrderServiceClient
    {
        private readonly HttpClient _http;
        private readonly IAuthServiceClient _authService;
        private readonly NavigationManager _navigationManager;

        public OrderServiceClient(HttpClient http, IAuthServiceClient authService, NavigationManager navigationManager) =>
            (_http, _authService, _navigationManager) = (http, authService, navigationManager);

        public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");
            return result.Data;
        }

        public async Task<List<OrderOverviewResponse>> GetOrders()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");
            return result.Data;
        }

        public async Task PlaceOrder()
        {
            if (await _authService.IsUserAuthenticated())
                await _http.PostAsync("api/order", null);
            else
                _navigationManager.NavigateTo("login");
        }
    }
}
