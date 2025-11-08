using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Financeiro
    {
        public int CodigoFinanceiro { get; set; }
        public int CodigoEmpresa { get; set; }
        public string TipoFinanceiro { get; set; }
        public string DescricaoFinanceiro { get; set; }
        public decimal ValorFinanceiro { get; set; }
        public DateTime DataFinanceiro { get; set; }
    }
}
