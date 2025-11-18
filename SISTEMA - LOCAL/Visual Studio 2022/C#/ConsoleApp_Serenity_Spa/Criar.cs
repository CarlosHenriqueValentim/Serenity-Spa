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
        public static void CadastrarCliente(int codigoEmpresa)
        {
            MySqlConnection conexao = Conexao.Conectar();

            Console.WriteLine("Nome:");
            string nome = Console.ReadLine();

            Console.WriteLine("Telefone:");
            string telefone = Console.ReadLine();

            Console.WriteLine("Email:");
            string email = Console.ReadLine();

            Console.WriteLine("Nascimento (yyyy-mm-dd):");
            string nascimento = Console.ReadLine();

            Console.WriteLine("Sexo (m/f):");
            string sexo = Console.ReadLine();

            string sql = @"INSERT INTO clientes 
                        (codigo_empresa, nome_cliente, telefone_cliente, email_cliente, nascimento_cliente, sexo_cliente)
                        VALUES (@empresa,@nome,@tel,@email,@nasc,@sexo)";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@empresa", codigoEmpresa);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@tel", telefone);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@nasc", nascimento);
            cmd.Parameters.AddWithValue("@sexo", sexo);

            cmd.ExecuteNonQuery();
            Console.WriteLine("Cliente cadastrado com sucesso!");

            Conexao.Desconectar(conexao);
        }
    }
}
