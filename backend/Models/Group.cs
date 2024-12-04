namespace SignlR_Web_ApI.Models;

public class Group
{
    public int Id { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}