using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Web.SignalR
{
    public class DormitoryHub : Hub
    {
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Announcement", message);
        }
    }
}
