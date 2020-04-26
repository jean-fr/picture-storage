using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FP.WebApp
{
    public class ImageHub : Hub
    {
        public async Task SendImage(string imageUrl, string imageTitle)
        {
            await Clients.All.SendAsync("ReceiveMessage", imageUrl, imageTitle);
        }
    }
}
