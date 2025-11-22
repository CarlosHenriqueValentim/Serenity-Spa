using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
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

    public class EmpresaDAO
    {
        public void Inserir(Empresa e)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO empresa (razao_social, nome_fantasia, cnpj_empresa, localizacao_empresa, telefone_empresa, email_empresa, site_empresa)
                               VALUES (@razao, @fantasia, @cnpj, @local, @telefone, @email, @site)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@razao", e.RazaoSocial);
                    cmd.Parameters.AddWithValue("@fantasia", e.NomeFantasia);
                    cmd.Parameters.AddWithValue("@cnpj", e.CnpjEmpresa);
                    cmd.Parameters.AddWithValue("@local", e.LocalizacaoEmpresa);
                    cmd.Parameters.AddWithValue("@telefone", e.TelefoneEmpresa);
                    cmd.Parameters.AddWithValue("@email", e.EmailEmpresa);
                    cmd.Parameters.AddWithValue("@site", e.SiteEmpresa);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Empresa> Listar()
        {
            var lista = new List<Empresa>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM empresa";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Empresa
                        {
                            CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                            RazaoSocial = dr.GetString("razao_social"),
                            NomeFantasia = dr.GetString("nome_fantasia"),
                            CnpjEmpresa = dr.GetString("cnpj_empresa"),
                            LocalizacaoEmpresa = dr.GetString("localizacao_empresa"),
                            TelefoneEmpresa = dr.GetString("telefone_empresa"),
                            EmailEmpresa = dr.GetString("email_empresa"),
                            SiteEmpresa = dr.GetString("site_empresa")
                        });
                    }
                }
            }
            return lista;
        }

        public Empresa BuscarPorId(int id)
        {
            Empresa e = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM empresa WHERE codigo_empresa=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            e = new Empresa
                            {
                                CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                                RazaoSocial = dr.GetString("razao_social"),
                                NomeFantasia = dr.GetString("nome_fantasia"),
                                CnpjEmpresa = dr.GetString("cnpj_empresa"),
                                LocalizacaoEmpresa = dr.GetString("localizacao_empresa"),
                                TelefoneEmpresa = dr.GetString("telefone_empresa"),
                                EmailEmpresa = dr.GetString("email_empresa"),
                                SiteEmpresa = dr.GetString("site_empresa")
                            };
                        }
                    }
                }
            }
            return e;
        }

        public void Atualizar(Empresa e)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE empresa SET razao_social=@razao, nome_fantasia=@fantasia, cnpj_empresa=@cnpj, localizacao_empresa=@local, telefone_empresa=@telefone, email_empresa=@email, site_empresa=@site
                               WHERE codigo_empresa=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@razao", e.RazaoSocial);
                    cmd.Parameters.AddWithValue("@fantasia", e.NomeFantasia);
                    cmd.Parameters.AddWithValue("@cnpj", e.CnpjEmpresa);
                    cmd.Parameters.AddWithValue("@local", e.LocalizacaoEmpresa);
                    cmd.Parameters.AddWithValue("@telefone", e.TelefoneEmpresa);
                    cmd.Parameters.AddWithValue("@email", e.EmailEmpresa);
                    cmd.Parameters.AddWithValue("@site", e.SiteEmpresa);
                    cmd.Parameters.AddWithValue("@id", e.CodigoEmpresa);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM empresa WHERE codigo_empresa=@id";
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
                string sql = "SELECT COUNT(*) FROM empresa";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
