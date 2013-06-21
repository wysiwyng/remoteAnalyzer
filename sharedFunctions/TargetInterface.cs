using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharedFunctions
{
    public interface ITarget
    {
        int getID();
        int getUID();
        DateTime getLastOnlineTime();
    }
}
