using SignlR_Web_ApI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace SignlR_Web_ApI.Services.Contracts
{
	public interface IJwtServices
	{
		Task<JwtSecurityToken> GenerateToken(User user);
		RefreshToken GenerateRefreshToken();
	}
}
