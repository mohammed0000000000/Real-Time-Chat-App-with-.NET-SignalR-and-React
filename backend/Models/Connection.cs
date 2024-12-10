namespace SignlR_Web_ApI.Models;

public class Connection
{
    public string ConnectionId { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;
    public User User { get; set; }

    public DateTime ConnectedAt { get; set; }
}