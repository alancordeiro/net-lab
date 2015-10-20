using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Imposto.Core.Domain;
using Imposto.Core.Data;

namespace Imposto.Core.XML
{
    public class XML
    {
        


        public XmlDocument GetXML(string query, string nodeName = "root", string dataSetName = "NewDataSet")
        {
            /*GenericConnection.SetarQuery(query.ToString(), true);
            GenericConnection.Executar();

            DataTable tabela = GenericConnection.PegarTabela();
            tabela.TableName = nodeName;
            tabela.DataSet.DataSetName = dataSetName;

            foreach (DataColumn col in tabela.Columns)
                col.ColumnName = col.ColumnName.ToLower();

            StringBuilder sb = new StringBuilder();
            StringWriter writer = new StringWriter(sb);

            tabela.WriteXml(writer);
            */
            XmlDocument doc = new XmlDocument();
            //doc.LoadXml(writer.ToString().Replace(" xml:space=\"preserve\"", null));

            return doc;
        }

        public void InserirDadosPedido(XmlNode doc)
        {
            try
            {
         /*       GenericConnection.Iniciar_Transacao(true);
                XmlNode noPrincipal = doc.SelectNodes("/root/data/gsimoesPedido")[0];
                String cdcotacao = "";
                StringBuilder valorRetorno = new StringBuilder();
                logErro = new StringBuilder();
                logErro.AppendLine("------------------------------------------------------------------");
                logErro.AppendLine("Iniciando baixa do pedido");

                valorRetorno.AppendLine(QueryToXML.ChamarPckEnviarCotacao(noPrincipal, out cdcotacao));
                //Iteração para enviar os itens do pedido
                foreach (XmlElement itemPedido in doc.SelectNodes("/root/data/gsimoesPedido/itens_pedidos/gsimoesItemPedido"))
                {
                    valorRetorno.AppendLine(QueryToXML.ChamarPckEnviarLinhaCotacao(noPrincipal, itemPedido, cdcotacao));
                    valorRetorno.AppendLine(QueryToXML.ChamarPckEnviarLinhaCotacaoRC(noPrincipal, itemPedido, cdcotacao));
                }
                //Iteração para enviar as condições de pagamento
                foreach (XmlElement itemCondPag in doc.SelectNodes("/root/data/gsimoesPedido/condicoes_pagamentos/gsimoesCondicaoPagamento"))
                {
                    valorRetorno.AppendLine(QueryToXML.ChamarPckEnviarCondicoesPagamento(noPrincipal, itemCondPag, cdcotacao));
                }

                valorRetorno.AppendLine(QueryToXML.ChamarPckEnviarFinalizarCotacao(noPrincipal, cdcotacao));

                //Verifica se ocorreu algum erro na execução das procedures.
                if (!String.IsNullOrWhiteSpace(valorRetorno.ToString()))
                {
                    throw new Exception(valorRetorno.ToString());
                }
                logErro.AppendLine("Pedido gravado com sucesso.");*/
            }
            catch (Exception ex)
            {
                //logErro.AppendLine("Critica na gravacao do pedido.\n" + ex.ToString().Trim());
                //throw ex;
            }
            finally
            {
                //logErro.AppendLine("---------------------------------------------------------------------------");
                //logErro.AppendLine("\n");
                //Integracao.GravarLog(logErro.ToString());
            }
        }
    }
}
