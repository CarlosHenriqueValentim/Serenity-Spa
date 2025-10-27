using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Funcionario
    {
        private int codigo_funcionario;
        protected int codigo_empresa;
        public string nomefuncionario;
        public string cargo_funcionario;
        public string telefone_funcionario;
        public string email_funcionario;


        public void CadastrarFuncionario(int codigo)
        {
            Funcionario f = new Funcionario();
            f.codigo_empresa = codigo_empresa;
            f.codigo_funcionario = codigo;

            Console.WriteLine("Nome do Funcionario:");
            f.nomefuncionario = Console.ReadLine();

            Console.WriteLine("Cargo:");
            f.cargo_funcionario = Console.ReadLine();

            Console.WriteLine("Telefone:");
            f.telefone_funcionario = Console.ReadLine();

            Console.WriteLine("Email:");
            f.email_funcionario = Console.ReadLine();

            Console.WriteLine("Funcionario cadastrado com sucesso!");
        }
         public void AlterarDados()
        {
            Console.WriteLine("Digite seu novo nome:");
            nomefuncionario = Console.ReadLine();
            Console.WriteLine("Digite seu cargo:");
            cargo_funcionario= Console.ReadLine();
        }

        public void ExibirDados()
        {
            Console.WriteLine($" Codigo: {codigo_funcionario} - Nome do Funcionario {nomefuncionario}| Cargo: {cargo_funcionario} Telefone: {telefone_funcionario} Email: {email_funcionario}");
        }

        }


    }

