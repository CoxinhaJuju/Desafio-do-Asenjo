using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino
{
    public partial class ArduinoIHM : Form
    {
        public ArduinoIHM()
        {
            InitializeComponent();
            ArduinoBLL.SetSerialPort(serialPort1);
            try
            {
                serialPort1.Open();
               MessageBox.Show("Porta serial COM5 conectada com sucesso.");
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro ao se conectar a porta COM5 (" + erro.Message + ")");
            }
        }

        private void tratarBotoes(object sender, EventArgs e)
        {
            Button generico = (Button)sender;

            // O Tag do botão deve conter o NÚMERO DO PINO DIGITAL DO ARDUINO (ex: "2", "3", "9", etc.)
            string pinoTag = generico.Tag.ToString();

            if (generico.Text == "Ligar")
            {
                generico.Text = "Desligar";
                // Chama a BLL com o pino e o estado "HIGH"
                ArduinoBLL.ligaDispositivo(pinoTag);
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
            }
            else
            {
                generico.Text = "Ligar";
                // Chama a BLL com o pino e o estado "LOW"
                ArduinoBLL.desligaDispositivo(pinoTag);
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Botão "Desligar todos os dispositivos"
            ArduinoBLL.setDisplay(0); // Zera o estado interno da BLL
            textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());

            // (Seu código para resetar o texto dos botões está correto)
            button1.Text = "Ligar";
            button2.Text = "Ligar";
            button3.Text = "Ligar";
            button4.Text = "Ligar";
            button5.Text = "Ligar";
            button6.Text = "Ligar";
            button7.Text = "Ligar";
            button8.Text = "Ligar";

            // Envia o comando para desligar todos os 8 pinos no Arduino
            for (int i = 2; i <= 9; i++) // Pinos 2 a 9
            {
                ArduinoBLL.enviarComandoSerial(i.ToString(), "LOW");
            }
        }
    }
}
