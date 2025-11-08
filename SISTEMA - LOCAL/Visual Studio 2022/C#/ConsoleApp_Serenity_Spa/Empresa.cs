using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class Empresa
    {
        public int CodigoEmpresa { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CnpjEmpresa { get; set; }
        public string LocalizacaoEmpresa { get; set; }
        public string TelefoneEmpresa { get; set; }
        public string EmailEmpresa { get; set; }
        public string SiteEmpresa { get; set; }
      
    }
}

