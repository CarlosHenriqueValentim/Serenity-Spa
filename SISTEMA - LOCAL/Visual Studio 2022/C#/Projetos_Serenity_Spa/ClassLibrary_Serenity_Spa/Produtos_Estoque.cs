using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class ProdutoEstoque
    {
        public int CodigoProduto { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeProduto { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public string Fornecedor { get; set; }
    }

    public class ProdutoEstoqueDAO
    {
        public void Inserir(ProdutoEstoque p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO produtos_estoque 
                               (codigo_empresa, nome_produto_estoque, categoria_produto_estoque, quantidade_produto_estoque, preco_unitario_produto_estoque, preco_total_produto_estoque, fornecedor_produto_estoque)
                               VALUES (@empresa, @nome, @categoria, @quantidade, @precoUnitario, @precoTotal, @fornecedor)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", p.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@nome", p.NomeProduto);
                    cmd.Parameters.AddWithValue("@categoria", p.Categoria);
                    cmd.Parameters.AddWithValue("@quantidade", p.Quantidade);
                    cmd.Parameters.AddWithValue("@precoUnitario", p.PrecoUnitario);
                    cmd.Parameters.AddWithValue("@precoTotal", p.PrecoTotal);
                    cmd.Parameters.AddWithValue("@fornecedor", p.Fornecedor);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ProdutoEstoque> Listar()
        {
            var lista = new List<ProdutoEstoque>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM produtos_estoque";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ProdutoEstoque
                        {
                            CodigoProduto = dr.GetInt32("codigo_produto"),
                            CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                            NomeProduto = dr.GetString("nome_produto_estoque"),
                            Categoria = dr.GetString("categoria_produto_estoque"),
                            Quantidade = dr.GetInt32("quantidade_produto_estoque"),
                            PrecoUnitario = dr.GetDecimal("preco_unitario_produto_estoque"),
                            PrecoTotal = dr.GetDecimal("preco_total_produto_estoque"),
                            Fornecedor = dr.GetString("fornecedor_produto_estoque")
                        });
                    }
                }
            }
            return lista;
        }

        public ProdutoEstoque BuscarPorId(int id)
        {
            ProdutoEstoque p = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM produtos_estoque WHERE codigo_produto=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            p = new ProdutoEstoque
                            {
                                CodigoProduto = dr.GetInt32("codigo_produto"),
                                CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                                NomeProduto = dr.GetString("nome_produto_estoque"),
                                Categoria = dr.GetString("categoria_produto_estoque"),
                                Quantidade = dr.GetInt32("quantidade_produto_estoque"),
                                PrecoUnitario = dr.GetDecimal("preco_unitario_produto_estoque"),
                                PrecoTotal = dr.GetDecimal("preco_total_produto_estoque"),
                                Fornecedor = dr.GetString("fornecedor_produto_estoque")
                            };
                        }
                    }
                }
            }
            return p;
        }

        public void Atualizar(ProdutoEstoque p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE produtos_estoque SET 
                               codigo_empresa=@empresa, nome_produto_estoque=@nome, categoria_produto_estoque=@categoria, quantidade_produto_estoque=@quantidade,
                               preco_unitario_produto_estoque=@precoUnitario, preco_total_produto_estoque=@precoTotal, fornecedor_produto_estoque=@fornecedor
                               WHERE codigo_produto=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", p.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@nome", p.NomeProduto);
                    cmd.Parameters.AddWithValue("@categoria", p.Categoria);
                    cmd.Parameters.AddWithValue("@quantidade", p.Quantidade);
                    cmd.Parameters.AddWithValue("@precoUnitario", p.PrecoUnitario);
                    cmd.Parameters.AddWithValue("@precoTotal", p.PrecoTotal);
                    cmd.Parameters.AddWithValue("@fornecedor", p.Fornecedor);
                    cmd.Parameters.AddWithValue("@id", p.CodigoProduto);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM produtos_estoque WHERE codigo_produto=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int Contar()
        {
            int total = 0;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM produtos_estoque";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
