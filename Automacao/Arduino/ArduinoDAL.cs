using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino
{
    internal class ArduinoDAL
    {
        private static String strConexao = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\unisanta\\Desktop\\Desafio-do-Asenjo\\Automacao\\Arduino\\bin\\Debug\\BD.accdb;";
        private static OleDbConnection conn = new OleDbConnection(strConexao);
        private static OleDbCommand strSQL;
        private static OleDbDataReader result;

        public static void conecta()
        {
            try
            {
                conn.Open();
            }
            catch (Exception)
            {
                Erro.setMsg("Problemas ao se conectar ao Banco de Dados");
            }
        }

        public static void desconecta()
        {
            conn.Close();
        }


        public static void inseriUmLog()
        {
            String aux = "insert into TabLog(dispositivo,operacao,data,hora) values ('" + Log.getDispositivo() + "','" + Log.getOperacao() + "','" + Log.getData() + "','" + Log.getHora() + "')";

            strSQL = new OleDbCommand(aux, conn);
            strSQL.ExecuteNonQuery();
        }
    }
}
