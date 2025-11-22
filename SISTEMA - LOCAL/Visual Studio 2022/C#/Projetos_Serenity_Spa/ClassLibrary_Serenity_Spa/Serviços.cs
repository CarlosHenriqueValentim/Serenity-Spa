using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class Servico
    {
        public int CodigoServico { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeServico { get; set; }
        public string DescricaoServico { get; set; }
        public int DuracaoMin { get; set; }
        public int DuracaoMax { get; set; }
        public decimal PrecoMin { get; set; }
        public decimal PrecoMax { get; set; }
    }

    public class ServicoDAO
    {
        public void Inserir(Servico s)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO servicos 
                               (codigo_empresa, nome_servico, descricao_servico, duracao_min_servico, duracao_max_servico, preco_min_servico, preco_max_servico)
                               VALUES (@empresa, @nome, @desc, @min, @max, @precoMin, @precoMax)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", s.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@nome", s.NomeServico);
                    cmd.Parameters.AddWithValue("@desc", s.DescricaoServico);
                    cmd.Parameters.AddWithValue("@min", s.DuracaoMin);
                    cmd.Parameters.AddWithValue("@max", s.DuracaoMax);
                    cmd.Parameters.AddWithValue("@precoMin", s.PrecoMin);
                    cmd.Parameters.AddWithValue("@precoMax", s.PrecoMax);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Servico> Listar()
        {
            var lista = new List<Servico>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM servicos";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Servico
                        {
                            CodigoServico = dr.GetInt32("codigo_servico"),
                            CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                            NomeServico = dr.GetString("nome_servico"),
                            DescricaoServico = dr.GetString("descricao_servico"),
                            DuracaoMin = dr.GetInt32("duracao_min_servico"),
                            DuracaoMax = dr.GetInt32("duracao_max_servico"),
                            PrecoMin = dr.GetDecimal("preco_min_servico"),
                            PrecoMax = dr.GetDecimal("preco_max_servico")
                        });
                    }
                }
            }
            return lista;
        }

        public Servico BuscarPorId(int id)
        {
            Servico s = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM servicos WHERE codigo_servico=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            s = new Servico
                            {
                                CodigoServico = dr.GetInt32("codigo_servico"),
                                CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                                NomeServico = dr.GetString("nome_servico"),
                                DescricaoServico = dr.GetString("descricao_servico"),
                                DuracaoMin = dr.GetInt32("duracao_min_servico"),
                                DuracaoMax = dr.GetInt32("duracao_max_servico"),
                                PrecoMin = dr.GetDecimal("preco_min_servico"),
                                PrecoMax = dr.GetDecimal("preco_max_servico")
                            };
                        }
                    }
                }
            }
            return s;
        }

        public void Atualizar(Servico s)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE servicos SET nome_servico=@nome, descricao_servico=@desc, duracao_min_servico=@min, duracao_max_servico=@max, preco_min_servico=@precoMin, preco_max_servico=@precoMax 
                               WHERE codigo_servico=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", s.NomeServico);
                    cmd.Parameters.AddWithValue("@desc", s.DescricaoServico);
                    cmd.Parameters.AddWithValue("@min", s.DuracaoMin);
                    cmd.Parameters.AddWithValue("@max", s.DuracaoMax);
                    cmd.Parameters.AddWithValue("@precoMin", s.PrecoMin);
                    cmd.Parameters.AddWithValue("@precoMax", s.PrecoMax);
                    cmd.Parameters.AddWithValue("@id", s.CodigoServico);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM servicos WHERE codigo_servico=@id";
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
                string sql = "SELECT COUNT(*) FROM servicos";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
