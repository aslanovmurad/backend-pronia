using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AP204_Pronia.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string  name,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",name,message);
        }
    }
}
