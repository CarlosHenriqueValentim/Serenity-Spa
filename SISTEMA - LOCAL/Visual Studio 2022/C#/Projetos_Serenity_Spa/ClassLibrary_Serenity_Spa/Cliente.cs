using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }
        public string NomeCliente { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
    }

    public class ClienteDAO
    {
        public void Inserir(Cliente c)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "INSERT INTO clientes (nome_cliente, email, telefone) VALUES (@nome, @email, @telefone)";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", c.NomeCliente);
                    cmd.Parameters.AddWithValue("@email", c.Email);
                    cmd.Parameters.AddWithValue("@telefone", c.Telefone);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM clientes";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Cliente c = new Cliente();
                        c.CodigoCliente = dr.GetInt32("codigo_cliente");
                        c.NomeCliente = dr.GetString("nome_cliente");
                        c.Email = dr.GetString("email");
                        c.Telefone = dr.GetString("telefone");

                        lista.Add(c);
                    }
                }
            }
            return lista;
        }

        public Cliente BuscarPorId(int id)
        {
            Cliente c = null;

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT * FROM clientes WHERE codigo_cliente = @id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            c = new Cliente();
                            c.CodigoCliente = dr.GetInt32("codigo_cliente");
                            c.NomeCliente = dr.GetString("nome_cliente");
                            c.Email = dr.GetString("email");
                            c.Telefone = dr.GetString("telefone");
                        }
                    }
                }
            }
            return c;
        }

        public void Atualizar(Cliente c)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "UPDATE clientes SET nome_cliente=@nome, email=@email, telefone=@telefone WHERE codigo_cliente=@id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", c.NomeCliente);
                    cmd.Parameters.AddWithValue("@email", c.Email);
                    cmd.Parameters.AddWithValue("@telefone", c.Telefone);
                    cmd.Parameters.AddWithValue("@id", c.CodigoCliente);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Excluir(int id)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "DELETE FROM clientes WHERE codigo_cliente=@id";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int Contar()
        {
            int total = 0;

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM clientes";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
