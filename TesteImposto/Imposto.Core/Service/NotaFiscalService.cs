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


namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Domain.Pedido pedido) 
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            //notaFiscal.ItensDaNotaFiscal = new List<NotaFiscalItem>();

            GerarXML(notaFiscal.EmitirNotaFiscal(pedido));
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
    }
}
