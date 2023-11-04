namespace FurnitureMarketBlazor.Client.Services.AuthService
{
    public interface IAuthServiceClient
    {
        Task<ServiceResponse<int>> Register(UserRegister request);
    }
}
