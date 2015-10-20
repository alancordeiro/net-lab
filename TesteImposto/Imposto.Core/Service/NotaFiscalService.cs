using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Imposto.Core.Domain;
using Imposto.Core.Data;
using System.Windows.Forms;



namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Domain.Pedido pedido) 
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            //notaFiscal.ItensDaNotaFiscal = new List<NotaFiscalItem>();

            notaFiscal = notaFiscal.EmitirNotaFiscal(pedido);
            if (GerarXML(notaFiscal))
            {
                PersistirBD(notaFiscal);
                pedido.ItensDoPedido.Clear();
            }
        }

        public SqlDataReader ValorPorCFOP(SqlConnection conex) 
        {
            SqlCommand cmd = new SqlCommand("P_CFOP", conex);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader;

            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                return reader;
            }
            return null;
              
        }
        
        //Poderia fazer o método ser para qualquer classe usando como paramêtro "T obj" ao invés de fixar à classe NotaFiscal

        public bool GerarXML(NotaFiscal NF)
            {
                try
                {
                    string dirxml = LerXML("CaminhoXML");
                   string caminho = dirxml+"\\NF"+NF.NumeroNotaFiscal+".XML";

                    FileStream stream = new FileStream(caminho, FileMode.Create);
                    XmlSerializer serializador = new XmlSerializer(typeof(NotaFiscal));
                    serializador.Serialize(stream, NF);
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public string LerXML(string parametro)
        {

            string dir = Directory.GetCurrentDirectory();
            dir = dir.Remove(dir.LastIndexOf("\\"));
            dir = dir.Remove(dir.LastIndexOf("\\"));
            dir = dir.Remove(dir.LastIndexOf("\\"));
            XmlTextReader x = new XmlTextReader(dir+"\\Parametros.xml");

            while (x.Read())
            {
                if (x.NodeType == XmlNodeType.Element && x.Name == parametro)
                    return (x.ReadString());
            }

            x.Close();
            return "";
        }

        public void PersistirBD(NotaFiscal NF)
        {
          SqlConnection conn = Conexao.obterConexao();

          try
          {        
            conn.Open();
          }
          catch
          {
            MessageBox.Show("Erro ao tentar conectar com o banco de dados");
          }
          
          SqlCommand cmd = new SqlCommand("P_NOTA_FISCAL", conn);
          cmd.Parameters.AddWithValue("@pID", 0);
          cmd.Parameters.AddWithValue("@pNumeroNotaFiscal", NF.NumeroNotaFiscal); 
          cmd.Parameters.AddWithValue("@pSerie", NF.Serie); 
          cmd.Parameters.AddWithValue("@pNomeCliente", NF.NomeCliente); 
          cmd.Parameters.AddWithValue("@pEstadoDestino", NF.EstadoDestino);
          cmd.Parameters.AddWithValue("@pEstadoOrigem", NF.EstadoOrigem); 
          cmd.CommandType = CommandType.StoredProcedure; 
             
          try 
          { 
            int n = cmd.ExecuteNonQuery(); 
            //if (i > 0) MessageBox.Show("Registro incluido com sucesso!"); 
          } 
          catch (Exception ex) 
          { 
            MessageBox.Show("Erro: " + ex.ToString()); 
          } 

          try
          {
          
            foreach (NotaFiscalItem itemNF in NF.ItensDaNotaFiscal)
            {
          
              SqlCommand cmd1 = new SqlCommand("P_NOTA_FISCAL_ITEM", conn);
              cmd1.Parameters.AddWithValue("@pID", 0);  
              cmd1.Parameters.AddWithValue("@pIdNotaFiscal", itemNF.IdNotaFiscal);
              cmd1.Parameters.AddWithValue("@pCfop", itemNF.Cfop); 
              cmd1.Parameters.AddWithValue("@pTipoIcms", itemNF.TipoIcms); 
              cmd1.Parameters.AddWithValue("@pBaseIcms", itemNF.BaseIcms);
              cmd1.Parameters.AddWithValue("@pAliquotaIcms", itemNF.AliquotaIcms); 
              cmd1.Parameters.AddWithValue("@pValorIcms", itemNF.ValorIcms); 
              cmd1.Parameters.AddWithValue("@pNomeProduto", itemNF.NomeProduto); 
              cmd1.Parameters.AddWithValue("@pCodigoProduto", itemNF.CodigoProduto);
              cmd1.Parameters.AddWithValue("@pBaseIPI", itemNF.BaseIPI);
              cmd1.Parameters.AddWithValue("@pAliquotaIPI", itemNF.AliquotaIPI);
              cmd1.Parameters.AddWithValue("@pValorIPI", itemNF.ValorIPI);
              cmd1.Parameters.AddWithValue("@pDesconto", itemNF.Desconto);
              cmd1.CommandType = CommandType.StoredProcedure; 
              int i = cmd1.ExecuteNonQuery(); 
            
             }
          }
          catch (Exception ex) 
          { 
            MessageBox.Show("Erro: " + ex.ToString()); 
          } 
          finally 
          { 
            conn.Close(); 
          } 

        }
    }
}
