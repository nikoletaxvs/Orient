using Microsoft.AspNetCore.SignalR;
using Orient.Interfaces;
using Orient.Models;
using Orient.Data;
using Orient.Controllers;
using Orient.BasicChat;
using System.Threading.Tasks;
namespace Orient.Hubs
{
    public class ChatHub : Hub
    {
        public readonly IChatAnswer _c;
        public ChatHub(IChatAnswer c)
        {
            _c = c;
        }
        
        public async Task SendMessage( string user,string message)
        {
           
            _c.AddAnswer(new chatAnswer() { Message = message, Name = user, Date = DateTime.Now.ToString() });

            await Clients.All.SendAsync("ReceiveMessage", user, message);

        }
       
    }
}

