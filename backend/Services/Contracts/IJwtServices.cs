using System.IdentityModel.Tokens.Jwt;
using SignlR_Web_ApI.DTOs;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Repository;

public interface IJwtServices
{
    Task <JwtSecurityToken> GenerateToken(User user);
    string GenerateRefreshToken();
}