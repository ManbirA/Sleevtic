#include <WiFi.h>
#include <Wire.h>
#include "Adafruit_DRV2605.h"

#define TCAADDR 0x70 //keep the same

Adafruit_DRV2605 drv1;
Adafruit_DRV2605 drv2;
Adafruit_DRV2605 drv3;
Adafruit_DRV2605 drv4;

// Replace with your network credentials
const char* ssid = "";
const char* password = "";

// Set web server port number to 80
WiFiServer server(80);

// Variable to store the HTTP request
String header;

//Set static IP address
IPAddress local_IP(192,168,116,221);
IPAddress gateway(192,168,1,1);

IPAddress subnet(255,255,0,0);

// Current time
unsigned long currentTime = millis();
// Previous time
unsigned long previousTime = 0; 
// Define timeout time in milliseconds (example: 2000ms = 2s)
const long timeoutTime = 2000;

// function to select module on TCA multiplexer
void tcaselect(uint8_t i) {
  if (i > 7) return;
  Wire.beginTransmission(TCAADDR);
  Wire.write(1 << i);
  Wire.endTransmission();  
}


void setup() {
  Serial.begin(115200);
  //
  // Setup motor drivers
  //
  Wire.begin();
  tcaselect(7);
  drv1.begin();
  drv1.selectLibrary(1);
  drv1.setMode(DRV2605_MODE_INTTRIG);
  tcaselect(6);
  drv2.begin();
  drv2.selectLibrary(1);
  drv2.setMode(DRV2605_MODE_INTTRIG);
  tcaselect(5);
  drv3.begin();
  drv3.selectLibrary(1);
  drv3.setMode(DRV2605_MODE_INTTRIG);
  tcaselect(4);
  drv4.begin();
  drv4.selectLibrary(1);
  drv4.setMode(DRV2605_MODE_INTTRIG);

  //
  // Setup Wi-Fi server
  //

  // Configure static IP Address
  if(!WiFi.config(local_IP, gateway, subnet)) {
    Serial.println("Failed to configure IP");
  }
  
  // Connect to Wi-Fi network with SSID and password
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  // Print local IP address and start web server
  Serial.println("");
  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  server.begin();
}

void playEffect(int effect){
  tcaselect(7);
  drv1.setWaveform(0, effect);
  drv1.setWaveform(1, 0);
  drv1.go();
  tcaselect(6);
  drv2.setWaveform(0, effect);
  drv2.setWaveform(1, 0);
  drv2.go();
  tcaselect(5);
  drv3.setWaveform(0, effect);
  drv3.setWaveform(1, 0);
  drv3.go();
  tcaselect(4);
  drv4.setWaveform(0, effect);
  drv4.setWaveform(1, 0);
  drv4.go();
}

void loop(){
//  Serial.println(WiFi.localIP());
  WiFiClient client = server.available();   // Listen for incoming clients

  if (client) {                             // If a new client connects,
    currentTime = millis();
    previousTime = currentTime;
    Serial.println("New Client.");          // print a message out in the serial port
    String currentLine = "";                // make a String to hold incoming data from the client
    while (client.connected() && currentTime - previousTime <= timeoutTime) {  // loop while the client's connected
      currentTime = millis();
      if (client.available()) {             // if there's bytes to read from the client,
        char c = client.read();             // read a byte, then
        Serial.write(c);                    // print it out the serial monitor
        header += c;
        if (c == '\n') {                    // if the byte is a newline character
          // if the current line is blank, you got two newline characters in a row.
          // that's the end of the client HTTP request, so send a response:
          if (currentLine.length() == 0) {
            // HTTP headers always start with a response code (e.g. HTTP/1.1 200 OK)
            // and a content-type so the client knows what's coming, then a blank line:
            // turns the GPIOs on and off
            if (header.indexOf("GET /1/on") >= 0) {
              Serial.println("Haptic effect 1 on");
              playEffect(55);
            } else if (header.indexOf("GET /2/on") >= 0) {
              Serial.println("Haptic effect 2 on");
              playEffect(8);
            } else if (header.indexOf("GET /3/on") >= 0) {
              Serial.println("Haptic effect 3 on");
              playEffect(11);
            } else if (header.indexOf("GET /4/on") >= 0) {
              Serial.println("Haptic effect 4 on");
              playEffect(49);
            }
            // Break out of the while loop
            break;
          } else { // if you got a newline, then clear currentLine
            currentLine = "";
          }
        } else if (c != '\r') {  // if you got anything else but a carriage return character,
          currentLine += c;      // add it to the end of the currentLine
        }
      }
    }
    // Clear the header variable
    header = "";
    // Close the connection
    client.stop();
    Serial.println("Client disconnected.");
    Serial.println("");
  }
}