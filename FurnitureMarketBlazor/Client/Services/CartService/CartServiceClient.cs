using Blazored.LocalStorage;

namespace FurnitureMarketBlazor.Client.Services.CartService
{
    public class CartServiceClient : ICartServiceClient
    {
        public event Action OnChange;

        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthServiceClient _authService;

        public CartServiceClient(ILocalStorageService localStorage, HttpClient http, IAuthServiceClient authService) =>
            (_localStorage, _http, _authService) = (localStorage, http, authService);

        public async Task GetCartItemsCount()
        {
            // Проверяем, аутентифицирован ли пользователь, вызывая метод IsUserAuthenticated(),
            // который возвращает true или false,
            // в зависимости от того, прошла ли успешно аутентификация
            if (await _authService.IsUserAuthenticated())
            {
                // Если пользователь аутентифицирован, выполняем следующие действия
                // Отправляем GET-запрос по адресу "api/cart/count" и ожидаем ответ в формате JSON,
                // который будет десериализован в объект типа ServiceResponse<int>
                // с помощью метода GetFromJsonAsync<T>()
                var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");

                // Извлекаем значение поля Data из полученного объекта result
                var count = result.Data;

                // Сохраняем значение count в локальное хранилище с ключом "cartItemsCount"
                // с помощью метода SetItemAsync<T>()
                await _localStorage.SetItemAsync<int>("cartItemsCount", count);
            }
            else
            {
                // Если пользователь не аутентифицирован, выполняем следующие действия
                // Получаем список элементов корзины из локального хранилища с ключом
                // "cart" с помощью метода GetItemAsync<T>()
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                /*
                    * Вычисляем количество элементов в корзине. Если cart равен null,
                      то устанавливаем значение count равным 0,
                      иначе устанавливаем значение count равным количеству элементов в списке cart
                      - var count = cart != null ? cart.Count : 0;

                    * Сохраняем значение count в локальное хранилище с ключом "cartItemsCount"
                      с помощью метода SetItemAsync<T>()
                      - await _localStorage.SetItemAsync<int>("cartItemsCount", count);
                */
                await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);

            }

            // Вызываем событие OnChange для уведомления об изменении количества элементов в корзине
            OnChange.Invoke();
        }

