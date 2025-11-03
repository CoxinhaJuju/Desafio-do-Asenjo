// =========================================================
// CÓDIGO ARDUINO: INTERPRETAÇÃO DE COMANDOS C#
// =========================================================

// Mapeamento dos pinos que você está usando (Dispositivos 1-8 na IHM)
// Estes correspondem aos pinos digitais 2 ao 9.
const int pinosLEDs[] = {2, 3, 4, 5, 6, 7, 8, 9};
const int numLEDs = sizeof(pinosLEDs) / sizeof(pinosLEDs[0]);

void setup() {
  // Inicializa a comunicação serial. 
  // A taxa de baud (9600) DEVE ser a mesma que você usa no C# e no SimulIDE!
  Serial.begin(9600); 

  // Configura todos os pinos como OUTPUT e garante que estão desligados
  for (int i = 0; i < numLEDs; i++) {
    pinMode(pinosLEDs[i], OUTPUT);
    digitalWrite(pinosLEDs[i], LOW); 
  }

  // Opcional: Envia uma mensagem inicial para o Monitor Serial do SimulIDE
  
}

void loop() {
  // Verifica se o programa C# enviou algum dado
  if (Serial.available() > 0) {
    
    // 1. Lê a string completa enviada pelo C# (até o '\n')
    // Exemplo: comando será "5,HIGH"
    String comando = Serial.readStringUntil('\n'); 
    comando.trim(); // Remove espaços em branco
    
    // 2. Procura pela vírgula (,) que separa o Pino do Estado
    int indiceVirgula = comando.indexOf(',');

    // Se encontramos a vírgula, processamos
    if (indiceVirgula > 0) {
      
      // 3. Extrai o número do pino (ex: "5")
      String pinoStr = comando.substring(0, indiceVirgula);
      int pino = pinoStr.toInt(); // Converte a String para o número inteiro

      // 4. Extrai o estado (ex: "HIGH" ou "LOW")
      // Usa .toUpperCase() para garantir que a leitura funciona com HIGH/high/High
      String estadoStr = comando.substring(indiceVirgula + 1);
      estadoStr.toUpperCase();
      
      // 5. Converte a String do estado para o formato do Arduino (HIGH/LOW)
      int estado;
      if (estadoStr.equals("HIGH") || estadoStr.equals("1")) {
        estado = HIGH;
      } else {
        estado = LOW;
      }

      // 6. Executa o comando, se o pino for válido
      // Verificamos se o pino está entre 2 e 9 (que são seus dispositivos)
      if (pino >= 2 && pino <= 9) { 
        digitalWrite(pino, estado);
        
        // Envia uma confirmação de volta para o C#
        Serial.print("OK:");
        Serial.println(comando);
      } else {
        // Pino inválido, notifica o C#
        
      }
    }
  }
}