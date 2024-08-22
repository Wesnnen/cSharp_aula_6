using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace aula8
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            var strConexao = "server=localhost;uid=root;database=bancodedados1";
            var conexao = new MySqlConnection(strConexao);
            conexao.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string nome = textBox1.Text;
            string email = textBox2.Text;
            string salario = textBox3.Text;
            string dataCriacao = DateTime.Now.ToString("yyyy-MM-dd");
            string status = "Ativo";


            string strConexao = "server=localhost;uid=root;database=bancodedados1";
            MySqlConnection conexao = new MySqlConnection(strConexao);

            try
            {
                conexao.Open();

                // Query SQL para inserção de dados na tabela 'usuarios'





                string query = $"INSERT INTO usuario (nome, email, DataCriacao, Status)  VALUES ('{nome}', '{email}', '{dataCriacao}', '{status}');";


                MySqlCommand cmd = new MySqlCommand(query, conexao);


                // Executa o comando de inserção
                int linhasAfetadas = cmd.ExecuteNonQuery();

                if (linhasAfetadas > 0)
                {
                    //pega a ultima posição valor da tabela.
                    long usuarioID = cmd.LastInsertedId;
                    string query2 = $"INSERT INTO usuarioperfil (salario, PerfilID)  \r\n values('{salario}',{usuarioID} );";
                    MySqlCommand cmd2 = new MySqlCommand(query2, conexao);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Dados inseridos com sucesso!");
                    // Após inserção, atualiza a ListView
                    clienteHelper.CarregarClientes();

                }
                else
                {
                    MessageBox.Show("Falha ao inserir dados.");
                }

                conexao.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {



            clienteHelper.CarregarClientes();
        }
        /*private void CarregarClientes()
        {
            var strConexao = "server=localhost;uid=root;database=bancodedados1";
            MySqlConnection conexao = new MySqlConnection(strConexao);

            try
            {
                conexao.Open();

                // Query SQL para selecionar todos os registros da tabela 'usuario'
                string query = "Select usuario.nome, usuario.Email,usuario.UsuarioID, usuario.status, usuario.DataCriacao, usuarioperfil.salario\r\nfrom usuario\r\njoin usuarioperfil ON usuario.UsuarioID = usuarioperfil.PerfilID ";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                conexao.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar usuarioss: {ex.Message}");
            }
        }*/

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}


