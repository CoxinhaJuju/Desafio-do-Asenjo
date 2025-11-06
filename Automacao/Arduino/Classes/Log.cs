using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arduino
{
    public class Log
    {
        public static String dispositivo;
        public static String operacao;
        public static String data;
        public static String hora;

        public static void setDispositivo(String _dispositivo) { dispositivo = _dispositivo; }
        public static void setOperacao(String _operacao) { operacao = _operacao; }
        public static void setData(String _data) { data = _data; }
        public static void setHora(String _hora) { hora = _hora; }

        public static String getDispositivo() { return dispositivo; }
        public static String getOperacao() { return operacao; }
        public static String getData() { return data; }
        public static String getHora() { return hora; }
    }
}
