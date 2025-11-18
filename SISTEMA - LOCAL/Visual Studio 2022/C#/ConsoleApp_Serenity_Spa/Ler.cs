using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public static class Ler
    {
        public static void MostrarClientes(int codigoEmpresa)
        {
            using (MySqlConnection conexao = Conexao.Conectar())
            {
                string sql = "SELECT * FROM clientes WHERE codigo_empresa=@empresa";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@empresa", codigoEmpresa);

                MySqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("ID | Nome | Telefone | Email | Nascimento | Sexo");
                Console.WriteLine("----------------------------------------------------------");

                while (reader.Read())
                {
                    Console.WriteLine($"{reader["codigo_cliente"]} | {reader["nome_cliente"]} | {reader["telefone_cliente"]} | {reader["email_cliente"]} | {reader["nascimento_cliente"]} | {reader["sexo_cliente"]}");
                }
            }
        }
    }
}
