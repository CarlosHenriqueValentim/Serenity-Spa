using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Agendamento
    {
        public int CodigoAgendamento { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoFuncionario { get; set; }
        public int CodigoServico { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Duracao { get; set; }
        public string Status { get; set; }

        public void Cadastrar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("Código do Cliente: ");
            CodigoCliente = int.Parse(Console.ReadLine());
            Console.Write("Código do Funcionário: ");
            CodigoFuncionario = int.Parse(Console.ReadLine());
            Console.Write("Código do Serviço: ");
            CodigoServico = int.Parse(Console.ReadLine());
            Console.Write("Data (yyyy-mm-dd): ");
            Data = DateTime.Parse(Console.ReadLine());
            Console.Write("Duração (hh:mm): ");
            Duracao = TimeSpan.Parse(Console.ReadLine());
            Console.Write("Status (agendado/concluido/cancelado): ");
            Status = Console.ReadLine();

            string sql = "INSERT INTO agendamentos (codigo_empresa, codigo_cliente, codigo_funcionario, codigo_servico, data, duracao_agendamento, status) " +
                         "VALUES (@empresa, @cliente, @func, @servico, @data, @duracao, @status)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@empresa", CodigoEmpresa);
            cmd.Parameters.AddWithValue("@cliente", CodigoCliente);
            cmd.Parameters.AddWithValue("@func", CodigoFuncionario);
            cmd.Parameters.AddWithValue("@servico", CodigoServico);
            cmd.Parameters.AddWithValue("@data", Data);
            cmd.Parameters.AddWithValue("@duracao", Duracao);
            cmd.Parameters.AddWithValue("@status", Status);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Agendamento cadastrado!");
        }

        public static void ListarTodos()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM agendamentos";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Agendamentos ===\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["codigo_agendamento"]} | Cliente: {reader["codigo_cliente"]} | Funcionário: {reader["codigo_funcionario"]} | Serviço: {reader["codigo_servico"]} | Data: {reader["data"]} | Duração: {reader["duracao_agendamento"]} | Status: {reader["status"]}");
            }
        }

        public static void Atualizar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do agendamento a atualizar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Novo status: ");
            string status = Console.ReadLine();

            string sql = "UPDATE agendamentos SET status=@status WHERE codigo_agendamento=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Agendamento atualizado!");
        }

        public static void Excluir()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do agendamento a excluir: ");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM agendamentos WHERE codigo_agendamento=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Agendamento excluído!");
        }
    }
}
