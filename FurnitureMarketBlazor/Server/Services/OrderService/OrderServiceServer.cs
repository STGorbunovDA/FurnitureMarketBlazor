namespace FurnitureMarketBlazor.Server.Services.OrderService
{
    public class OrderServiceServer : IOrderServiceServer
    {
        private readonly DataContext _context;
        private readonly ICartServiceServer _cartService;
        private readonly IAuthServiceServer _authService;

        public OrderServiceServer(DataContext context, ICartServiceServer cartService, IAuthServiceServer authService) =>
            (_context, _cartService, _authService) = (context, cartService, authService);

        // Метод для получения деталей заказа по его идентификатору
        public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId)
        {
            // Создаем экземпляр ServiceResponse для возврата результата
            var response = new ServiceResponse<OrderDetailsResponse>();

            // Получаем заказ из базы данных, включая связанные сущности OrderItems, Product и ProductType
            var order = await _context.Orders
                .Include(o => o.OrderItems) // указывает, что нужно подгрузить связанную сущность OrderItems для каждого найденного заказа.
                .ThenInclude(oi => oi.Product) // указывает на подгрузку связанной сущности Product для каждой найденной сущности OrderItem.
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.ProductType) // указывает на подгрузку связанной сущности ProductType для каждой найденной сущности OrderItem.
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate) //  указывает на сортировку заказов по дате заказа в порядке убывания.
                .FirstOrDefaultAsync();

            // Проверяем, найден ли заказ
            if (order == null)
            {
                // Если заказ не найден, устанавливаем Success в false,
                // добавляем сообщение об ошибке и возвращаем response
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            // Создаем объект OrderDetailsResponse для ответа
            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Products = new List<OrderDetailsProductResponse>()
            };

            // Добавляем продукты заказа в список Products
            order.OrderItems.ForEach(item =>
            {
                orderDetailsResponse.Products.Add(new OrderDetailsProductResponse
                {
                    ProductId = item.ProductId,
                    ImageUrl = item.Product.ImageUrl,
                    ProductType = item.ProductType.Name,
                    Quantity = item.Quantity,
                    Title = item.Product.Title,
                    TotalPrice = item.TotalPrice
                });
            });

            // Устанавливаем полученные данные в response и возвращаем response
            response.Data = orderDetailsResponse;
            return response;
        }

        // Метод для получения списка заказов пользователя
        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders()
        {
            // Создаем экземпляр ServiceResponse для возврата результата
            var response = new ServiceResponse<List<OrderOverviewResponse>>();

            // Получаем список заказов пользователя из базы данных, включая связанные сущности OrderItems и Product
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == _authService.GetUserId())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            // Создаем список OrderOverviewResponse для ответа
            var orderResponse = new List<OrderOverviewResponse>();

            // Заполняем список OrderOverviewResponse на основе полученных заказов
            orders.ForEach(o =>
            {
                var products = o.OrderItems.Select(oi => oi.Product.Title);
                //var productCount = o.OrderItems.Count;

                string productText = string.Join(", ", products);

                orderResponse.Add(new OrderOverviewResponse
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    TotalPrice = o.TotalPrice,
                    Product = productText,
                    ProductImageUrl = o.OrderItems.First().Product.ImageUrl // Показать первую картинку продукта
                });
            });

            #region старый метод

            //orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            //{
            //    Id = o.Id,
            //    OrderDate = o.OrderDate,
            //    TotalPrice = o.TotalPrice,
            //    Product = o.OrderItems.Count > 1 ?
            //        $"{o.OrderItems.First().Product.Title} and" +
            //        $" {o.OrderItems.Count - 1} more..." :
            //        o.OrderItems.First().Product.Title,
            //    ProductImageUrl = o.OrderItems.First().Product.ImageUrl
            //}));

            #endregion

            // Устанавливаем полученные данные в response и возвращаем response
            response.Data = orderResponse;
            return response;
        }

        // Метод для размещения заказа
        public async Task<ServiceResponse<bool>> PlaceOrder()
        {
            // Получаем список товаров корзины из сервиса корзины
            var products = (await _cartService.GetDbCartProducts()).Data;

            // Вычисляем общую стоимость всех товаров в корзине
            decimal totalPrice = 0;

            products.ForEach(product =>
            {
                totalPrice += product.Price * product.Quantity;
            });

            // Создаем список OrderItem на основе товаров из корзины
            var orderItems = new List<OrderItem>();
            products.ForEach(product =>
                orderItems.Add(
                    new OrderItem
                    {
                        ProductId = product.ProductId,
                        ProductTypeId = product.ProductTypeId,
                        Quantity = product.Quantity,
                        TotalPrice = product.Price * product.Quantity
                    }));

            // Создаем объект Order со значениями для размещения заказа
            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems
            };

            // Добавляем заказ в базу данных
            _context.Orders.Add(order);

            // Удаляем элементы корзины пользователя из базы данных
            _context.CartItems.RemoveRange(_context.CartItems
                .Where(ci => ci.UserId == _authService.GetUserId()));

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            // Возвращаем успешный результат размещения заказа
            return new ServiceResponse<bool> { Data = true };
        }
    }
}
