using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Domain.Entities
{
    public class Counter
    {
        public Guid Id { get; set; }
        public string Route { get; set; }
        public int CurrentCount { get; set; } = 0;
    }
}
