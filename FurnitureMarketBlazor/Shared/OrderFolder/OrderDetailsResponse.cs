namespace FurnitureMarketBlazor.Shared.OrderFolder
{
    // Ответ на детали заказа
    public class OrderDetailsResponse
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailsProductResponse> Products { get; set; }
    }
}
