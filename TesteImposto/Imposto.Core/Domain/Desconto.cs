﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Desconto
    {
        public double CalcularDesconto(NotaFiscal NF)
        {
            if (NF.EstadoDestino == "SP" || NF.EstadoDestino == "RJ" || NF.EstadoDestino == "ES" || NF.EstadoDestino == "MG")
            {
                return 0.1;
            }
            else
            {
                return 1;
            }
         }
    }
}
