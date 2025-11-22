using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_Serenity_Spa
{
    #region Config
    public static class DbConfig
    {
        public static string ConnectionString { get; set; } =
            "Server=localhost;Port=3306;Database=Serenity_Spa;Uid=root;Pwd=;SslMode=None;";
    }

    internal static class Db
    {
        internal static MySqlConnection OpenConnection()
        {
            var c = new MySqlConnection(DbConfig.ConnectionString);
            c.Open();
            return c;
        }

        internal static string SafeGetString(MySqlDataReader r, string name)
        {
            int i = r.GetOrdinal(name);
            return r.IsDBNull(i) ? null : r.GetString(i);
        }

        internal static int SafeGetInt(MySqlDataReader r, string name)
        {
            int i = r.GetOrdinal(name);
            return r.IsDBNull(i) ? 0 : r.GetInt32(i);
        }

        internal static decimal SafeGetDecimal(MySqlDataReader r, string name)
        {
            int i = r.GetOrdinal(name);
            return r.IsDBNull(i) ? 0m : r.GetDecimal(i);
        }

        internal static DateTime SafeGetDateTime(MySqlDataReader r, string name)
        {
            int i = r.GetOrdinal(name);
            return r.IsDBNull(i) ? DateTime.MinValue : r.GetDateTime(i);
        }

        internal static TimeSpan SafeGetTimeSpan(MySqlDataReader r, string name)
        {
            int i = r.GetOrdinal(name);
            return r.IsDBNull(i) ? TimeSpan.Zero : r.GetTimeSpan(i);
        }
    }
    #endregion

    #region Models

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

    public class Servico
    {
        public int CodigoServico { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeServico { get; set; }
        public string DescricaoServico { get; set; }
        public int DuracaoMinServico { get; set; }
        public int DuracaoMaxServico { get; set; }
        public decimal PrecoMinServico { get; set; }
        public decimal PrecoMaxServico { get; set; }
    }

    public class Pacote
    {
        public int CodigoPacote { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomePacote { get; set; }
        public string DescricaoPacote { get; set; }
        public decimal PrecoPacote { get; set; }
        public int DuracaoPacote { get; set; }
        public decimal ValorPorPessoaExtraPacote { get; set; }
    }

    public class ItensPacote
    {
        public int CodigoItensPacote { get; set; }
        public int CodigoPacote { get; set; }
        public int CodigoServico { get; set; }
        public int OrdemExecucaoItensPacote { get; set; }
        public int QuantidadeItensPacote { get; set; }
    }

    public class ProdutoEstoque
    {
        public int CodigoProduto { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeProdutoEstoque { get; set; }
        public string CategoriaProdutoEstoque { get; set; }
        public int QuantidadeProdutoEstoque { get; set; }
        public decimal PrecoUnitarioProdutoEstoque { get; set; }
        public decimal PrecoTotalProdutoEstoque { get; set; }
        public string FornecedorProdutoEstoque { get; set; }
    }

    public class Financeiro
    {
        public int CodigoFinanceiro { get; set; }
        public int CodigoEmpresa { get; set; }
        public string TipoFinanceiro { get; set; }
        public string DescricaoFinanceiro { get; set; }
        public decimal ValorFinanceiro { get; set; }
        public DateTime DataFinanceiro { get; set; }
    }

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

    public class Cliente
    {
        public int CodigoCliente { get; set; }
        public int CodigoEmpresa { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataNascCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public string EmailCliente { get; set; }
        public string SenhaCliente { get; set; }
        public char SexoCliente { get; set; }
    }

    public class Agendamento
    {
        public int CodigoAgendamento { get; set; }
        public int CodigoEmpresa { get; set; }
        public int CodigoCliente { get; set; }
        public int CodigoFuncionario { get; set; }
        public int CodigoServico { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan Hora { get; set; }
        public string Status { get; set; }
    }

    #endregion

    #region Repository Cliente

    public class ClienteRepository
    {
        public IEnumerable<Cliente> ListAll()
        {
            var lista = new List<Cliente>();

            using var conn = Db.OpenConnection();
            using var cmd = new MySqlCommand("SELECT * FROM clientes", conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                lista.Add(new Cliente
                {
                    CodigoCliente = Db.SafeGetInt(r, "codigo_cliente"),
                    CodigoEmpresa = Db.SafeGetInt(r, "codigo_empresa"),
                    NomeCliente = Db.SafeGetString(r, "nome_cliente"),
                    DataNascCliente = Db.SafeGetDateTime(r, "data_nasc_cliente"),
                    TelefoneCliente = Db.SafeGetString(r, "telefone_cliente"),
                    EmailCliente = Db.SafeGetString(r, "email_cliente"),
                    SenhaCliente = Db.SafeGetString(r, "senha_cliente"),
                    SexoCliente = Db.SafeGetString(r, "sexo_cliente")?[0] ?? 'N'
                });
            }

            return lista;
        }

        public int Create(Cliente c)
        {
            using var conn = Db.OpenConnection();
            using var cmd = new MySqlCommand(@"
            INSERT INTO clientes 
            (codigo_empresa,nome_cliente,data_nasc_cliente,telefone_cliente,email_cliente,senha_cliente,sexo_cliente)
            VALUES (@empresa,@nome,@data,@tel,@email,@senha,@sexo);
            SELECT LAST_INSERT_ID();", conn);

            cmd.Parameters.AddWithValue("@empresa", c.CodigoEmpresa);
            cmd.Parameters.AddWithValue("@nome", c.NomeCliente);
            cmd.Parameters.AddWithValue("@data", c.DataNascCliente);
            cmd.Parameters.AddWithValue("@tel", c.TelefoneCliente);
            cmd.Parameters.AddWithValue("@email", c.EmailCliente);
            cmd.Parameters.AddWithValue("@senha", c.SenhaCliente);
            cmd.Parameters.AddWithValue("@sexo", c.SexoCliente);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool Delete(int id)
        {
            using var conn = Db.OpenConnection();
            using var cmd = new MySqlCommand("DELETE FROM clientes WHERE codigo_cliente=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    #endregion

    #region Repository Agendamento

    public class AgendamentoRepository
    {
        public IEnumerable<Agendamento> ListAll()
        {
            var lista = new List<Agendamento>();

            using var conn = Db.OpenConnection();
            using var cmd = new MySqlCommand("SELECT * FROM agendamentos", conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                lista.Add(new Agendamento
                {
                    CodigoAgendamento = Db.SafeGetInt(r, "codigo_agendamento"),
                    CodigoEmpresa = Db.SafeGetInt(r, "codigo_empresa"),
                    CodigoCliente = Db.SafeGetInt(r, "codigo_cliente"),
                    CodigoFuncionario = Db.SafeGetInt(r, "codigo_funcionario"),
                    CodigoServico = Db.SafeGetInt(r, "codigo_servico"),
                    Data = Db.SafeGetDateTime(r, "data_agendamento"),
                    Hora = Db.SafeGetTimeSpan(r, "hora_agendamento"),
                    Status = Db.SafeGetString(r, "status_agendamento")
                });
            }

            return lista;
        }

        public int Create(Agendamento ag)
        {
            using var conn = Db.OpenConnection();
            using var cmd = new MySqlCommand(@"
            INSERT INTO agendamentos
            (codigo_empresa,codigo_cliente,codigo_funcionario,codigo_servico,data_agendamento,hora_agendamento,status_agendamento)
            VALUES (@emp,@cli,@func,@srv,@data,@hora,@status);
            SELECT LAST_INSERT_ID();", conn);

            cmd.Parameters.AddWithValue("@emp", ag.CodigoEmpresa);
            cmd.Parameters.AddWithValue("@cli", ag.CodigoCliente);
            cmd.Parameters.AddWithValue("@func", ag.CodigoFuncionario);
            cmd.Parameters.AddWithValue("@srv", ag.CodigoServico);
            cmd.Parameters.AddWithValue("@data", ag.Data);
            cmd.Parameters.AddWithValue("@hora", ag.Hora);
            cmd.Parameters.AddWithValue("@status", ag.Status);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool UpdateStatus(int id, string status)
        {
            using var conn = Db.OpenConnection();
            using var cmd = new MySqlCommand(
                "UPDATE agendamentos SET status_agendamento=@status WHERE codigo_agendamento=@id",
                conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", status);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var conn = Db.OpenConnection();
            using var cmd = new MySqlCommand("DELETE FROM agendamentos WHERE codigo_agendamento=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            return cmd.ExecuteNonQuery() > 0;
        }
    }

    #endregion
}
