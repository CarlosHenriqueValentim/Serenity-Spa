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
            new MySqlConnection();

            try
            {
                conexao = new MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=ss");
                conexao.Open();
            } 

            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            string sql = "select * from login";
            cmd = new MySqlCommand(sql, conexao);
            MySqlDataAdapter rdr = cmd.ExecuteReader();



            string opcao = "";

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
                opcao = Console.ReadLine();

                
            }
        }
    }
}