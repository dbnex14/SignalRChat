using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    /*
     * Create and use hubs by ingeriting from Hub and adding public methods that are callable by clients.
     * These can also have return a value or complex type such as types and arrays as any C# method.
     * SignalR will handle serialization/deserialization for you.
     * Dont store state in a property on Hub as Hubs are transient and every hub method call is executed
     * on a new hub instance.  Also, use await when calling async methods that depend on hub staying alive.
     * Such methods can fail if called without await and hub method completes before async method completes.
     * Hub class has the Context property with ConnectionId, UserIdentifier, User, Items, HttpContext etc.
     * It also has Clients property containing properties for communication btw server and client.  Below,
     * it uses All to send message to all connected clients but it can send also message to specific clients,
     * groups, users.
     * This class represents the server.
     */
    public class ChatHub : Hub
    {
        // sends message to all clients
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        // sends message back to the caller, not used in this example
        public async Task SendMessageToCaller(string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        // sends message to all clients in the Users group, not used in this example
        public async Task SendMessageToGroups(string message)
        {
            await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", message);
        }
    }
}
