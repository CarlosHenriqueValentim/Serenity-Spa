using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Pacote
    {
        public int CodigoPacote { get; set; }
        public int CodigoEmpresa { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Duracao { get; set; }
        public decimal ValorPessoaExtra { get; set; }

        public void Cadastrar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("Nome do Pacote: ");
            Nome = Console.ReadLine();
            Console.Write("Descrição: ");
            Descricao = Console.ReadLine();
            Console.Write("Preço: ");
            Preco = decimal.Parse(Console.ReadLine());
            Console.Write("Duração (minutos): ");
            Duracao = int.Parse(Console.ReadLine());
            Console.Write("Valor por pessoa extra: ");
            ValorPessoaExtra = decimal.Parse(Console.ReadLine());

            string sql = "INSERT INTO pacotes (codigo_empresa, nome_pacote, descricao_pacote, preco_pacote, duracao_pacote, valor_por_pessoa_extra_pacote) " +
                         "VALUES (@empresa, @nome, @desc, @preco, @duracao, @valorExtra)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@empresa", CodigoEmpresa);
            cmd.Parameters.AddWithValue("@nome", Nome);
            cmd.Parameters.AddWithValue("@desc", Descricao);
            cmd.Parameters.AddWithValue("@preco", Preco);
            cmd.Parameters.AddWithValue("@duracao", Duracao);
            cmd.Parameters.AddWithValue("@valorExtra", ValorPessoaExtra);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Pacote cadastrado com sucesso!");
        }

        public static void ListarTodos()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM pacotes";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Lista de Pacotes ===\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["codigo_pacote"]} | Nome: {reader["nome_pacote"]} | Preço: {reader["preco_pacote"]} | Duração: {reader["duracao_pacote"]} | Valor Extra: {reader["valor_por_pessoa_extra_pacote"]}");
            }
        }

        public static void Atualizar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do pacote a atualizar: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine();
            Console.Write("Nova descrição: ");
            string desc = Console.ReadLine();
            Console.Write("Novo preço: ");
            decimal preco = decimal.Parse(Console.ReadLine());
            Console.Write("Nova duração: ");
            int duracao = int.Parse(Console.ReadLine());
            Console.Write("Novo valor por pessoa extra: ");
            decimal valorExtra = decimal.Parse(Console.ReadLine());

            string sql = "UPDATE pacotes SET nome_pacote=@nome, descricao_pacote=@desc, preco_pacote=@preco, duracao_pacote=@duracao, valor_por_pessoa_extra_pacote=@valorExtra WHERE codigo_pacote=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@preco", preco);
            cmd.Parameters.AddWithValue("@duracao", duracao);
            cmd.Parameters.AddWithValue("@valorExtra", valorExtra);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Pacote atualizado com sucesso!");
        }

        public static void Excluir()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do pacote a excluir: ");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM pacotes WHERE codigo_pacote=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Pacote excluído com sucesso!");
        }
    }
}
