using Itmo.Dormitory.DataAccess;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Itmo.Dormitory.API.Infrastructure.Middlewares
{
    public class ActivityTracker : IActivityTracker
    {
        private readonly DormitoryDbContext _dormitoryDbContext;

        public ActivityTracker(DormitoryDbContext dormitoryDbContext)
        {
            _dormitoryDbContext = dormitoryDbContext;
        }

        public void EndpointVissited(string route)
        {
            var res = _dormitoryDbContext.Counters.Where(el => el.Route == route).FirstOrDefault();
            if (res == null)
            {
                res = _dormitoryDbContext.Counters.Add(new Domain.Entities.Counter
                {
                    Route = route,
                    CurrentCount = 0
                }).Entity;
            }
            
            res.CurrentCount++;
            _dormitoryDbContext.SaveChanges();
        }
    }
}
