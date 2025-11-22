using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class ItensPacote
    {
        public int CodigoItensPacote { get; set; }
        public int CodigoPacote { get; set; }
        public int CodigoServico { get; set; }
        public int OrdemExecucao { get; set; }
        public int Quantidade { get; set; }
    }

    public class ItensPacoteDAO
    {
        public void Inserir(ItensPacote i)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO itens_pacote 
                               (codigo_pacote, codigo_servico, ordem_execucao_itens_pacote, quantidade_itens_pacote)
                               VALUES (@pacote, @servico, @ordem, @quantidade)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pacote", i.CodigoPacote);
                    cmd.Parameters.AddWithValue("@servico", i.CodigoServico);
                    cmd.Parameters.AddWithValue("@ordem", i.OrdemExecucao);
                    cmd.Parameters.AddWithValue("@quantidade", i.Quantidade);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ItensPacote> Listar()
        {
            var lista = new List<ItensPacote>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM itens_pacote";
                using (var cmd = new MySqlCommand(sql, conn))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ItensPacote
                        {
                            CodigoItensPacote = dr.GetInt32("codigo_itens_pacote"),
                            CodigoPacote = dr.GetInt32("codigo_pacote"),
                            CodigoServico = dr.GetInt32("codigo_servico"),
                            OrdemExecucao = dr.GetInt32("ordem_execucao_itens_pacote"),
                            Quantidade = dr.GetInt32("quantidade_itens_pacote")
                        });
                    }
                }
            }
            return lista;
        }

        public ItensPacote BuscarPorId(int id)
        {
            ItensPacote i = null;
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM itens_pacote WHERE codigo_itens_pacote=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            i = new ItensPacote
                            {
                                CodigoItensPacote = dr.GetInt32("codigo_itens_pacote"),
                                CodigoPacote = dr.GetInt32("codigo_pacote"),
                                CodigoServico = dr.GetInt32("codigo_servico"),
                                OrdemExecucao = dr.GetInt32("ordem_execucao_itens_pacote"),
                                Quantidade = dr.GetInt32("quantidade_itens_pacote")
                            };
                        }
                    }
                }
            }
            return i;
        }

        public void Atualizar(ItensPacote i)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE itens_pacote SET codigo_pacote=@pacote, codigo_servico=@servico, ordem_execucao_itens_pacote=@ordem, quantidade_itens_pacote=@quantidade 
                               WHERE codigo_itens_pacote=@id";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@pacote", i.CodigoPacote);
                    cmd.Parameters.AddWithValue("@servico", i.CodigoServico);
                    cmd.Parameters.AddWithValue("@ordem", i.OrdemExecucao);
                    cmd.Parameters.AddWithValue("@quantidade", i.Quantidade);
                    cmd.Parameters.AddWithValue("@id", i.CodigoItensPacote);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM itens_pacote WHERE codigo_itens_pacote=@id";
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
                string sql = "SELECT COUNT(*) FROM itens_pacote";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
