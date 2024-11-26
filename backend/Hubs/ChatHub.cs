using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Hubs;

public class ChatHub : Hub
{
    public async Task JoinChat(UserConnection conn)
    {
        await Clients.All.SendAsync("Receive Message", "Admin", $"{conn.Username} has joined");
        
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "Admin", $"{conn.Username} has joined the chat room");
    }
}