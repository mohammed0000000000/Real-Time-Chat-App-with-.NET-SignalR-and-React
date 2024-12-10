using Microsoft.EntityFrameworkCore;

namespace SignlR_Web_ApI.Models;

[Owned]
public class RefreshToken
{
    public string Token { get; set; }
    public DateTime ExpiresOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? RevokedOn { get; set; }
    
    public bool isExpired => DateTime.UtcNow >= ExpiresOn;
    public bool isActive => RevokedOn == null && !isExpired;
}