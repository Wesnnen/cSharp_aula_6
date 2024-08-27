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

namespace aula8
{
    public partial class Form4 : Form
    {
        private string UsuarioID;
        public Form4()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

            clienteHelper.view(listViewClientes);

            // Carrega os clientes na ListView
            clienteHelper.xx(listViewClientes);
            comboBox1.Items.Add("Ativo");
            comboBox1.Items.Add("Inativo");

        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Verifica se há algum item selecionado
            if (listViewClientes.SelectedItems.Count > 0)
            {
                // Pega o item selecionado
                ListViewItem itemSelecionado = listViewClientes.SelectedItems[0];

                // Preenche os campos de texto e o ComboBox com os dados do item selecionado
                UsuarioID = itemSelecionado.SubItems[0].Text;  // Armazena o ID do usuário
                textBox1.Text = itemSelecionado.SubItems[1].Text;  // Nome
                textBox2.Text = itemSelecionado.SubItems[3].Text;  // idade
                textBox3.Text = itemSelecionado.SubItems[5].Text;  // salario
                textBox4.Text = itemSelecionado.SubItems[2].Text;  // Email
                comboBox1.Text = itemSelecionado.SubItems[4].Text;  // Status
            }
        }



        // Método para atualizar o usuário no banco de dados
        private void AtualizarUsuario(string UsuarioID, string nome, string salario, string idade, string email, string status)
        {
            string strConexao = "server=localhost;uid=root;database=bancodedados1";
            MySqlConnection conexao = new MySqlConnection(strConexao);

            try
            {
                conexao.Open();

                // Query para atualizar os dados na tabela 'usuarios' (exceto Salário)
                string queryAtualizarUsuario = @"
     UPDATE usuario SET 
         Nome = @Nome, 
         Idade = @Idade, 
         Email = @Email, 
         Status = @Status 
     WHERE UsuarioID = @UsuarioID";

                MySqlCommand cmdAtualizarUsuario = new MySqlCommand(queryAtualizarUsuario, conexao);
                cmdAtualizarUsuario.Parameters.AddWithValue("@Nome", nome);
                cmdAtualizarUsuario.Parameters.AddWithValue("@Idade", idade);
                cmdAtualizarUsuario.Parameters.AddWithValue("@Email", email);
                cmdAtualizarUsuario.Parameters.AddWithValue("@Status", status);
                cmdAtualizarUsuario.Parameters.AddWithValue("@UsuarioID", UsuarioID);

                cmdAtualizarUsuario.ExecuteNonQuery();

                // Atualiza o Salário na tabela 'usuarioperfil'
                string queryAtualizarSalario = @"
     UPDATE usuarioperfil SET 
         salario = @Salario 
     WHERE PerfilID = @UsuarioID";  // Assumindo que PerfilID é o mesmo que UsuarioID

                MySqlCommand cmdAtualizarSalario = new MySqlCommand(queryAtualizarSalario, conexao);
                cmdAtualizarSalario.Parameters.AddWithValue("@Salario", salario);
                cmdAtualizarSalario.Parameters.AddWithValue("@UsuarioID", UsuarioID);

                cmdAtualizarSalario.ExecuteNonQuery();

                MessageBox.Show("Dados atualizados com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o usuário: {ex.Message}");
            }
            finally
            {
                conexao.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsuarioID))
            {
                MessageBox.Show("Selecione um usuário na lista.");
                return;
            }

            // Atualiza os dados do usuário no banco de dados
            AtualizarUsuario(
                UsuarioID,
                textBox1.Text,  // Nome
                textBox3.Text,  // Salário
            textBox2.Text,  // Idade
                textBox4.Text,  // Email
                comboBox1.Text  // Status
            );

            // Atualiza a lista de clientes
            clienteHelper.xx(listViewClientes);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

