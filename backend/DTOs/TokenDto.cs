using System.Security.Claims;

namespace SignlR_Web_ApI.DTOs;

public class TokenDto
{
    public string userId { get; set; } 
    public List<Claim> claims { get; set; } = new List<Claim>();

}