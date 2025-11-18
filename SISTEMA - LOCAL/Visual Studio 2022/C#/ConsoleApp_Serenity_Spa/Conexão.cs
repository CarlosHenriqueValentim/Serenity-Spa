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
        public static string dadosConexao = "server=localhost;uid=root;pwd=root;database=serenity_spa;port=3306";

        public static MySqlConnection Conectar()
        {
            MySqlConnection conexao = new MySqlConnection(dadosConexao);
            try
            {
                conexao.Open();
                return conexao;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao conectar: " + ex.Message);
                return null;
            }
        }

        public static void Desconectar(MySqlConnection conexao)
        {
            if (conexao != null)
            {
                try
                {
                    if (conexao.State == System.Data.ConnectionState.Open)
                        conexao.Close();
                    conexao.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao desconectar: " + ex.Message);
                }
            }
        }
    }
}
