using Microsoft.AspNetCore.SignalR;

namespace SignalR.Services
{
    public class ChatHub:Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }



        public async Task SendMessage(string Message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Message);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Disconnected ! ");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
