using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    internal class Ler
    {
        public static void MostrarClientesTabela()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM clientes";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            // Cabeçalho da tabela
            Console.WriteLine("+----+--------------------------+-----------------+---------------------------+------------+------+");
            Console.WriteLine("| ID | Nome                     | Telefone        | Email                     | Nascimento | Sexo |");
            Console.WriteLine("+----+--------------------------+-----------------+---------------------------+------------+------+");

            // Conteúdo da tabela
            while (reader.Read())
            {
                string linha = String.Format("| {0,-2} | {1,-24} | {2,-15} | {3,-25} | {4,-10} | {5,-4} |",
                    reader["codigo_cliente"],
                    reader["nome_cliente"],
                    reader["telefone_cliente"],
                    reader["email_cliente"],
                    Convert.ToDateTime(reader["nascimento_cliente"]).ToString("yyyy-MM-dd"),
                    reader["sexo_cliente"]);
                Console.WriteLine(linha);
            }

            Console.WriteLine("+----+--------------------------+-----------------+---------------------------+------------+------+");
        }
    }
}