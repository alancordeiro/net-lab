using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Imposto.Core.Domain
{
    [Serializable]
    [XmlRoot]
    public class NotaFiscal
    {
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        [XmlArrayItem]
        public List<NotaFiscalItem> ItensDaNotaFiscal { get; private set; }

        public NotaFiscal()
        {
            this.ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }

        public NotaFiscal EmitirNotaFiscal(Pedido pedido)
        {
                       
            this.NumeroNotaFiscal = new Random().Next(int.MaxValue); 
            this.Serie = new Random().Next(Int32.MaxValue);
            this.NomeCliente = pedido.NomeCliente;

            this.EstadoOrigem = pedido.EstadoOrigem;
            this.EstadoDestino = pedido.EstadoDestino;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                CFOP cfop = new CFOP();

                notaFiscalItem.Cfop = cfop.CalcularCFOP(this);

                if (this.EstadoDestino == this.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }
                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido*0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = itemPedido.ValorItemPedido;
                }
                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms*notaFiscalItem.AliquotaIcms;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;                   
                    
                }

                AliquotaIPI Aliq = new AliquotaIPI();
                notaFiscalItem.AliquotaIPI = Aliq.CalcularAliquotaIPI(itemPedido.Brinde);
                notaFiscalItem.BaseIPI = itemPedido.ValorItemPedido;
                notaFiscalItem.ValorIPI = notaFiscalItem.BaseIPI * notaFiscalItem.AliquotaIPI;

                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;
                notaFiscalItem.IdNotaFiscal = this.NumeroNotaFiscal;

                Desconto Desc = new Desconto();
                notaFiscalItem.Desconto = Desc.CalcularDesconto(this) * itemPedido.ValorItemPedido;

                this.ItensDaNotaFiscal.Add(notaFiscalItem);                
            }

            
            return this;
        }

    }
}
