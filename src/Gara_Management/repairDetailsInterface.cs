using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management
{
    public interface repairDetailInterface
    {
        void ReceivedData(string idCom, decimal price, int quantity, decimal wage);
    }
}
