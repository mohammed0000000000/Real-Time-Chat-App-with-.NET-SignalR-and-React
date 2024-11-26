using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using SignlR_Web_ApI.DataServices;
using SignlR_Web_ApI.Models;

namespace SignlR_Web_ApI.Hubs;

public class ChatHub : Hub
{
    
    private readonly ShardDb _shardDb;
    
    public ChatHub(ShardDb shardDb)
    {
        _shardDb = shardDb;
    }
    public async Task JoinChat(UserConnection conn)
    {
        await Clients.All.SendAsync("ReceiveMessage", "Admin", $"{conn.Username} has joined");
        
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        _shardDb.connections[Context.ConnectionId] = conn;
        await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "Admin", $"{conn.Username} has joined the chat room");
    }

    public async Task SendMessage(string message)
    {
        if (_shardDb.connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            await Clients.Group(conn.ChatRoom).SendAsync("RecieveSpacificMessage", conn.Username, message);
        }
    }
}