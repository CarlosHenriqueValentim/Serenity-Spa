using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Empresa
    {
        private int codigo;
        public string nome;
        public string cnpj;
        public string localizacao;
        public string nome_fantasia;
        public string razao_social;
        public string telefone;
        public string email;
        public string site;

        public void ExibirEmpresa(int codigo)
        {
            Empresa Serenity = new Empresa();
            Console.WriteLine("----------Dados da Empresa Serenity SPA----------");
            Console.WriteLine($"Código: {codigo}");
            Console.WriteLine($"Razão social: {razao_social}");
            Console.WriteLine($"Nome fantasia: {nome_fantasia}");
            Console.WriteLine($"CNPJ: {cnpj}");
            Console.WriteLine($"Localização: {localizacao}");
            Console.WriteLine($"Telefone: {telefone}");
            Console.WriteLine($"Email: {email}");
            Console.WriteLine($"Site: {site}");
        }
      
    }
}

