using Microsoft.AspNetCore.SignalR;
namespace Orient.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string user,string message,string time)
        {
            Clients.All.SendAsync("ReceiveMessage", user, message,DateTime.Now);
        }
    }
}
