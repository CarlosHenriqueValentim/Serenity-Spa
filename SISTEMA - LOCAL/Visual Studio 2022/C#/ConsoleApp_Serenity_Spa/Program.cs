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
            string opcao;
            int empresa = 1; // código da empresa padrão

            do
            {
                Console.Clear();
                DesenharCabecalho();

                // Menu centralizado com espaçamento
                Console.WriteLine("                       MENU PRINCIPAL                       ");
                Console.WriteLine("============================================================");
                Console.WriteLine(" 1 - Cadastrar Cliente");
                Console.WriteLine(" 2 - Listar Clientes");
                Console.WriteLine(" 3 - Atualizar Cliente");
                Console.WriteLine(" 4 - Excluir Cliente");
                Console.WriteLine(" 0 - Sair");
                Console.WriteLine("============================================================");
                Console.Write("Escolha uma opção: ");
                opcao = Console.ReadLine();

                Console.Clear();
                DesenharCabecalho();

                switch (opcao)
                {
                    case "1":
                        DesenharTitulo("CADASTRO DE CLIENTE");
                        Criar.CadastrarCliente(empresa);
                        break;

                    case "2":
                        DesenharTitulo("LISTA DE CLIENTES");
                        Ler.MostrarClientesTabela();
                        break;

                    case "3":
                        DesenharTitulo("ATUALIZAR CLIENTE");
                        Atualizar.AtualizarCliente();
                        break;

                    case "4":
                        DesenharTitulo("EXCLUIR CLIENTE");
                        Excluir.ExcluirCliente();
                        break;

                    case "0":
                        Console.WriteLine("Saindo... Obrigado por usar o Serenity Spa!");
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }

                if (opcao != "0")
                {
                    Console.WriteLine("\nPressione Enter para voltar ao menu...");
                    Console.ReadLine();
                }

            } while (opcao != "0");
        }

        static void DesenharCabecalho()
        {
            Console.WriteLine("============================================================");
            Console.WriteLine("                     SERENITY SPA                             ");
            Console.WriteLine("============================================================\n");
        }

        static void DesenharTitulo(string titulo)
        {
            string linha = new string('=', titulo.Length + 10);
            Console.WriteLine(linha);
            Console.WriteLine($"   {titulo.ToUpper()}   ");
            Console.WriteLine(linha + "\n");
        }
    }
}
