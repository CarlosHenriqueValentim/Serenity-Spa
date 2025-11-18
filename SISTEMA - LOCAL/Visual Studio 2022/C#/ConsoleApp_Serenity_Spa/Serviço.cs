using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Servico
    {
        public int CodigoServico { get; set; }
        public int CodigoEmpresa { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int DuracaoMin { get; set; }
        public int DuracaoMax { get; set; }
        public decimal PrecoMin { get; set; }
        public decimal PrecoMax { get; set; }

        // CADASTRAR
        public void Cadastrar()
        {
            MySqlConnection conexao = Conexao.Conectar();

            Console.Write("Nome do Serviço: ");
            Nome = Console.ReadLine();
            Console.Write("Descrição: ");
            Descricao = Console.ReadLine();
            Console.Write("Duração mínima (min): ");
            DuracaoMin = int.Parse(Console.ReadLine());
            Console.Write("Duração máxima (min): ");
            DuracaoMax = int.Parse(Console.ReadLine());
            Console.Write("Preço mínimo: ");
            PrecoMin = decimal.Parse(Console.ReadLine());
            Console.Write("Preço máximo: ");
            PrecoMax = decimal.Parse(Console.ReadLine());

            string sql = "INSERT INTO servicos (codigo_empresa, nome_servico, descricao_servico, duracao_min_servico, duracao_max_servico, preco_min_servico, preco_max_servico) " +
                         "VALUES (@empresa, @nome, @descricao, @dmin, @dmax, @pmin, @pmax)";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@empresa", CodigoEmpresa);
            cmd.Parameters.AddWithValue("@nome", Nome);
            cmd.Parameters.AddWithValue("@descricao", Descricao);
            cmd.Parameters.AddWithValue("@dmin", DuracaoMin);
            cmd.Parameters.AddWithValue("@dmax", DuracaoMax);
            cmd.Parameters.AddWithValue("@pmin", PrecoMin);
            cmd.Parameters.AddWithValue("@pmax", PrecoMax);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Serviço cadastrado com sucesso!");
        }

        // LISTAR
        public static void ListarTodos()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM servicos";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Lista de Serviços ===\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["codigo_servico"]} | Nome: {reader["nome_servico"]} | Descrição: {reader["descricao_servico"]} | Preço: {reader["preco_min_servico"]}-{reader["preco_max_servico"]} | Duração: {reader["duracao_min_servico"]}-{reader["duracao_max_servico"]} min");
            }
        }

        // ATUALIZAR
        public static void Atualizar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do serviço a atualizar: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Novo nome: ");
            string nome = Console.ReadLine();
            Console.Write("Nova descrição: ");
            string descricao = Console.ReadLine();
            Console.Write("Duração mínima: ");
            int dmin = int.Parse(Console.ReadLine());
            Console.Write("Duração máxima: ");
            int dmax = int.Parse(Console.ReadLine());
            Console.Write("Preço mínimo: ");
            decimal pmin = decimal.Parse(Console.ReadLine());
            Console.Write("Preço máximo: ");
            decimal pmax = decimal.Parse(Console.ReadLine());

            string sql = "UPDATE servicos SET nome_servico=@nome, descricao_servico=@descricao, duracao_min_servico=@dmin, duracao_max_servico=@dmax, preco_min_servico=@pmin, preco_max_servico=@pmax WHERE codigo_servico=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@descricao", descricao);
            cmd.Parameters.AddWithValue("@dmin", dmin);
            cmd.Parameters.AddWithValue("@dmax", dmax);
            cmd.Parameters.AddWithValue("@pmin", pmin);
            cmd.Parameters.AddWithValue("@pmax", pmax);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Serviço atualizado com sucesso!");
        }

        // EXCLUIR
        public static void Excluir()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do serviço a excluir: ");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM servicos WHERE codigo_servico=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Serviço excluído com sucesso!");
        }
    }
}
