using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Arduino;

namespace Arduino
{
    public class ArduinoBLL
    {
        private static int display = 0; 
        private static SerialPort _serialPort; 
       
        /*
        --------------------------------------------------|
            Conexão com o Banco de Dados e inserir uma Log|
        --------------------------------------------------|
        */

        public static void conectaDAL()
        {
            ArduinoDAL.conecta();
        }

        public static void desconectaDAL()
        {
            ArduinoDAL.desconecta();
        }
        public static void inserirUmaLog()
        {
            ArduinoDAL.inseriUmLog();
        }

        /*
        --------------------------------------------------|
            Conexão com o SerialPort                      |
        --------------------------------------------------|
        */

        private static int MapearDispositivoParaPino(int dispositivo)
        {
            
            return dispositivo + 1;
        }

        
        public static void SetSerialPort(SerialPort portaSerial)
        {
            _serialPort = portaSerial;
        }

     

        public static void enviarComandoSerial(string pino, string estado)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                
                string comando = $"{pino},{estado}\n";
                _serialPort.Write(comando);
            }
        }

        

        public static void setDisplay(int _display)
        {
            display = _display;
            
            if (display == 0)
            {
                for (int i = 1; i <= 8; i++) 
                {
                    enviarComandoSerial(MapearDispositivoParaPino(i).ToString(), "LOW");
                }
            }
        }

        public static int getDisplay() { return display; }

        public static String mostraBits(int _x)
        {
            
            String retorno = "";
            int aux = 128; 

            for (int i = 0; i < 8; ++i)
            {
                if ((_x & aux) != 0)
                    retorno += "1";
                else
                    retorno += "0";
                aux = aux >> 1;
            }
            return retorno;
        }

        
        public static void ligaDispositivo(String _n)
        {
            int dispositivo = int.Parse(_n);
            int pinoArduino = MapearDispositivoParaPino(dispositivo);

            
            int aux = 1 << (dispositivo - 1); 
            display = display | aux;

            
            enviarComandoSerial(pinoArduino.ToString(), "HIGH");
        }

        
        public static void desligaDispositivo(String _n)
        {
            int dispositivo = int.Parse(_n);
            int pinoArduino = MapearDispositivoParaPino(dispositivo);

            
            int aux = 1 << (dispositivo - 1);
            display = display & ~aux;

            
            enviarComandoSerial(pinoArduino.ToString(), "LOW");
        }
    }
}