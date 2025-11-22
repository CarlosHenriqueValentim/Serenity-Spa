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
        public int CodigoCliente { get; set; }
        public int CodigoFuncionario { get; set; }
        public int CodigoServico { get; set; }
        public DateTime Data { get; set; }
        public string Status { get; set; }
        public int Duracao { get; set; }
    }

    public class AgendamentoDAO
    {
        public List<Agendamento> Listar()
        {
            List<Agendamento> lista = new List<Agendamento>();

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT * FROM agendamentos ORDER BY data DESC";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Agendamento a = new Agendamento();
                        a.CodigoAgendamento = dr.GetInt32("codigo_agendamento");
                        a.CodigoCliente = dr.GetInt32("codigo_clientes");
                        a.CodigoFuncionario = dr.GetInt32("codigo_funcionario");
                        a.CodigoServico = dr.GetInt32("codigo_servico");
                        a.Status = dr.GetString("status");
                        a.Duracao = dr.GetInt32("duracao_agendamento");
                        a.Data = dr.GetDateTime("data");

                        lista.Add(a);
                    }
                }
            }

            return lista;
        }

        public int Contar()
        {
            int total = 0;

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM agendamentos";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return total;
        }
    }
}
