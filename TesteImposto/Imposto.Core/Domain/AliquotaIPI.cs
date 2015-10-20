using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
     public class AliquotaIPI
    {
        public double CalcularAliquotaIPI(bool brind)
        {
            if (brind)
            {
                return 0;
            }
            else
            {
                return 0.1;
            }
         }
 
    }
}
