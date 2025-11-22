using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerenitySpa
{
    public class Database
    {
        private static string conexao = "server=localhost;database=serenity_spa;uid=root;pwd=root;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(conexao);
        }
    }
}
