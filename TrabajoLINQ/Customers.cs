using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoLINQ
{
    public partial class Customers
    {
        public string Origin()
        {
            return City + " - " + Country;
        }
    }
}
