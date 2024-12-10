using SignlR_Web_ApI.DTOs;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Services.Contracts;

public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegisterDto model);
    Task<AuthModel> LoginAsync(LoginDto login);
    Task<AuthModel> RefreshTokenAsync(string token);
}