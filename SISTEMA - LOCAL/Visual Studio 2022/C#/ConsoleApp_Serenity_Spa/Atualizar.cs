using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    internal class Atualizar
    {
        public static void AtualizarCliente()
        {
            Console.Write("ID do cliente: ");
            int id = int.Parse(Console.ReadLine());

            MySqlConnection conexao = Conexao.Conectar();
            string sqlSelect = "SELECT * FROM clientes WHERE codigo_cliente=@id";
            MySqlCommand cmd = new MySqlCommand(sqlSelect, conexao);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Cliente c = new Cliente
                {
                    Codigo = id,
                    Nome = reader["nome_cliente"].ToString(),
                    Telefone = reader["telefone_cliente"].ToString(),
                    Email = reader["email_cliente"].ToString(),
                    Nascimento = reader["nascimento_cliente"].ToString(),
                    Sexo = reader["sexo_cliente"].ToString()
                };
                reader.Close();
                c.AlterarDados();

                string sqlUpdate = @"UPDATE clientes 
                                     SET nome_cliente=@nome, telefone_cliente=@tel, email_cliente=@email, 
                                         nascimento_cliente=@nasc, sexo_cliente=@sexo
                                     WHERE codigo_cliente=@id";
                MySqlCommand cmdUpdate = new MySqlCommand(sqlUpdate, conexao);
                cmdUpdate.Parameters.AddWithValue("@nome", c.Nome);
                cmdUpdate.Parameters.AddWithValue("@tel", c.Telefone);
                cmdUpdate.Parameters.AddWithValue("@email", c.Email);
                cmdUpdate.Parameters.AddWithValue("@nasc", c.Nascimento);
                cmdUpdate.Parameters.AddWithValue("@sexo", c.Sexo);
                cmdUpdate.Parameters.AddWithValue("@id", id);

                cmdUpdate.ExecuteNonQuery();
                Console.WriteLine("Cliente atualizado com sucesso!");
            }
            else
            {
                Console.WriteLine("Cliente não encontrado.");
            }

            Conexao.Desconectar(conexao);
        }
    }
}
