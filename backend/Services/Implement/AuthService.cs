using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SignlR_Web_ApI.DTOs;
using SignlR_Web_ApI.Models;
namespace SignlR_Web_ApI.Services.Contracts;

public class AuthService : IAuthService
{
    private readonly UserManager<User> userManager;
    private readonly IConfiguration configuration;
    private readonly IJwtServices jwtServices;
    public AuthService(UserManager<User> userManager, IConfiguration configuration,IJwtServices jwtServices) {
        this.userManager = userManager;
        this.configuration = configuration;
        this.jwtServices = jwtServices;
    }
    public async Task<AuthModel> RegisterAsync(RegisterDto model)
    {
        try
        {
            if(await userManager.FindByEmailAsync(model.EmailAddress) is not null)
                return new AuthModel {  Message = "Email already exists." };
            if (await userManager.FindByNameAsync(model.UserName) is not null)
                return new AuthModel { Message = "Username already exists." };

            var user = new User { UserName = model.UserName, Email = model.EmailAddress, PasswordHash = model.Password };
            var result = await userManager.CreateAsync(user, model.Password);
            if(!result.Succeeded)
            {
                StringBuilder errorMessage = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errorMessage.Append(error.Description);
                    errorMessage.Append(" ");
                }
                return new AuthModel { Message = errorMessage.ToString() };

            }

            var token = await jwtServices.GenerateToken(user);
            return new AuthModel
            {
                Message = "Registration Succesfful",
                Email = model.EmailAddress,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiredOn = token.ValidTo,
                Username = model.UserName,
                IsAuthenticated = true,
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}