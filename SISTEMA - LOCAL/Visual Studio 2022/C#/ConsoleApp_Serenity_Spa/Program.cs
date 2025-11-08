using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.WriteLine("SERENITY SPA");
                Console.WriteLine("1 - Cadastrar Cliente");
                Console.WriteLine("2 - Listar Clientes");
                Console.WriteLine("3 - Atualizar Cliente");
                Console.WriteLine("4 - Excluir Cliente");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção:");

                opcao = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (opcao)
                {
                    case 1:
                        Criar.InserirCliente();
                        break;
                    case 2:

                        break;
                    case 3:
                        Atualizar.AtualizarCliente();
                        break;
                    case 4:
                        Excluir.ExcluirCliente();
                        break;
                    case 0:
                        Console.WriteLine("Saindo... Obrigado por usar o sistema Serenity Spa");
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.\n");
                        break;
                }
            } while (opcao != 0);
        }
    }
}