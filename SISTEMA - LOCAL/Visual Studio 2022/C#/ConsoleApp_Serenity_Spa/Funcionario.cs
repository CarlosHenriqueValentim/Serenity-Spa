using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Funcionario
    {
        public int CodigoFuncionario { get; set; }
        public int CodigoEmpresa { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public void Cadastrar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("Nome do Funcionário: ");
            Nome = Console.ReadLine();
            Console.Write("Cargo: ");
            Cargo = Console.ReadLine();
            Console.Write("Telefone: ");
            Telefone = Console.ReadLine();
            Console.Write("Email: ");
            Email = Console.ReadLine();

            string sql = "INSERT INTO funcionarios (codigo_empresa, nome_funcionario, cargo_funcionario, telefone_funcionario, email_funcionario) " +
                         "VALUES (@empresa, @nome, @cargo, @tel, @email)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@empresa", CodigoEmpresa);
            cmd.Parameters.AddWithValue("@nome", Nome);
            cmd.Parameters.AddWithValue("@cargo", Cargo);
            cmd.Parameters.AddWithValue("@tel", Telefone);
            cmd.Parameters.AddWithValue("@email", Email);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Funcionário cadastrado!");
        }

        public static void ListarTodos()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM funcionarios";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Funcionários ===\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["codigo_funcionario"]} | Nome: {reader["nome_funcionario"]} | Cargo: {reader["cargo_funcionario"]} | Telefone: {reader["telefone_funcionario"]} | Email: {reader["email_funcionario"]}");
            }
        }

        public static void Atualizar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do funcionário a atualizar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Novo cargo: ");
            string cargo = Console.ReadLine();
            Console.Write("Novo telefone: ");
            string tel = Console.ReadLine();
            Console.Write("Novo email: ");
            string email = Console.ReadLine();

            string sql = "UPDATE funcionarios SET cargo_funcionario=@cargo, telefone_funcionario=@tel, email_funcionario=@email WHERE codigo_funcionario=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@cargo", cargo);
            cmd.Parameters.AddWithValue("@tel", tel);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Funcionário atualizado!");
        }

        public static void Excluir()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do funcionário a excluir: ");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM funcionarios WHERE codigo_funcionario=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Funcionário excluído!");
        }
    }
}
