using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Pacote
    {
        public int CodigoPacote { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomePacote { get; set; }
        public string DescricaoPacote { get; set; }
        public decimal PrecoPacote { get; set; }
        public int DuracaoPacote { get; set; }
        public decimal ValorPorPessoaExtraPacote { get; set; }
    }
}
