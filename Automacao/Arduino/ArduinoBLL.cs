using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports; 

namespace Arduino
{
    public class ArduinoBLL
    {
        private static int display = 0; // Estado interno que reflete os 8 LEDs/Dispositivos
        private static SerialPort _serialPort; // <-- Variável para armazenar a porta serial

        // Mapeamento: Dispositivo na IHM (1-8) para Pino do Arduino (2-9)
        // O Tag que você usa na IHM é o número do Dispositivo (1 a 8), mas o Arduino usa o Pino (2 a 9).
        private static int MapearDispositivoParaPino(int dispositivo)
        {
            // Se o Dispositivo for 1, o Pino é 2. Se for 8, o Pino é 9.
            return dispositivo + 1;
        }

        // Construtor Estático para configurar a porta serial
        public static void SetSerialPort(SerialPort portaSerial)
        {
            _serialPort = portaSerial;
        }

        // =========================================================
        // MÉTODO DE COMUNICAÇÃO SERIAL
        // =========================================================

        public static void enviarComandoSerial(string pino, string estado)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                // Formato de comando que o Arduino espera: "PINO,ESTADO\n"
                string comando = $"{pino},{estado}\n";
                _serialPort.Write(comando);
            }
        }

        // =========================================================
        // MÉTODOS DE LÓGICA EXISTENTES (com integração serial)
        // =========================================================

        public static void setDisplay(int _display)
        {
            display = _display;
            // Se o display for zerado (Desligar Todos), desliga os pinos no Arduino
            if (display == 0)
            {
                for (int i = 1; i <= 8; i++) // Dispositivos 1 a 8
                {
                    enviarComandoSerial(MapearDispositivoParaPino(i).ToString(), "LOW");
                }
            }
        }

        public static int getDisplay() { return display; }

        public static String mostraBits(int _x)
        {
            // Seu método está correto para mostrar os bits (8-bit representation)
            String retorno = "";
            int aux = 128; // Começa em 2^7

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

        // O Tag (_n) agora é o número do Dispositivo (1 a 8)
        public static void ligaDispositivo(String _n)
        {
            int dispositivo = int.Parse(_n);
            int pinoArduino = MapearDispositivoParaPino(dispositivo);

            // Lógica Bitwise (seu código original - correta para o estado interno)
            int aux = 1 << (dispositivo - 1); // Dispositivo 1 (bit 0)
            display = display | aux;

            // COMANDO SERIAL
            enviarComandoSerial(pinoArduino.ToString(), "HIGH");
        }

        // O Tag (_n) agora é o número do Dispositivo (1 a 8)
        public static void desligaDispositivo(String _n)
        {
            int dispositivo = int.Parse(_n);
            int pinoArduino = MapearDispositivoParaPino(dispositivo);

            // Lógica Bitwise (seu código original - correta para o estado interno)
            int aux = 1 << (dispositivo - 1);
            display = display & ~aux;

            // COMANDO SERIAL
            enviarComandoSerial(pinoArduino.ToString(), "LOW");
        }
    }
}