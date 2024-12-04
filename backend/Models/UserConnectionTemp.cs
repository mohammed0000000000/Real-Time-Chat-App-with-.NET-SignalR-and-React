using System.ComponentModel.DataAnnotations.Schema;

namespace SignlR_Web_ApI.Models;

[NotMapped]
public class UserConnectionTemp
{
    public string Username { get; set; } = string.Empty;
    public string ChatRoom { get; set; } = string.Empty;
}

