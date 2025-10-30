using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_SerenitySpa
{
    public class Program
    {
        static void Main(string[] args)
        {
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
