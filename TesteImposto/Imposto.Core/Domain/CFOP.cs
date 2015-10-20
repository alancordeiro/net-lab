using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imposto.Core.Domain
{
    public class CFOP
    {
        public string CalcularCFOP(NotaFiscal NF)
        {
            if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "RJ"))
            {
                return "6.000";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "PE"))
            {
                return "6.001";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "MG"))
            {
                return "6.002";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "PB"))
            {
                return "6.003";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "PR"))
            {
                return "6.004";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "PI"))
            {
                return "6.005";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "RO"))
            {
                return "6.006";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "SE"))
            {
                return "6.007";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "TO"))
            {
                return "6.008";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "SE"))
            {
                return "6.009";
            }
            else if ((NF.EstadoOrigem == "SP") && (NF.EstadoDestino == "PA"))
            {
                return "6.010";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "RJ"))
            {
                return "6.000";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "PE"))
            {
                return "6.001";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "MG"))
            {
                return "6.002";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "PB"))
            {
                return "6.003";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "PR"))
            {
                return "6.004";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "PI"))
            {
                return "6.005";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "RO"))
            {
                return "6.006";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "SE"))
            {
                return "6.007";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "TO"))
            {
                return "6.008";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "SE"))
            {
                return "6.009";
            }
            else if ((NF.EstadoOrigem == "MG") && (NF.EstadoDestino == "PA"))
            {
                return "6.010";
            }
            else
            {
                //Se não for previsto, considero o CFOP mais alto
                return "6.010";
            }
        }
    }
}
