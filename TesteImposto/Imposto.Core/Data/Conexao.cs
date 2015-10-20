using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Imposto.Core.Data
{
    public class Conexao
    {
        private static string connString = @"server = .\sqlexpress;
    Database = Imposto;
    integrated security = true;";
            
    private static SqlConnection conn = null;   

    
    public static SqlConnection obterConexao(){
    
      conn = new SqlConnection(connString);
            
      try{
        
        conn.Open();
      }
      catch(SqlException sqle){
        conn = null;
        
      }

      return conn;
    }

    public static void fecharConexao(){
      if(conn != null){
        conn.Close();
      }
    }
  }
}