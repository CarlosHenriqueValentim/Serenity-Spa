using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_SerenitySpa
{
    public class Cliente
    {
        private int codigo_cliente;
        protected int codigo_empresa;
        public string nome_cliente;
        public string telefone_cliente;
        public string email_cliente;
        public string nascimento_cliente;
        public string sexo_cliente;

        public void CadastrarCliente(int codigo)
        {
            Cliente c = new Cliente();
            c.codigo_empresa = codigo_empresa;
            c.codigo_cliente = codigo_cliente;

            Console.WriteLine("Nome:");
            c.nome_cliente = Console.ReadLine();

            Console.WriteLine("Telefone:");
            c.telefone_cliente = Console.ReadLine();

            Console.WriteLine("Email:");
            c.email_cliente = Console.ReadLine();

            Console.WriteLine("Data de nascimento:");
            c.nascimento_cliente = Console.ReadLine();

            Console.WriteLine("Sexo:");
            c.sexo_cliente = Console.ReadLine();

            Console.WriteLine("Cliente cadastrado com sucesso!");
        }

        public void ExibirDados()
        {
            Console.WriteLine($" Codigo: {codigo_cliente} - Nome do Cliente: {nome_cliente} | Telefone: {telefone_cliente} Email:{email_cliente} Data de nascimento:{nascimento_cliente} Sexo:{sexo_cliente}");
        }

        public void AlterarDados()
        {
            Console.WriteLine("Digite seu novo nome:");
            nome_cliente = Console.ReadLine();

            Console.WriteLine("Digite seu telefone:");
            telefone_cliente = Console.ReadLine();

            Console.WriteLine("Digite seu Email:");
            email_cliente = Console.ReadLine();

            Console.WriteLine("Digite sua data de nascimento:");
            nascimento_cliente = Console.ReadLine();

            Console.WriteLine("Digite seu sexo:");
            sexo_cliente = Console.ReadLine();

        }



    }



}

