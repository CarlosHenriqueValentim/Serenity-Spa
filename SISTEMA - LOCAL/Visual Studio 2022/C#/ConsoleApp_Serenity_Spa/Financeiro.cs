using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Financeiro
    {
        public int CodigoFinanceiro { get; set; }
        public int CodigoEmpresa { get; set; }
        public string Tipo { get; set; } // receita ou despesa
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }

        public void Cadastrar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("Tipo (receita/despesa): ");
            Tipo = Console.ReadLine();
            Console.Write("Descrição: ");
            Descricao = Console.ReadLine();
            Console.Write("Valor: ");
            Valor = decimal.Parse(Console.ReadLine());
            Console.Write("Data (yyyy-mm-dd): ");
            Data = DateTime.Parse(Console.ReadLine());

            string sql = "INSERT INTO financeiro (codigo_empresa, tipo_financeiro, descricao_financeiro, valor_financeiro, data_financeiro) " +
                         "VALUES (@empresa, @tipo, @desc, @valor, @data)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@empresa", CodigoEmpresa);
            cmd.Parameters.AddWithValue("@tipo", Tipo);
            cmd.Parameters.AddWithValue("@desc", Descricao);
            cmd.Parameters.AddWithValue("@valor", Valor);
            cmd.Parameters.AddWithValue("@data", Data);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Registro financeiro cadastrado!");
        }

        public static void ListarTodos()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM financeiro";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Financeiro ===\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["codigo_financeiro"]} | Tipo: {reader["tipo_financeiro"]} | Descrição: {reader["descricao_financeiro"]} | Valor: {reader["valor_financeiro"]} | Data: {reader["data_financeiro"]}");
            }
        }

        public static void Atualizar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do registro a atualizar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Novo tipo: ");
            string tipo = Console.ReadLine();
            Console.Write("Nova descrição: ");
            string desc = Console.ReadLine();
            Console.Write("Novo valor: ");
            decimal valor = decimal.Parse(Console.ReadLine());
            Console.Write("Nova data (yyyy-mm-dd): ");
            DateTime data = DateTime.Parse(Console.ReadLine());

            string sql = "UPDATE financeiro SET tipo_financeiro=@tipo, descricao_financeiro=@desc, valor_financeiro=@valor, data_financeiro=@data WHERE codigo_financeiro=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@valor", valor);
            cmd.Parameters.AddWithValue("@data", data);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Registro atualizado!");
        }

        public static void Excluir()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do registro a excluir: ");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM financeiro WHERE codigo_financeiro=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Registro excluído!");
        }
    }
}
