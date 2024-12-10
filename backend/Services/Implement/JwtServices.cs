using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SignlR_Web_ApI.Helper;
using SignlR_Web_ApI.Models;
namespace SignlR_Web_ApI.Services.Contracts;

public class JwtServices:IJwtServices
{
    private readonly JWT _jwt;
    private readonly UserManager<User> userManager;
    public JwtServices(IOptions<JWT> jwt, UserManager<User> userManager)
    {
        _jwt = jwt.Value;
        this.userManager = userManager;
        Console.WriteLine(jwt.Value);
    }

    public async Task <JwtSecurityToken> GenerateToken(User user)
    {
        // Step 2: Create user claims
        var userClaims = new List<Claim>();
        userClaims.AddRange([
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        ]);
    
        var userRoles = await userManager.GetRolesAsync(user);

        // Step 3: Add role-based claims
        if (userRoles?.Any() == true) {
            userClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());
        }

        List<Claim> claims = new List<Claim>();
        
        // Add cliams user claims
       claims.AddRange([
         new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         new Claim(JwtRegisteredClaimNames.Email, user.Email),
       ]);
       claims.Union(userClaims);
       
        // Create a new JWT Token
        Console.WriteLine("Error when generate token" + _jwt.SecrityKey);
       var signInkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecrityKey));
       var signInCredentialList = new SigningCredentials(signInkey, SecurityAlgorithms.HmacSha256);
       var tokenDescriptor = new JwtSecurityToken(
           issuer: _jwt.IssuerIp,
           audience: _jwt.AudienceIP,
           claims: claims,
           expires: DateTime.Now.AddMinutes(_jwt.AccessTokenExpired),
           signingCredentials: signInCredentialList
           ); 
       
       return tokenDescriptor;
    }

    public string GenerateRefreshToken()
    {
        // return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return Guid.NewGuid().ToString();   
    }
}