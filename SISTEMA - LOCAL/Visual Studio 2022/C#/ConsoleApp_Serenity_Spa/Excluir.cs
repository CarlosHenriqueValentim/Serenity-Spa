using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    internal class Excluir
    {
        public static void ExcluirCliente()
        {
            Console.Write("ID do cliente: ");
            int id = int.Parse(Console.ReadLine());

            MySqlConnection conexao = Conexao.Conectar();
            string sql = "DELETE FROM clientes WHERE codigo_cliente=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            int linhas = cmd.ExecuteNonQuery();

            if (linhas > 0)
                Console.WriteLine("Cliente excluído com sucesso!");
            else
                Console.WriteLine("Cliente não encontrado.");

            Conexao.Desconectar(conexao);
        }
    }
}
