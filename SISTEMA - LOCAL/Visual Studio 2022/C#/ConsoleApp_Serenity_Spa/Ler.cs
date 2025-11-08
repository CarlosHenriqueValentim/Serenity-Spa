using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa.CRUD
{
    class Ler
    {
        public static void MostrarClientes()
        {
            MySqlConnection conexao = Conexao.Conectar();

            string sql = "SELECT * FROM clientes";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\nLista de Clientes:\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id_cliente"]} | Nome: {reader["nome"]} | Email: {reader["email"]} | Telefone: {reader["telefone"]}");
            }

            Console.WriteLine();
            Conexao.Desconectar(conexao);
        }
    }
}
