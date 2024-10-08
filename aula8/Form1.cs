using MySql.Data.MySqlClient;

namespace aula8
{
    public partial class Form1 : Form
    {
        public Form1()
        {//pwd = senha
            InitializeComponent();
            var strConexao = "server=localhost;uid=root;database=bancodedados1";
            var conexao = new MySqlConnection(strConexao);
            conexao.Open();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            clienteHelper.view(listViewClientes);
            CarregarClientes();

        }
        private void CarregarClientes()
        {
            var strConexao = "server=localhost;uid=root;database=bancodedados1";
            MySqlConnection conexao = new MySqlConnection(strConexao);

            try
            {
                conexao.Open();

                // Query SQL para selecionar todos os registros da tabela 'usuario'
                string query = "Select usuario.nome, usuario.Email,usuario.UsuarioID, usuario.status, usuario.Idade, usuario.DataCriacao, usuarioperfil.salario\r\nfrom usuario\r\njoin usuarioperfil ON usuario.UsuarioID = usuarioperfil.PerfilID ";
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
                    item.SubItems.Add(reader["idade"].ToString());
                    item.SubItems.Add(reader["status"].ToString());
                    item.SubItems.Add(reader["salario"].ToString());
                    item.SubItems.Add(Convert.ToDateTime(reader["DataCriacao"]).ToString("dd/MM/yyyy"));
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
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }



        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewClientes.SelectedItems.Count > 0)
            {
                MessageBox.Show("C�digo do produto escolhido: " + listViewClientes.SelectedItems[0].Text);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CarregarClientes();
        }
    }
}