using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace FurnitureMarketBlazor.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
        {
            _localStorageService = localStorageService;
            _http = http;
        }

        /*
            * Данный метод GetAuthenticationStateAsync является переопределением метода базового 
              класса AuthenticationStateProvider. Он предназначен для получения текущего 
              аутентификационного состояния пользователя.
            * Данный код важен для реализации пользовательского провайдера состояния аутентификации (CustomAuthStateProvider). 
              Он позволяет получать текущее аутентификационное состояние пользователя на основе JWT-токена, 
              сохраненного в локальном хранилище. Это важно для обеспечения безопасной аутентификации и авторизации 
              пользователей в веб-приложении на основе Blazor или ASP.NET Core.
        */
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            /*
                * Получение значения токена из локального хранилища.
                * Значение токена извлекается из локального хранилища с помощью _localStorageService, 
                  который, является сервисом для работы с локальным хранилищем.
            */
            string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

            /*
                * Создание нового объекта ClaimsIdentity.
                * Создается пустой объект ClaimsIdentity, который будет заполнен утверждениями 
                  из JWT-токена, если он существует и валиден.
            */
            var identity = new ClaimsIdentity();

            /*
                * Очистка заголовков аутентификации
                * Заголовки аутентификации _http.DefaultRequestHeaders.Authorization устанавливаются 
                  в значение null для HTTP-запросов. Это гарантирует, что предыдущие заголовки аутентификации очищены.
            */
            _http.DefaultRequestHeaders.Authorization = null;

            /*
                * Проверка наличия значения токена.
                * Производится проверка наличия значения токена. Если значение токена существует и не пустое, 
                  выполняется следующий код для аутентификации пользователя.
            */
            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    /*
                        * Создание объекта ClaimsIdentity на основе утверждений из JWT-токена.
                        * Метод ParseClaimsFromJwt разбирает JWT-токен и возвращает коллекцию утверждений 
                          (IEnumerable<Claim>), на основе которой создается объект ClaimsIdentity. 
                          Также указывается тип аутентификации "jwt".
                    */
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");

                    /*
                        * Установка заголовка аутентификации для HTTP-запросов.
                        * Устанавливается заголовок аутентификации для HTTP-запросов с типом аутентификации "Bearer" 
                          и значением токена. Значение токена очищается от лишних кавычек 
                          (authToken.Replace("\"", "")), если они присутствуют.
                    */
                    _http.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
                }
                catch
                {
                    /*
                        * В случае ошибки удаление токена из локального хранилища и создание пустого ClaimsIdentity.
                        * В случае возникновения ошибки при разборе JWT-токена или его невалидности, 
                          токен удаляется из локального хранилища (_localStorageService). 
                          Затем ClaimsIdentity устанавливается в пустое значение.
                    */
                    await _localStorageService.RemoveItemAsync("authToken");
                    identity = new ClaimsIdentity();
                }
            }
            /*
                * Создается объект ClaimsPrincipal на основе созданного ClaimsIdentity. 
                  ClaimsPrincipal представляет аутентифицированного пользователя.
            */
            var user = new ClaimsPrincipal(identity);

            /*
                * Создается объект AuthenticationState на основе созданного ClaimsPrincipal. 
                  AuthenticationState представляет текущее аутентификационное состояние.
            */
            var state = new AuthenticationState(user);

            /*
                * Метод NotifyAuthenticationStateChanged уведомляет систему о изменении аутентификационного состояния. 
                  Здесь состояние аутентификации устанавливается для дальнейшего использования в приложении.
            */
            NotifyAuthenticationStateChanged(Task.FromResult(state));

            /*
                * Возвращение объекта AuthenticationState
                * 
            */
            return state;
        }

        /*
            * Метод используется для парсинга JWT-токена и извлечения утверждений из его тела. 
              Утверждения представляют собой пары ключ-значение, которые используются для представления 
              информации об аутентификации и авторизации пользователей.
        */
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            // Разделение JWT-токена jwt на части с помощью метода Split('.'). Затем выбирается вторая часть,
            // содержащая тело токена (payload).
            var payload = jwt.Split('.')[1];

            // Вызов метода ParseBase64WithoutPadding для парсинга Base64-кодированного значения тела токена (payload),
            // чтобы получить массив байтов.
            var jsonBytes = ParseBase64WithoutPadding(payload);

            // Десериализация массива байтов (jsonBytes) в объект Dictionary<string, object> с помощью
            // System.Text.Json.JsonSerializer. Объект Dictionary будет содержать ключи и значения,
            // представляющие утверждения из тела токена.
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            // Создание коллекции утверждений (IEnumerable<Claim>) на основе ключей и значений из keyValuePairs
            // с помощью метода Select. Каждая пара ключ-значение преобразуется в объект Claim.
            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            // Возвращение коллекции утверждений.
            return claims;
        }


        /*
            * Метод выполняет обработку строки base64, чтобы она соответствовала ожидаемой длине при декодировании 
              из Base64. Это необходимо для дальнейшего парсинга и получения утверждений из JWT-токена.
        */
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            // Вычисление остатка от деления длины строки base64 на 4 с помощью оператора %.
            switch (base64.Length % 4)
            {
                // Если остаток равен 2, значит строка base64 требует двух символов заполнения ==. Поэтому добавляем == к строке base64.
                case 2: base64 += "=="; break;
                // Если остаток равен 3, значит строка base64 требует одного символа заполнения =. Поэтому добавляем = к строке base64.
                case 3: base64 += "="; break;
            }
            // Преобразование строки base64 в массив байтов с помощью метода Convert.FromBase64String.
            return Convert.FromBase64String(base64);
        }
    }
}
