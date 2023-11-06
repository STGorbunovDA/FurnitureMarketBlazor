namespace FurnitureMarketBlazor.Shared.OrderFolder
{
    // Хранение общих Деталей Order's
    public class OrderDetailsResponse
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailsProductResponse> Products { get; set; }
    }
}
