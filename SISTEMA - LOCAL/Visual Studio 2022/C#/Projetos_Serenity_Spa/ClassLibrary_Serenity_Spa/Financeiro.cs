using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class Financeiro
    {
        public int CodigoFinanceiro { get; set; }
        public int CodigoEmpresa { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }

    public class FinanceiroDAO
    {
        public void Inserir(Financeiro f)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO financeiro 
                               (codigo_empresa, tipo_financeiro, descricao_financeiro, valor_financeiro, data_financeiro)
                               VALUES (@empresa, @tipo, @descricao, @valor, @data)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", f.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@tipo", f.Tipo);
                    cmd.Parameters.AddWithValue("@descricao", f.Descricao);
                    cmd.Parameters.AddWithValue("@valor", f.Valor);
                    cmd.Parameters.AddWithValue("@data", f.Data);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Financeiro> Listar()
        {
            var lista = new List<Financeiro>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM financeiro";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Financeiro
                        {
                            CodigoFinanceiro = dr.GetInt32("codigo_financeiro"),
                            CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                            Tipo = dr.GetString("tipo_financeiro"),
                            Descricao = dr.GetString("descricao_financeiro"),
                            Valor = dr.GetDecimal("valor_financeiro"),
                            Data = dr.GetDateTime("data_financeiro")
                        });
                    }
                }
            }
            return lista;
        }

        public Financeiro BuscarPorId(int id)
        {
            Financeiro f = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM financeiro WHERE codigo_financeiro=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            f = new Financeiro
                            {
                                CodigoFinanceiro = dr.GetInt32("codigo_financeiro"),
                                CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                                Tipo = dr.GetString("tipo_financeiro"),
                                Descricao = dr.GetString("descricao_financeiro"),
                                Valor = dr.GetDecimal("valor_financeiro"),
                                Data = dr.GetDateTime("data_financeiro")
                            };
                        }
                    }
                }
            }
            return f;
        }

        public void Atualizar(Financeiro f)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE financeiro SET 
                               codigo_empresa=@empresa, tipo_financeiro=@tipo, descricao_financeiro=@descricao, valor_financeiro=@valor, data_financeiro=@data
                               WHERE codigo_financeiro=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", f.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@tipo", f.Tipo);
                    cmd.Parameters.AddWithValue("@descricao", f.Descricao);
                    cmd.Parameters.AddWithValue("@valor", f.Valor);
                    cmd.Parameters.AddWithValue("@data", f.Data);
                    cmd.Parameters.AddWithValue("@id", f.CodigoFinanceiro);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM financeiro WHERE codigo_financeiro=@id";
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
                string sql = "SELECT COUNT(*) FROM financeiro";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
