using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class Desconto
    {
        public void CalcularDesconto(NotaFiscal NF)
        {
            foreach (NotaFiscalItem itemNF in NF.ItensDaNotaFiscal)
            {
                if (NF.EstadoDestino == "SP" || NF.EstadoDestino == "RJ" || NF.EstadoDestino == "ES" || NF.EstadoDestino == "MG")
                {
                    itemNF.Desconto = 0.1*itemNF.BaseIPI;
                }
            }
        }
    }
}
