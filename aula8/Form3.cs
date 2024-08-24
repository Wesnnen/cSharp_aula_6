﻿using MySql.Data.MySqlClient;
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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            listViewClientes.View = View.Details;
            listViewClientes.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Email", 200, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Data da Criação", 200, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Status", 200, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Salario", 200, HorizontalAlignment.Left);
            listViewClientes.FullRowSelect = true; // Ativa a seleção da linha toda
            listViewClientes.GridLines = true; // Adiciona linhas de grade para melhor visualização

            // Carrega os clientes na ListView
            
        }
        private void CarregarClientes(string x)
        {
            var strConexao = "server=localhost;uid=root;database=bancodedados1";
            MySqlConnection conexao = new MySqlConnection(strConexao);

            try
            {
                conexao.Open();

                // Query SQL para selecionar todos os registros da tabela 'usuario'
                string query = "Select usuario.nome, usuario.Email,usuario.UsuarioID, usuario.status, usuario.DataCriacao, usuarioperfil.salario\r\nfrom usuario\r\njoin usuarioperfil ON usuario.UsuarioID = usuarioperfil.PerfilID\r\nWhere usuario.nome ='" + x + "'";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Limpa os itens existentes no ListView antes de recarregar
                listViewClientes.Items.Clear();

                // Itera sobre os dados e os adiciona ao ListView
                while (reader.Read())
                {
                    // Adiciona os itens ao ListView
                    ListViewItem item = new ListViewItem(reader["UsuarioID"].ToString());
                    item.SubItems.Add(reader["nome"].ToString());
                    item.SubItems.Add(reader["email"].ToString());
                    item.SubItems.Add(reader["DataCriacao"].ToString());
                    item.SubItems.Add(reader["status"].ToString());
                    item.SubItems.Add(reader["salario"].ToString());
                    listViewClientes.Items.Add(item);
                }

                reader.Close();
                conexao.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar usuarioss: {ex.Message}");
            }
        }
        private void listViewClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClientes.SelectedItems.Count > 0)
            {
                MessageBox.Show("Código do produto escolhido: " + listViewClientes.SelectedItems[0].Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
