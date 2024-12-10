using Microsoft.AspNetCore.Identity;

namespace SignlR_Web_ApI.Models;

public class User : IdentityUser
{
    public ICollection<Connection> UserConnections = new List<Connection>();
	public ICollection<UserGroup> UserGroups = new List<UserGroup>();
	public ICollection<Message> Messages = new List<Message>();
	public ICollection<PrivateMessage> SentPrivateMessages = new List<PrivateMessage>();
	public ICollection<PrivateMessage> ReceivedPrivateMessages = new List<PrivateMessage>();
	public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}