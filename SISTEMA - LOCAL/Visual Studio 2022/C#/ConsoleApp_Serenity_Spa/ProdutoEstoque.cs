using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_SerenitySpa
{
    public class ProdutoEstoque
    {
        public int CodigoProduto { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeProdutoEstoque { get; set; }
        public string CategoriaProdutoEstoque { get; set; }
        public int QuantidadeProdutoEstoque { get; set; }
        public decimal PrecoUnitarioProdutoEstoque { get; set; }
        public decimal PrecoTotalProdutoEstoque { get; set; }
        public string FornecedorProdutoEstoque { get; set; }
    }
}
