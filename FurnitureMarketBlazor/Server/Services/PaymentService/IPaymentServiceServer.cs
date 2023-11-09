using Stripe.Checkout;

namespace FurnitureMarketBlazor.Server.Services.PaymentService
{
    public interface IPaymentServiceServer
    {
        Task<Session> CreateCheckoutSession();
        Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request);
    }
}
