using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.API.Infrastructure.Middlewares
{
    public interface IActivityTracker
    {
        void EndpointVissited(string route);
    }
}
