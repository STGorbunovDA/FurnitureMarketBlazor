﻿namespace FurnitureMarketBlazor.Server.Services.AuthService
{
    public interface IAuthServiceServer
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
    }
}
