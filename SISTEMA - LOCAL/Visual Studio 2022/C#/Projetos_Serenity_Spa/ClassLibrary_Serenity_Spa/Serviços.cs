using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class ServicoDAO
    {
        public int Contar()
        {
            int total = 0;

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string sql = "SELECT COUNT(*) FROM servicos";
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            return total;
        }
    }
}
