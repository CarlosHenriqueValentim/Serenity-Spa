using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Funcionario
    {
        public int CodigoFuncionario { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeFuncionario { get; set; }
        public string CargoFuncionario { get; set; }
        public string TelefoneFuncionario { get; set; }
        public string EmailFuncionario { get; set; }
    }
}

