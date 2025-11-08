using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    internal class Criar
    {
        public static void InserirCliente()
        {
            MySqlConnection conexao = Conexao.Conectar();

            Console.WriteLine("Digite o nome do cliente:");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o email do cliente:");
            string email = Console.ReadLine();

            Console.WriteLine("Digite o telefone do cliente:");
            string telefone = Console.ReadLine();

            string sql = "INSERT INTO clientes (nome, email, telefone) VALUES (@nome, @email, @telefone)";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@telefone", telefone);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Cliente cadastrado com sucesso!\n");

            Conexao.Desconectar(conexao);
        }
    }
}
