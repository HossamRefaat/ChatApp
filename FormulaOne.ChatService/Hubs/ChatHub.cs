using FormulaOne.ChatService.DataService;
using FormulaOne.ChatService.Model;
using Microsoft.AspNetCore.SignalR;

namespace FormulaOne.ChatService.Hubs;

public class ChatHub : Hub
{

    private readonly SharedDb _shared;

    public ChatHub(SharedDb shared) => _shared = shared;

    public async Task JoinRoom(UserConnection conn)
    {
        await Clients.All.SendAsync("ReceiveMessage", $"admin", $"{conn.UserName} has joined the room");
    }

    public async Task JoinSpecificChatRoom(UserConnection conn)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);
        _shared.Connections[Context.ConnectionId] = conn;
        await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", $"admin", $"{conn.UserName} has joined the room {conn.ChatRoom}");
    }

    public async Task SendMessage(string msg)
    {
        if(_shared.Connections.TryGetValue(Context.ConnectionId, out UserConnection conn))
        {
            await Clients.Group(conn.ChatRoom)
                .SendAsync("ReceiveSpecificMessage", conn.UserName, msg);
        }
    }
}
