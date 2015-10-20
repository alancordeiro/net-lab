using Imposto.Core.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Imposto.Core.Domain;
using Imposto.Core.Data;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)));
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)));
                     
            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {            
            NotaFiscalService service = new NotaFiscalService();
            pedido.EstadoOrigem = cbEstadoOrigem.Text;
            pedido.EstadoDestino = cbEstadoDestino.Text;
            pedido.NomeCliente = textBoxNomeCliente.Text;
            
            DataTable table = (DataTable)dataGridViewPedidos.DataSource;

            foreach (DataRow row in table.Rows)
            {
                string brind = row["Brinde"].ToString();

                if (brind == "")
                {
                    brind = "false";
                }

                pedido.ItensDoPedido.Add(
                    new PedidoItem()
                    {
                        Brinde = Convert.ToBoolean(brind),
                        CodigoProduto =  row["Codigo do produto"].ToString(),
                        NomeProduto = row["Nome do produto"].ToString(),
                        ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())            
                    });
            }

            service.GerarNotaFiscal(pedido);
            MessageBox.Show("Operação efetuada com sucesso");

            textBoxNomeCliente.Text = "";
            cbEstadoOrigem.SelectedIndex = -1;
            cbEstadoDestino.SelectedIndex = -1;
            dataGridViewPedidos.DataSource = null;

            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();

            textBoxNomeCliente.Focus();
            
            //Teste da SP P_CFOP
            /* SqlConnection conn = Conexao.obterConexao();

            if (conn == null)
            {
                MessageBox.Show("Erro ao tentar conectar com o banco de dados");
            }

            SqlDataReader reader = service.ValorPorCFOP(conn);

            while (reader.Read())
            {
                MessageBox.Show(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[3].ToString());
            }   
       
            Conexao.fecharConexao();*/
        }

        private void FormImposto_Load(object sender, EventArgs e)
        {
            /*SqlConnection conn = Conexao.obterConexao();

            if (conn == null)
            {
                MessageBox.Show("Erro ao tentar conectar com o banco de dados");
            }
            else
            {
                MessageBox.Show("BD OK");
            }*/
        }

        private void FormImposto_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Conexao.fecharConexao();
        }

        private void textBoxNomeCliente_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeCliente.Text == "")
            {
                MessageBox.Show("Informe o nome do cliente!");
                textBoxNomeCliente.Focus();
            }

        }

        private void cbEstadoOrigem_Leave(object sender, EventArgs e)
        {
            if (cbEstadoOrigem.SelectedIndex == -1)
            {
                MessageBox.Show("Informe o estado de origem!");
                cbEstadoOrigem.Focus();
            }
        }

        private void cbEstadoDestino_Leave(object sender, EventArgs e)
        {
            if (cbEstadoDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Informe o estado de destino!");
                cbEstadoDestino.Focus();
            }
        }
    }
}
