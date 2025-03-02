using Microsoft.AspNetCore.SignalR;

namespace VRBackend.Hubs;

public class QuizHub : Hub
{
    // This method can be expanded for client-to-server interactions if needed.
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