        public async Task AddToCart(CartItem cartItem)
        {
            // Проверяем, аутентифицирован ли пользователь, вызывая метод IsUserAuthenticated(),
            // который возвращает true или false, в зависимости от того, прошла ли успешно аутентификация
            // Если пользователь аутентифицирован, выполняем следующие действия
            // Отправляем POST-запрос по адресу "api/cart/add", передавая в теле запроса объект cartItem,
            // который будет сериализован в формат JSON с помощью метода PostAsJsonAsync()
            if (await _authService.IsUserAuthenticated())
                await _http.PostAsJsonAsync("api/cart/add", cartItem);
            else
            {
                // Если пользователь не аутентифицирован, выполняем следующие действия
                // Получаем список элементов корзины из локального хранилища с ключом "cart" с помощью метода GetItemAsync<T>()
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                // Проверяем, равен ли cart null (т.е. список элементов корзины пуст)
                // Если список элементов корзины пуст, создаем новый пустой список
                if (cart == null)
                    cart = new List<CartItem>();

                // Проверяем, есть ли в корзине уже элемент с тем же ProductId и ProductTypeId, что и cartItem,
                // используя метод Find() для поиска такого элемента в списке cart
                var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);

                // Если элемент с такими ProductId и ProductTypeId не найден
                // Добавляем новый элемент cartItem в список cart
                if (sameItem == null)
                    cart.Add(cartItem);
                else
                {
                    // Иначе, если элемент с такими ProductId и ProductTypeId уже существует в корзине,
                    // увеличиваем его количество (Quantity) на значение cartItem.Quantity
                    sameItem.Quantity += cartItem.Quantity;
                }

                // Сохраняем обновленный список элементов корзины в локальное хранилище с ключом "cart"
                // с помощью метода SetItemAsync<T>()
                await _localStorage.SetItemAsync("cart", cart);
            }
            // Вызываем метод GetCartItemsCount() для обновления количества элементов в корзине
            await GetCartItemsCount();
        }

        public async Task<List<CartProductResponse>> GetCartProducts()
        {
            // Проверяем, аутентифицирован ли пользователь, вызывая метод IsUserAuthenticated(),
            // который возвращает true или false в зависимости от успешности аутентификации
            if (await _authService.IsUserAuthenticated())
            {
                // Если пользователь аутентифицирован, выполняем следующие действия
                // Отправляем GET-запрос по адресу "api/cart" и получаем ответ в формате JSON
                // Полученный ответ десериализуется в объект типа ServiceResponse<List<CartProductResponse>>
                // с помощью метода GetFromJsonAsync<T>()
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/cart");

                return response.Data;
            }
            else
            {
                // Если пользователь не аутентифицирован, выполняем следующие действия
                // Получаем список элементов корзины из локального хранилища с ключом "cart"
                // с помощью метода GetItemAsync<T>()
                var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                // Проверяем, равен ли cartItems null (т.е. список элементов корзины пуст)
                // Если список элементов корзины пуст, возвращаем новый пустой список
                // типа List<CartProductResponse>
                if (cartItems == null)
                    return new List<CartProductResponse>();


                // Отправляем POST-запрос по адресу "api/cart/products", передавая в теле запроса список cartItems,
                // который будет сериализован в формат JSON с помощью метода PostAsJsonAsync()
                var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);

                // Читаем содержимое ответа в формате JSON и десериализуем его в объект типа
                // ServiceResponse<List<CartProductResponse>> с помощью метода ReadFromJsonAsync<T>()
                // для доступа к данным (свойство Data)
                var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();

                return cartProducts.Data;
            }
        }

        public async Task RemoveProductFromCart(int productId, int productTypeId)
        {
            // Проверяем, аутентифицирован ли пользователь, вызывая метод IsUserAuthenticated(),
            // который возвращает true или false в зависимости от успешности аутентификации
            // Если пользователь аутентифицирован, выполняем следующие действия
            // Отправляем DELETE-запрос по адресу $"api/cart/{productId}/{productTypeId}"
            // с помощью метода DeleteAsync()
            if (await _authService.IsUserAuthenticated())
                await _http.DeleteAsync($"api/cart/{productId}/{productTypeId}");
            else
            {
                // Если пользователь не аутентифицирован, выполняем следующие действия
                // Получаем список элементов корзины из локального хранилища с ключом "cart"
                // с помощью метода GetItemAsync<T>()
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                // Проверяем, равен ли cart null (т.е. список элементов корзины пуст)
                if (cart == null)
                    return;

                // Ищем элемент корзины в списке cart по заданным условиям, используя метод Find() и лямбда-выражение
                var cartItem = cart.Find(x => x.ProductId == productId && x.ProductTypeId == productTypeId);

                // Проверяем, найден ли элемент корзины
                if (cartItem != null)
                {
                    // Если элемент корзины найден, удаляем его из списка cart с помощью метода Remove()
                    cart.Remove(cartItem);

                    // Сохраняем измененный список элементов корзины в локальное хранилище с помощью метода SetItemAsync()
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            // Получаем список элементов корзины из локального хранилища
            // с ключом "cart" с помощью метода GetItemAsync<T>()
            var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

            // Проверяем, равен ли localCart null (т.е. список элементов корзины пуст)
            if (localCart == null)
                return;

            // Отправляем POST-запрос по адресу "api/cart" и передаем localCart
            // в качестве JSON-данных с помощью метода PostAsJsonAsync()
            await _http.PostAsJsonAsync("api/cart", localCart);

            // Проверяем значение параметра emptyLocalCart
            if (emptyLocalCart)
            {
                // Если параметр emptyLocalCart равен true, удаляем список элементов корзины
                // из локального хранилища с ключом "cart" с помощью метода RemoveItemAsync()
                await _localStorage.RemoveItemAsync("cart");
            }
        }

        public async Task UpdateQuantity(CartProductResponse product)
        {
            // Проверяем, аутентифицирован ли пользователь, вызывая метод IsUserAuthenticated()
            if (await _authService.IsUserAuthenticated())
            {
                // Если пользователь аутентифицирован, создаем объект CartItem
                // с полями ProductId, Quantity и ProductTypeId
                var request = new CartItem
                {
                    ProductId = product.ProductId,
                    Quantity = product.Quantity,
                    ProductTypeId = product.ProductTypeId
                };
                // Отправляем PUT-запрос по адресу "api/cart/update-quantity"
                // и передаем request в качестве JSON-данных
                // с помощью метода PutAsJsonAsync()
                await _http.PutAsJsonAsync("api/cart/update-quantity", request);
            }
            else
            {
                // Если пользователь не аутентифицирован, получаем список элементов корзины
                // из локального хранилища с ключом "cart"
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");

                // Проверяем, равен ли cart null (т.е. список элементов корзины пуст)
                // Если список элементов корзины пуст, выходим из метода
                if (cart == null)
                    return;

                // Ищем элемент корзины с заданными значениями ProductId и ProductTypeId с помощью метода Find()
                var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.ProductTypeId == product.ProductTypeId);

                // Проверяем, найден ли элемент корзины
                if (cartItem != null)
                {
                    // Если элемент корзины найден, обновляем его Quantity значением из product
                    cartItem.Quantity = product.Quantity;

                    // Обновляем список элементов корзины в локальном хранилище с помощью метода SetItemAsync()
                    await _localStorage.SetItemAsync("cart", cart);
                }
            }
        }
    }
}
