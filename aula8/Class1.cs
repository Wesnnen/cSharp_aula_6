using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aula8
{
    public class clienteHelper
    {
        public static void CarregarClientes()
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
        }
    }
}
