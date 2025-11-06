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
        private void InserirLogNoBanco(char check, char taligado, Button generico)
        {
            ArduinoBLL.conectaDAL();
            if (check == 't')
            {
                Log.dispositivo = "Todos foram Desligados";
                Log.operacao = "Desligar Todos";
                Log.data = DateTime.Now.ToString("dd/MM/yyyy");
                Log.hora = DateTime.Now.ToString("HH:mm");
            }
            else
            {
                if (taligado == 's')
                {
                    Log.dispositivo = generico.Tag.ToString(); 
                    Log.operacao = "Ligar";
                    Log.data = DateTime.Now.ToString("dd/MM/yyyy");
                    Log.hora = DateTime.Now.ToString("HH:mm");
                }
                else
                {
                    Log.dispositivo = generico.Tag.ToString();
                    Log.operacao = "Desligar";
                    Log.data = DateTime.Now.ToString("dd/MM/yyyy");
                    Log.hora = DateTime.Now.ToString("HH:mm");
                }
            }
            ArduinoBLL.inserirUmaLog();
            
        }

        private void tratarBotoes(object sender, EventArgs e)
        {
            Button generico = (Button)sender;
            char check = 'u', taligado;
            
            string pinoTag = generico.Tag.ToString();

            if (generico.Text == "Ligar")
            {
                taligado = 's';
                generico.Text = "Desligar";
                ArduinoBLL.ligaDispositivo(pinoTag);
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
                InserirLogNoBanco(check, taligado, generico);

            }
            else
            {
                taligado = 'n';
                generico.Text = "Ligar";
                ArduinoBLL.desligaDispositivo(pinoTag);
                textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());
                InserirLogNoBanco(check, taligado, generico);
            }
            ArduinoBLL.desconectaDAL();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            char check = 't', taligado = 'n';
            Button generico = (Button)sender;   
            ArduinoBLL.setDisplay(0); 
            textBox1.Text = ArduinoBLL.mostraBits(ArduinoBLL.getDisplay());

            button1.Text = "Ligar";
            button2.Text = "Ligar";
            button3.Text = "Ligar";
            button4.Text = "Ligar";
            button5.Text = "Ligar";
            button6.Text = "Ligar";
            button7.Text = "Ligar";
            button8.Text = "Ligar";

            for (int i = 2; i <= 9; i++) 
            {
                ArduinoBLL.enviarComandoSerial(i.ToString(), "LOW");
            }
            InserirLogNoBanco(check, taligado, generico);

            ArduinoBLL.desconectaDAL();
        }
    }
}
