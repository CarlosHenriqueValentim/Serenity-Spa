using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    class Serviço
    {
       public class Servico
        {
            public int CodigoServico { get; set; }
            public int CodigoEmpresa { get; set; }
            public string NomeServico { get; set; }
            public string DescricaoServico { get; set; }
            public int DuracaoMinServico { get; set; }
            public int DuracaoMaxServico { get; set; }
            public decimal PrecoMinServico { get; set; }
            public decimal PrecoMaxServico { get; set; }
        }
    }
}
