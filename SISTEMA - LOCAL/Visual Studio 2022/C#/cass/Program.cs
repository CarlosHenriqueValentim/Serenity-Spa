using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace cass
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection conexao;
            MySqlCommand cmd;
            MySqlDataReader rdr;

            try
            {
                conexao = new MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=ss");
                conexao.Open();
                Console.WriteLine("Conexão estabelecida com sucesso!");

                string sql = "select * from clientes";
                cmd = new MySqlCommand(sql, conexao);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("Código do cliente: " + rdr["codigo_cliente"].ToString());
                    Console.WriteLine("Nome: " + rdr["nome_cliente"].ToString());
                    Console.WriteLine("Telefone: " + rdr["telefone_cliente"].ToString());
                    Console.WriteLine("E-mail: " + rdr["email_cliente"].ToString());
                    Console.WriteLine("Nascimento: " + Convert.ToDateTime(rdr["nascimento_cliente"]).ToString("dd/MM/yyyy"));
                    Console.WriteLine("Sexo: " + rdr["sexo_cliente"].ToString());
                    Console.WriteLine("----------------------------------");
                }

                rdr.Close();
                conexao.Close();
                Console.WriteLine("Conexão encerrada com sucesso.");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }

            Console.ReadKey();


            /*string opcao = "";

            while (opcao != "q")
            {
                Console.Clear();
                Console.WriteLine("---------- Serenity Spa ----------");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Clientes");
                Console.WriteLine("3 - Cadastrar Funcionário");
                Console.WriteLine("4 - Listar Funcionários");
                Console.WriteLine("5 - Cadastrar Serviço");
                Console.WriteLine("6 - Listar Serviços");
                Console.WriteLine("7 - Fazer Agendamento");
                Console.WriteLine("8 - Listar Agendamentos");
                Console.WriteLine("q - Sair");
                Console.Write("Escolha: ");
                opcao = Console.ReadLine();*/


        }
    }
}