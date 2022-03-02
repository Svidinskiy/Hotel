using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel
{
    public partial class Rooms
    {
        public override string ToString()
        {
            return Number.ToString(); //+ " ꟷ " + Status.ToString();
        }
    }
    public partial class Clients
    {
        public override string ToString()
        {
            return FIO;

        }
    }
    public partial class Housemaid
    {
        public override string ToString()
        {
            return FIO;

        }
    }
}
