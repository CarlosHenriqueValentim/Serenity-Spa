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
        public int CodigoCliente { get; set; }      // Código do cliente (PK)
        public int CodigoEmpresa { get; set; }      // Empresa
        public string Nome { get; set; }            // Nome do cliente
        public string Telefone { get; set; }        // Telefone
        public string Email { get; set; }           // Email
        public DateTime Nascimento { get; set; }    // Data de nascimento
        public string Sexo { get; set; }            // Sexo
    }
}
