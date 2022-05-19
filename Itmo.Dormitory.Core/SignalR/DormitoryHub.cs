using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Core.SignalR
{
    public class DormitoryHub : Hub
    {
        protected IHubContext<DormitoryHub> _context;

        public DormitoryHub(IHubContext<DormitoryHub> context)
        {
            this._context = context;
        }

        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Announcement", message);
        }
    }
}
