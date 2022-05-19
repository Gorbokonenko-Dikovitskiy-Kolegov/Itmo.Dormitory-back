using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itmo.Dormitory.Common.Exceptions
{
    public interface IBaseException
    {
        public int StatusCode { get;}
    }
}
