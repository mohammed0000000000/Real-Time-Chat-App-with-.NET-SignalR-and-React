namespace SignlR_Web_ApI.Models;

public class UserGroup
{
    public string UserId { get; set; } = string.Empty;
    public User User { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public DateTime JoinAt { get; set; }
}