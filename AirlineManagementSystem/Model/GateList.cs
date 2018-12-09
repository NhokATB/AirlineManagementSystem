using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportManagerSystem.Model
{
    public class GateList:List<int>
    {
        public GateList()
        {
            this.AddRange(Enumerable.Range(1, 20));
        }
    }
}
