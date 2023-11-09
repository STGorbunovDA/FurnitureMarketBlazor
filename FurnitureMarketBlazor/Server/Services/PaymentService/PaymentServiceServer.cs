using Stripe.Checkout;
using Stripe;
using FurnitureMarketBlazor.Server.Infrastructure;

namespace FurnitureMarketBlazor.Server.Services.PaymentService
{
    public class PaymentServiceServer : IPaymentServiceServer
    {
        private readonly ICartServiceServer _cartService;
        private readonly IAuthServiceServer _authService;
        private readonly IOrderServiceServer _orderService;

        // скачать https://github.com/stripe/stripe-cli/releases/tag/v1.18.0 
        // извлечь желательно в C:\Development\Stripe 
        // Запустить через cmd c правами админа C:\Development\Stripe\stripe
        // stripe login
        // stripe listen --forward-to https://localhost:7197/api/payment


        string secret = FileDataReader.Data[1];

        public PaymentServiceServer(ICartServiceServer cartService, IAuthServiceServer authService,
            IOrderServiceServer orderService)
        {
            StripeConfiguration.ApiKey = FileDataReader.Data[0];

            _cartService = cartService;
            _authService = authService;
            _orderService = orderService;
        }

        public async Task<Session> CreateCheckoutSession()
        {
            var products = (await _cartService.GetDbCartProducts()).Data; // Получение продуктов из корзины
            var lineItems = new List<SessionLineItemOptions>(); // Создание списка позиций заказа для сессии оплаты

            products.ForEach(product => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = product.Price * 100, // Установка стоимости продукта в копейках
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.Title, // Установка названия продукта
                        Images = new List<string> { product.ImageUrl } // Установка изображения продукта
                    }
                },
                Quantity = product.Quantity // Установка количества продукта
            }));

            var options = new SessionCreateOptions
            {
                CustomerEmail = _authService.GetUserEmail(), // Установка электронной почты пользователя из аутентификации
                ShippingAddressCollection =
                    new SessionShippingAddressCollectionOptions
                    {
                        AllowedCountries = new List<string> { "US" }
                    },
                PaymentMethodTypes = new List<string>
                {
                    "card" // Установка типа платежного метода (карта)
                },
                LineItems = lineItems,  // Установка списка позиций заказа
                Mode = "payment", // Установка режима сессии (оплата)
                SuccessUrl = "https://localhost:7197/order-success", // URL-адрес успешного завершения заказа
                CancelUrl = "https://localhost:7197/cart" // URL-адрес отмены оплаты и возврата в корзину
            };

            var service = new SessionService();
            Session session = service.Create(options); // Создание сессии оплаты Stripe
            return session;
        }

        public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
        {
            var json = await new StreamReader(request.Body).ReadToEndAsync();  // Чтение данных запроса в формате JSON

            try
            {
                // Обработка webhook события от Stripe и проверка подлинности с помощью секретного ключа
                var stripeEvent = EventUtility.ConstructEvent(
                        json,
                        request.Headers["Stripe-Signature"],
                        secret
                    );

                // Получение пользователя по указанной электронной почте из аутентификации
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var user = await _authService.GetUserByEmail(session.CustomerEmail);

                    //Размещение заказа для пользователя с указанным идентификатором
                    await _orderService.PlaceOrder(user.Id);
                }

                // костыль т.к. веб хук stripe метода FulfillOrder возвращает ошибку  500
                //await _orderService.PlaceOrder(_authService.GetUserId());

                // Возврат успешного результата выполнения операции
                return new ServiceResponse<bool> { Data = true };
            }
            catch (StripeException e)
            {
                // Возврат результата с ошибкой, если возникла ошибка обработки Stripe
                return new ServiceResponse<bool> { Data = false, Success = false, Message = e.Message };
            }
        }
    }
}
