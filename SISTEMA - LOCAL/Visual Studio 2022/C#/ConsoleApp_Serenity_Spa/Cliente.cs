using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Cliente
    {
        public int Codigo { get; set; }
        public int Empresa { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Nascimento { get; set; }
        public string Sexo { get; set; }

        public void AlterarDados()
        {
            Console.WriteLine("Digite novo nome:");
            Nome = Console.ReadLine();

            Console.WriteLine("Digite novo telefone:");
            Telefone = Console.ReadLine();

            Console.WriteLine("Digite novo email:");
            Email = Console.ReadLine();

            Console.WriteLine("Digite nova data de nascimento (yyyy-mm-dd):");
            Nascimento = Console.ReadLine();

            Console.WriteLine("Digite sexo (m/f):");
            Sexo = Console.ReadLine();
        }

        public void Exibir()
        {
            Console.WriteLine($"ID: {Codigo} | Nome: {Nome} | Telefone: {Telefone} | Email: {Email} | Nascimento: {Nascimento} | Sexo: {Sexo}");
        }
    }
}
