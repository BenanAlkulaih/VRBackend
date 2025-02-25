using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace VRBackend.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(ILogger<ChatHub> logger)
    {
        _logger = logger;
    }

    public async Task SendMessage(string user, string message)
    {
        _logger.LogInformation("Received message from {User}: {Message}", user, message);
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
