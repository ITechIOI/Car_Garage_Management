using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gara_Management
{
    public interface grnInterface
    {
        void ReceivedData(string idCom, int price, int quantity);
    }
}
