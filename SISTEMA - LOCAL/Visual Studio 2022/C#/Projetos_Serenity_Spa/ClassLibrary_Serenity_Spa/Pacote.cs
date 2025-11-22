using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class Pacote
    {
        public int CodigoPacote { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomePacote { get; set; }
        public string DescricaoPacote { get; set; }
        public decimal PrecoPacote { get; set; }
        public int DuracaoPacote { get; set; }
        public decimal ValorPessoaExtra { get; set; }
    }

    public class PacoteDAO
    {
        public void Inserir(Pacote p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO pacotes 
                               (codigo_empresa, nome_pacote, descricao_pacote, preco_pacote, duracao_pacote, valor_por_pessoa_extra_pacote)
                               VALUES (@empresa, @nome, @desc, @preco, @duracao, @extra)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", p.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@nome", p.NomePacote);
                    cmd.Parameters.AddWithValue("@desc", p.DescricaoPacote);
                    cmd.Parameters.AddWithValue("@preco", p.PrecoPacote);
                    cmd.Parameters.AddWithValue("@duracao", p.DuracaoPacote);
                    cmd.Parameters.AddWithValue("@extra", p.ValorPessoaExtra);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Pacote> Listar()
        {
            var lista = new List<Pacote>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM pacotes";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Pacote
                        {
                            CodigoPacote = dr.GetInt32("codigo_pacote"),
                            CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                            NomePacote = dr.GetString("nome_pacote"),
                            DescricaoPacote = dr.GetString("descricao_pacote"),
                            PrecoPacote = dr.GetDecimal("preco_pacote"),
                            DuracaoPacote = dr.GetInt32("duracao_pacote"),
                            ValorPessoaExtra = dr.GetDecimal("valor_por_pessoa_extra_pacote")
                        });
                    }
                }
            }
            return lista;
        }

        public Pacote BuscarPorId(int id)
        {
            Pacote p = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM pacotes WHERE codigo_pacote=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            p = new Pacote
                            {
                                CodigoPacote = dr.GetInt32("codigo_pacote"),
                                CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                                NomePacote = dr.GetString("nome_pacote"),
                                DescricaoPacote = dr.GetString("descricao_pacote"),
                                PrecoPacote = dr.GetDecimal("preco_pacote"),
                                DuracaoPacote = dr.GetInt32("duracao_pacote"),
                                ValorPessoaExtra = dr.GetDecimal("valor_por_pessoa_extra_pacote")
                            };
                        }
                    }
                }
            }
            return p;
        }

        public void Atualizar(Pacote p)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE pacotes SET nome_pacote=@nome, descricao_pacote=@desc, preco_pacote=@preco, duracao_pacote=@duracao, valor_por_pessoa_extra_pacote=@extra 
                               WHERE codigo_pacote=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", p.NomePacote);
                    cmd.Parameters.AddWithValue("@desc", p.DescricaoPacote);
                    cmd.Parameters.AddWithValue("@preco", p.PrecoPacote);
                    cmd.Parameters.AddWithValue("@duracao", p.DuracaoPacote);
                    cmd.Parameters.AddWithValue("@extra", p.ValorPessoaExtra);
                    cmd.Parameters.AddWithValue("@id", p.CodigoPacote);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM pacotes WHERE codigo_pacote=@id";
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
                string sql = "SELECT COUNT(*) FROM pacotes";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
