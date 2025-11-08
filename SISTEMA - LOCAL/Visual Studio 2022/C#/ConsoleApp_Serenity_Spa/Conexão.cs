using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp_SerenitySpa
{
    public static class Conexao
    {
        private const string StringConexao = "server=127.0.0.1;uid=root;pwd=root;database=Serenity_Spa;port=3306";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(StringConexao);
        }
    }
}