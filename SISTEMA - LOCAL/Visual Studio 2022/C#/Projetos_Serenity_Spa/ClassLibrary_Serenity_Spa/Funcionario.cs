using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class Funcionario
    {
        public int CodigoFuncionario { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeFuncionario { get; set; }
        public string CargoFuncionario { get; set; }
        public string TelefoneFuncionario { get; set; }
        public string EmailFuncionario { get; set; }
        public string SenhaFuncionario { get; set; }
    }

    public class FuncionarioDAO
    {
        public void Inserir(Funcionario f)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO funcionarios 
                               (codigo_empresa, nome_funcionario, cargo_funcionario, telefone_funcionario, email_funcionario, senha_funcionario)
                               VALUES (@empresa, @nome, @cargo, @telefone, @email, @senha)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", f.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@nome", f.NomeFuncionario);
                    cmd.Parameters.AddWithValue("@cargo", f.CargoFuncionario);
                    cmd.Parameters.AddWithValue("@telefone", f.TelefoneFuncionario);
                    cmd.Parameters.AddWithValue("@email", f.EmailFuncionario);
                    cmd.Parameters.AddWithValue("@senha", f.SenhaFuncionario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Funcionario> Listar()
        {
            var lista = new List<Funcionario>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM funcionarios";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Funcionario
                        {
                            CodigoFuncionario = dr.GetInt32("codigo_funcionario"),
                            CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                            NomeFuncionario = dr.GetString("nome_funcionario"),
                            CargoFuncionario = dr.GetString("cargo_funcionario"),
                            TelefoneFuncionario = dr.GetString("telefone_funcionario"),
                            EmailFuncionario = dr.GetString("email_funcionario"),
                            SenhaFuncionario = dr.GetString("senha_funcionario")
                        });
                    }
                }
            }
            return lista;
        }

        public Funcionario BuscarPorId(int id)
        {
            Funcionario f = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM funcionarios WHERE codigo_funcionario=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            f = new Funcionario
                            {
                                CodigoFuncionario = dr.GetInt32("codigo_funcionario"),
                                CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                                NomeFuncionario = dr.GetString("nome_funcionario"),
                                CargoFuncionario = dr.GetString("cargo_funcionario"),
                                TelefoneFuncionario = dr.GetString("telefone_funcionario"),
                                EmailFuncionario = dr.GetString("email_funcionario"),
                                SenhaFuncionario = dr.GetString("senha_funcionario")
                            };
                        }
                    }
                }
            }
            return f;
        }

        public void Atualizar(Funcionario f)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE funcionarios SET 
                               nome_funcionario=@nome, cargo_funcionario=@cargo, telefone_funcionario=@telefone, email_funcionario=@email, senha_funcionario=@senha
                               WHERE codigo_funcionario=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", f.NomeFuncionario);
                    cmd.Parameters.AddWithValue("@cargo", f.CargoFuncionario);
                    cmd.Parameters.AddWithValue("@telefone", f.TelefoneFuncionario);
                    cmd.Parameters.AddWithValue("@email", f.EmailFuncionario);
                    cmd.Parameters.AddWithValue("@senha", f.SenhaFuncionario);
                    cmd.Parameters.AddWithValue("@id", f.CodigoFuncionario);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM funcionarios WHERE codigo_funcionario=@id";
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
                string sql = "SELECT COUNT(*) FROM funcionarios";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
