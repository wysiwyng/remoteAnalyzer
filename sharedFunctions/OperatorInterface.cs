using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sharedFunctions
{
    public interface IOperator
    {
        int getID();
        int getUID();
        DateTime getLastOnlineTime();
        int getPermissions();
    }
}
