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
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public string Fornecedor { get; set; }

        public void Cadastrar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("Nome do Produto: ");
            Nome = Console.ReadLine();
            Console.Write("Categoria: ");
            Categoria = Console.ReadLine();
            Console.Write("Quantidade: ");
            Quantidade = int.Parse(Console.ReadLine());
            Console.Write("Preço Unitário: ");
            PrecoUnitario = decimal.Parse(Console.ReadLine());
            PrecoTotal = PrecoUnitario * Quantidade;
            Console.Write("Fornecedor: ");
            Fornecedor = Console.ReadLine();

            string sql = "INSERT INTO produtos_estoque (codigo_empresa, nome_produto_estoque, categoria_produto_estoque, quantidade_produto_estoque, preco_unitario_produto_estoque, preco_total_produto_estoque, fornecedor_produto_estoque) " +
                         "VALUES (@empresa, @nome, @cat, @qtd, @preco, @total, @fornecedor)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@empresa", CodigoEmpresa);
            cmd.Parameters.AddWithValue("@nome", Nome);
            cmd.Parameters.AddWithValue("@cat", Categoria);
            cmd.Parameters.AddWithValue("@qtd", Quantidade);
            cmd.Parameters.AddWithValue("@preco", PrecoUnitario);
            cmd.Parameters.AddWithValue("@total", PrecoTotal);
            cmd.Parameters.AddWithValue("@fornecedor", Fornecedor);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Produto cadastrado com sucesso!");
        }

        public static void ListarTodos()
        {
            MySqlConnection conexao = Conexao.Conectar();
            string sql = "SELECT * FROM produtos_estoque";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n=== Produtos em Estoque ===\n");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["codigo_produto"]} | Nome: {reader["nome_produto_estoque"]} | Categoria: {reader["categoria_produto_estoque"]} | Quantidade: {reader["quantidade_produto_estoque"]} | Preço Unit: {reader["preco_unitario_produto_estoque"]} | Total: {reader["preco_total_produto_estoque"]} | Fornecedor: {reader["fornecedor_produto_estoque"]}");
            }
        }

        public static void Atualizar()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do produto a atualizar: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Nova quantidade: ");
            int qtd = int.Parse(Console.ReadLine());
            Console.Write("Novo preço unitário: ");
            decimal preco = decimal.Parse(Console.ReadLine());
            decimal total = qtd * preco;

            string sql = "UPDATE produtos_estoque SET quantidade_produto_estoque=@qtd, preco_unitario_produto_estoque=@preco, preco_total_produto_estoque=@total WHERE codigo_produto=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@qtd", qtd);
            cmd.Parameters.AddWithValue("@preco", preco);
            cmd.Parameters.AddWithValue("@total", total);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Produto atualizado!");
        }

        public static void Excluir()
        {
            MySqlConnection conexao = Conexao.Conectar();
            Console.Write("ID do produto a excluir: ");
            int id = int.Parse(Console.ReadLine());

            string sql = "DELETE FROM produtos_estoque WHERE codigo_produto=@id";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Produto excluído!");
        }
    }
}
