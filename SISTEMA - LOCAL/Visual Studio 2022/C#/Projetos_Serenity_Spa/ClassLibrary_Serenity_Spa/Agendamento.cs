using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class Agendamento
    {
        public int CodigoAgendamento { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoFuncionario { get; set; }
        public int CodigoServico { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Duracao { get; set; }
        public string Status { get; set; } // agendado, concluido, cancelado
    }

    public class AgendamentoDAO
    {
        public void Inserir(Agendamento a)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO agendamentos 
                               (codigo_empresa, codigo_clientes, codigo_funcionario, codigo_servico, data, duracao_agendamento, status)
                               VALUES (@empresa, @cliente, @funcionario, @servico, @data, @duracao, @status)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@empresa", a.CodigoEmpresa);
                    cmd.Parameters.AddWithValue("@cliente", a.CodigoCliente);
                    cmd.Parameters.AddWithValue("@funcionario", a.CodigoFuncionario);
                    cmd.Parameters.AddWithValue("@servico", a.CodigoServico);
                    cmd.Parameters.AddWithValue("@data", a.Data);
                    cmd.Parameters.AddWithValue("@duracao", a.Duracao);
                    cmd.Parameters.AddWithValue("@status", a.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Agendamento> Listar()
        {
            var lista = new List<Agendamento>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM agendamentos";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Agendamento
                        {
                            CodigoAgendamento = dr.GetInt32("codigo_agendamento"),
                            CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                            CodigoCliente = dr.GetInt32("codigo_clientes"),
                            CodigoFuncionario = dr.GetInt32("codigo_funcionario"),
                            CodigoServico = dr.GetInt32("codigo_servico"),
                            Data = dr.GetDateTime("data"),
                            Duracao = dr.GetTimeSpan("duracao_agendamento"),
                            Status = dr.GetString("status")
                        });
                    }
                }
            }
            return lista;
        }

        public Agendamento BuscarPorId(int id)
        {
            Agendamento a = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM agendamentos WHERE codigo_agendamento=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            a = new Agendamento
                            {
                                CodigoAgendamento = dr.GetInt32("codigo_agendamento"),
                                CodigoEmpresa = dr.GetInt32("codigo_empresa"),
                                CodigoCliente = dr.GetInt32("codigo_clientes"),
                                CodigoFuncionario = dr.GetInt32("codigo_funcionario"),
                                CodigoServico = dr.GetInt32("codigo_servico"),
                                Data = dr.GetDateTime("data"),
                                Duracao = dr.GetTimeSpan("duracao_agendamento"),
                                Status = dr.GetString("status")
                            };
                        }
                    }
                }
            }
            return a;
        }

        public void Atualizar(Agendamento a)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE agendamentos SET 
                               codigo_clientes=@cliente, codigo_funcionario=@funcionario, codigo_servico=@servico,
                               data=@data, duracao_agendamento=@duracao, status=@status
                               WHERE codigo_agendamento=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@cliente", a.CodigoCliente);
                    cmd.Parameters.AddWithValue("@funcionario", a.CodigoFuncionario);
                    cmd.Parameters.AddWithValue("@servico", a.CodigoServico);
                    cmd.Parameters.AddWithValue("@data", a.Data);
                    cmd.Parameters.AddWithValue("@duracao", a.Duracao);
                    cmd.Parameters.AddWithValue("@status", a.Status);
                    cmd.Parameters.AddWithValue("@id", a.CodigoAgendamento);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM agendamentos WHERE codigo_agendamento=@id";
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
                string sql = "SELECT COUNT(*) FROM agendamentos";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
