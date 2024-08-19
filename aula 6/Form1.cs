using MySql.Data.MySqlClient;

namespace aula_6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //pwd = senha
            var strConexao = "server=localhost;uid=root;database=bancodedados1";
            var conexao = new MySqlConnection(strConexao);
            conexao.Open();
            listViewClientes.View = View.Details;
            listViewClientes.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Email", 200, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Data da Criação", 200, HorizontalAlignment.Left);
            listViewClientes.Columns.Add("Status", 200, HorizontalAlignment.Left);
            listViewClientes.FullRowSelect = true; // Ativa a seleção da linha toda
            listViewClientes.GridLines = true; // Adiciona linhas de grade para melhor visualização

            // Carrega os clientes na ListView
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
                string query = "SELECT * FROM usuario Where status = 'ativo' ";
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

        }
    }
}
