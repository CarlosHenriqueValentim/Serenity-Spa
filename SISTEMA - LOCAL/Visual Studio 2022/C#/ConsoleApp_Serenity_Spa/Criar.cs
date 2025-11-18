using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp_SerenitySpa
{
    public static class Criar
    {
        public static void CadastrarCliente(int codigoEmpresa)
        {
            using (MySqlConnection conexao = Conexao.Conectar())
            {
                Cliente c = new Cliente();
                c.CodigoEmpresa = codigoEmpresa;

                // Nome
                Console.Write("Nome: ");
                c.Nome = Console.ReadLine()?.Trim();

                // Telefone
                Console.Write("Telefone: ");
                c.Telefone = Console.ReadLine()?.Trim();

                // Email
                Console.Write("Email: ");
                c.Email = Console.ReadLine()?.Trim();

                // Data de nascimento
                DateTime nascimento;
                while (true)
                {
                    Console.Write("Data de nascimento (yyyy-MM-dd): ");
                    string input = Console.ReadLine()?.Trim().Replace(" ", "-").Replace("/", "-");

                    if (DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out nascimento))
                    {
                        c.Nascimento = nascimento;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Formato inválido. Digite no formato yyyy-MM-dd (ex: 2000-11-12).");
                    }
                }

                // Sexo
                string sexo;
                while (true)
                {
                    Console.Write("Sexo (m/f): ");
                    sexo = Console.ReadLine()?.Trim().ToLower();
                    if (sexo == "m" || sexo == "f")
                        break;
                    else
                        Console.WriteLine("Valor inválido. Digite 'm' ou 'f'.");
                }
                c.Sexo = sexo;

                // Comando SQL
                string sql = "INSERT INTO clientes (codigo_empresa, nome_cliente, telefone_cliente, email_cliente, nascimento_cliente, sexo_cliente) " +
                             "VALUES (@empresa, @nome, @tel, @email, @nasc, @sexo)";

                using (MySqlCommand cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@empresa", c.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@nome", c.Nome);
                    cmd.Parameters.AddWithValue("@tel", c.Telefone);
                    cmd.Parameters.AddWithValue("@email", c.Email);
                    cmd.Parameters.AddWithValue("@nasc", c.Nascimento.ToString("yyyy-MM-dd")); // envia no formato correto
                    cmd.Parameters.AddWithValue("@sexo", c.Sexo);

                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Cliente cadastrado com sucesso!");
        }
    }
}