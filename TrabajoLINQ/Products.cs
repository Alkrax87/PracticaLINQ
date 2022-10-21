using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoLINQ
{
    public partial class Products
    {
        public decimal Total()
        {
            return Convert.ToDecimal(UnitPrice * UnitsInStock);
        }
    }
}
