namespace FurnitureMarketBlazor.Server.Services.CartService
{
    public class CartServiceServer : ICartServiceServer
    {
        private readonly DataContext _context;
        private readonly IAuthServiceServer _authService;

        public CartServiceServer(DataContext context, IAuthServiceServer authService) =>
            (_context, _authService) = (context, authService);


        /*
            * Метод GetCartProducts() принимает список cartItems, каждый элемент которого представляет 
              позицию в корзине пользователя. Он выполняет поиск соответствующих продуктов и вариантов 
              продуктов в базе данных и формирует список CartProductResponse, который содержит информацию 
              о каждом продукте в корзине.
        */
        public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
        {
            // Создание экземпляра ServiceResponse<List<CartProductResponse>> и инициализация списка данных
            var result = new ServiceResponse<List<CartProductResponse>>
            {
                Data = new List<CartProductResponse>()
            };

            // Перебор каждого элемента в списке cartItems
            foreach (var item in cartItems)
            {
                // Получение продукта из базы данных по его идентификатору (item.ProductId)
                // Если продукт с таким идентификатором не найден, переход к следующей итерации цикла
                var product = await _context.Products
                    .Where(p => p.Id == item.ProductId)
                    .FirstOrDefaultAsync();

                if (product == null)
                    continue;

                // Получение варианта продукта из базы данных, соответствующего заданным критериям:
                // item.ProductId - идентификатор продукта, item.ProductTypeId - идентификатор типа продукта
                // Включение связанной сущности ProductType для productVariant
                var productVariant = await _context.ProductVariants
                    .Where(v => v.ProductId == item.ProductId
                        && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

                if (productVariant == null)
                    continue;

                // Создание экземпляра CartProductResponse и заполнение его свойств значениями из найденных продукта и варианта продукта
                var cartProduct = new CartProductResponse
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity
                };

                // Добавление cartProduct в список данных result.Data
                result.Data.Add(cartProduct);
            }

            return result;
        }

        /*
            * Метод StoreCartItems() принимает список cartItems, каждый элемент которого 
              представляет позицию в корзине пользователя. Он просто сохраняет элементы 
              корзины пользователя в базе данных и возвращает обновленный список продуктов корзины.
            
        */
        public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
        {
            // Для каждого элемента cartItem в списке cartItems задается значение свойства
            // UserId с помощью метода GetUserId()
            cartItems.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());

            // Добавление всех элементов из списка cartItems в DbSet CartItems в контекст базы данных _context
            _context.CartItems.AddRange(cartItems);

            // Асинхронное сохранение изменений в базе данных
            await _context.SaveChangesAsync();

            // Возврат результата вызова метода GetDbCartProducts()
            return await GetDbCartProducts();
        }

        /*
            * Возвращает результат вызова метода GetCartProducts() с аргументом - асинхронно
              полученным списком элементов из базы данных, соответствующим условию, что значение
              свойства UserId равно значению, возвращаемому методом GetUserId()  
        */
        public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts(int? userId = null)
        {
            /*
                * Вызывается метод _context.CartItems, который представляет таблицу элементов 
                  корзины в базе данных.
                * Применяется фильтр Where(ci => ci.UserId == GetUserId()), чтобы выбрать только 
                  те элементы корзины, у которых значение свойства UserId равно значению, 
                  возвращаемому методом GetUserId(). Это связывает элементы корзины 
                  с определенным пользователем.
                * Вызывается метод ToListAsync(), чтобы асинхронно получить список элементов 
                  корзины из базы данных, удовлетворяющих фильтру.
                * Обновленный список элементов корзины передается в метод GetCartProducts().
                * Возвращается результат вызова GetCartProducts(), который возвращает 
                  ServiceResponse<List<CartProductResponse>> - ответ службы, содержащий 
                  список продуктов корзины, готовый для использования.
            */

            if (userId == null)
                userId = _authService.GetUserId();

            return await GetCartProducts(await _context.CartItems
                .Where(ci => ci.UserId == userId).ToListAsync());
        }

