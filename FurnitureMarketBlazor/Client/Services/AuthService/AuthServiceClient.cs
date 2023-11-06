namespace FurnitureMarketBlazor.Client.Services.AuthService
{
    public class AuthServiceClient : IAuthServiceClient
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthServiceClient(HttpClient http, AuthenticationStateProvider authStateProvider) =>
            (_http, _authStateProvider) = (http, authStateProvider);

        /*
               * Выполняется асинхронный запрос к провайдеру состояния аутентификации (_authStateProvider)
                 с помощью метода GetAuthenticationStateAsync(), чтобы получить текущее состояние аутентификации
                 - var authenticationState = await _authStateProvider.GetAuthenticationStateAsync();

               * Возвращается значение, указывающее, аутентифицирован ли пользователь, 
                 используя свойство Identity.IsAuthenticated, которое возвращает true, если идентификация 
                 пользователя прошла успешно, иначе возвращает false 
                 - return authenticationState.User.Identity.IsAuthenticated;
           */
        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/change-password", request.Password);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
    }
}
