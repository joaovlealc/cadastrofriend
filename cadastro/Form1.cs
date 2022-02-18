using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CadastroEvento
{
    public partial class CadastroEvento : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aluno\\source\\repos\\CadastroEvento\\CadastroEvento\\DbEvento.mdf;Integrated Security=True");
        public CadastroEvento()
        {
            InitializeComponent();
        }
        public void CarregaDgv()
        {
            string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aluno\\source\\repos\\CadastroEvento\\CadastroEvento\\DbEvento.mdf;Integrated Security=True";
            string query = "SELECT * FROM evento";
            SqlConnection con = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable Evento = new DataTable();
            da.Fill(Evento);
            dgvEvento.DataSource = Evento;
            con.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaDgv();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("inserir", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
            cmd.Parameters.AddWithValue("@data_nas", SqlDbType.Date).Value = txtData_Na.Text.Trim();
            cmd.Parameters.AddWithValue("@cidade", SqlDbType.NChar).Value = txtCidade.Text.Trim();
            cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
            cmd.ExecuteNonQuery();
            CarregaDgv();
            MessageBox.Show("Pessoa cadastrada com sucesso!", "Cadastro de Pessoas");
            txtNome.Text = "";
            txtData_Na.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";
            con.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("atualizar", con);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@data_nas", SqlDbType.Date).Value = txtData_Na.Text.Trim();
                cmd.Parameters.AddWithValue("@cidade", SqlDbType.NChar).Value = txtCidade.Text.Trim();
                cmd.Parameters.AddWithValue("@celular", SqlDbType.NChar).Value = txtCelular.Text.Trim();
                cmd.ExecuteNonQuery();
                CarregaDgv();
                MessageBox.Show("Pessoa atualizada com sucesso!", "Cadastro de Pessoas");
                txtNome.Text = "";
                txtData_Na.Text = "";
                txtCidade.Text = "";
                txtCelular.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("excluir", con);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                CarregaDgv();
                MessageBox.Show("Pessoa atualizada com sucesso!", "Cadastro de Pessoas");
                txtNome.Text = "";
                txtData_Na.Text = "";
                txtCidade.Text = "";
                txtCelular.Text = "";
                con.Close();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("excluir", con);
                cmd.Parameters.AddWithValue("@nome", SqlDbType.NChar).Value = txtNome.Text.Trim();
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.Read())
                {
                    txtNome.Text = dr["nome"].ToString();
                    txtData_Na.Text = dr["data_nas"].ToString();
                    txtCidade.Text = dr["cidade"].ToString();
                    txtCelular.Text = dr["celular"].ToString();
                    con.Close();
                } else
                {
                    MessageBox.Show("Nenhum registro encontrado!");
                }
            }
            finally
            {

            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNome.Text = "";
            txtData_Na.Text = "";
            txtCidade.Text = "";
            txtCelular.Text = "";
        }

        private void dgvEvento_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvEvento.Rows[e.RowIndex];
                txtNome.Text = row.Cells[0].Value.ToString();
                txtData_Na.Text = row.Cells[1].Value.ToString();
                txtCidade.Text = row.Cells[2].Value.ToString();
                txtCelular.Text = row.Cells[3].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}