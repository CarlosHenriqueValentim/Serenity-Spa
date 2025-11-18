using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class ItensPacote
    {
        public int CodigoItem { get; set; }
        public int CodigoPacote { get; set; }
        public int CodigoServico { get; set; }
        public int OrdemExecucao { get; set; }
        public int Quantidade { get; set; }

        public void Cadastrar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("Código do Pacote: ");
            CodigoPacote = int.Parse(Console.ReadLine());
            Console.Write("Código do Serviço: ");
            CodigoServico = int.Parse(Console.ReadLine());
            Console.Write("Ordem de Execução: ");
            OrdemExecucao = int.Parse(Console.ReadLine());
            Console.Write("Quantidade: ");
            Quantidade = int.Parse(Console.ReadLine());

            string sql = "INSERT INTO itens_pacote (codigo_pacote, codigo_servico, ordem_execucao_itens_pacote, quantidade_itens_pacote) " +
                         "VALUES (@pacote, @servico, @ordem, @quantidade)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@pacote", CodigoPacote);
            cmd.Parameters.AddWithValue("@servico", CodigoServico);
            cmd.Parameters.AddWithValue("@ordem", OrdemExecucao);
            cmd.Parameters.AddWithValue("@quantidade", Quantidade);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Item do pacote cadastrado!");
        }

        public static void ListarTodos()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM itens_pacote";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Itens dos Pacotes ===\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["codigo_itens_pacote"]} | Pacote: {reader["codigo_pacote"]} | Serviço: {reader["codigo_servico"]} | Ordem: {reader["ordem_execucao_itens_pacote"]} | Quantidade: {reader["quantidade_itens_pacote"]}");
            }
        }

        public static void Atualizar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do item a atualizar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nova ordem de execução: ");
            int ordem = int.Parse(Console.ReadLine());
            Console.Write("Nova quantidade: ");
            int qtd = int.Parse(Console.ReadLine());

            string sql = "UPDATE itens_pacote SET ordem_execucao_itens_pacote=@ordem, quantidade_itens_pacote=@qtd WHERE codigo_itens_pacote=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@ordem", ordem);
            cmd.Parameters.AddWithValue("@qtd", qtd);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Item atualizado!");
        }

        public static void Excluir()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do item a excluir: ");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM itens_pacote WHERE codigo_itens_pacote=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Item excluído!");
        }
    }
}