        /*
            * Метод возрващающий общее кол-во продуктов по пользователю.
        */
        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            // Объявляем переменную count для хранения количества элементов корзины
            var count = (await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count;

            // Создаем новый объект ServiceResponse<int> и устанавливаем его свойство
            // Data равным значению переменной count
            return new ServiceResponse<int> { Data = count };
        }

        /*
            * Добавление продукта к карточке товара по пользователю
        */
        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            // Присваиваем свойству UserId объекта cartItem значение, полученное из метода GetUserId()
            cartItem.UserId = _authService.GetUserId();

            // Выполняется асинхронный запрос к базе данных с помощью _context.CartItems, чтобы
            // получить первый элемент корзины, у которого значение свойства ProductId равно
            // значению свойства ProductId объекта cartItem, значение свойства ProductTypeId
            // равно значению свойства ProductTypeId объекта cartItem и значение свойства UserId
            // равно значению свойства UserId объекта cartItem
            var sameItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId &&
                ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == cartItem.UserId);

            // Проверяем, есть ли в корзине уже элемент с такими же значениями ProductId, ProductTypeId и UserId
            // Если такого элемента нет, добавляем объект cartItem в DbSet _context.CartItems
            if (sameItem == null)
                _context.CartItems.Add(cartItem);
            else
            {
                // Если такой элемент уже есть в корзине, увеличиваем его Quantity
                // на значение свойства Quantity объекта cartItem
                sameItem.Quantity += cartItem.Quantity;
            }

            // Применяем изменения, внесенные в объект _context, к базе данных
            await _context.SaveChangesAsync();

            // Создаем новый объект ServiceResponse<bool> и устанавливаем его свойство Data равным значению true
            return new ServiceResponse<bool> { Data = true };
        }

        /*
             * Обновление кол-ва продуктов по пользователю
        */
        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            // Выполняется асинхронный запрос к базе данных с помощью _context.CartItems, чтобы
            // получить первый элемент корзины, у которого значение свойства ProductId равно
            // значению свойства ProductId объекта cartItem, значение свойства ProductTypeId
            // равно значению свойства ProductTypeId объекта cartItem и значение свойства UserId
            // равно значению, полученному из метода GetUserId()
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == cartItem.ProductId &&
                ci.ProductTypeId == cartItem.ProductTypeId && ci.UserId == _authService.GetUserId());

            // Проверяем, существует ли dbCartItem
            // Если dbCartItem не существует, создаем новый объект ServiceResponse<bool> с
            // установленными свойствами Data = false, Success = false и Message = "Cart item does not exist."
            if (dbCartItem == null)
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "Cart item does not exist." };

            // Устанавливаем значение свойства Quantity объекта dbCartItem равным
            // значению свойства Quantity объекта cartItem
            dbCartItem.Quantity = cartItem.Quantity;

            // Применяем изменения, внесенные в объект _context, к базе данных
            await _context.SaveChangesAsync();

            // Создаем новый объект ServiceResponse<bool> и устанавливаем его свойство Data равным значению true
            return new ServiceResponse<bool> { Data = true };
        }

        /*
            * Метод удаления продукта из корзины по пользователю
        */
        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
        {
            // Выполняется асинхронный запрос к базе данных с помощью _context.CartItems, чтобы
            // получить первый элемент корзины, у которого значение свойства ProductId равно
            // значению productId, значение свойства ProductTypeId равно значению productTypeId
            // и значение свойства UserId равно значению, полученному из метода GetUserId()
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.ProductId == productId &&
                ci.ProductTypeId == productTypeId && ci.UserId == _authService.GetUserId());

            // Проверяем, существует ли dbCartItem
            // Если dbCartItem не существует, создаем новый объект ServiceResponse<bool> с
            // установленными свойствами Data = false, Success = false и Message = "Cart item does not exist."
            if (dbCartItem == null)
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "Cart item does not exist." };

            // Удаляем dbCartItem из контекста базы данных
            _context.CartItems.Remove(dbCartItem);

            // Применяем изменения, внесенные в объект _context, к базе данных
            await _context.SaveChangesAsync();

            // Создаем новый объект ServiceResponse<bool> и устанавливаем его свойство Data равным значению true
            return new ServiceResponse<bool> { Data = true };
        }

    }
}
