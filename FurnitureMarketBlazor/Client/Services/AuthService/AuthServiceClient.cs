namespace FurnitureMarketBlazor.Client.Services.AuthService
{
    public class AuthServiceClient : IAuthServiceClient
    {
        private readonly HttpClient _http;

        public AuthServiceClient(HttpClient http) => _http = http;

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
