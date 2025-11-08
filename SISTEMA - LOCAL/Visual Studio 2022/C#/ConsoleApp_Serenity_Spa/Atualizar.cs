using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Atualizar
    {
        public static void AtualizarCliente()
        {
            MySqlConnection conexao = Conexao.Conectar();

            Console.WriteLine("Digite o ID do cliente que deseja atualizar:");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o novo nome:");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o novo email:");
            string email = Console.ReadLine();

            Console.WriteLine("Digite o novo telefone:");
            string telefone = Console.ReadLine();

            string sql = "UPDATE clientes SET nome = @nome, email = @email, telefone = @telefone WHERE id_cliente = @id";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@telefone", telefone);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Cliente atualizado com sucesso!\n");

            Conexao.Desconectar(conexao);
        }
    }
}
