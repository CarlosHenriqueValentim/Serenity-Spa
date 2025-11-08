using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Agendamentos
    {
        public int CodigoAgendamento { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoFuncionario { get; set; }
        public int CodigoServico { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan DuracaoAgendamento { get; set; }
        public string Status { get; set; }
    }
}
