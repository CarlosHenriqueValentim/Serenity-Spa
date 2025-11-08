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
            MySqlConnection conexao = Conexao.Conectar();

            Console.WriteLine("Digite o ID do cliente que deseja excluir:");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM clientes WHERE id_cliente = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Cliente excluído com sucesso!\n");

            Conexao.Desconectar(conexao);
        }
    }
}
