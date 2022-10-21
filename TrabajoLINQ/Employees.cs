using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoLINQ
{
    public partial class Employees
    {
        public string FullName()
        {
            return FirstName + " " + LastName;
        }
    }
}
