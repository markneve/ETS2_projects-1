//Sample using LiquidCrystal library
#include <LiquidCrystal.h>

/*******************************************************

This program will display odometer reading sent from PC --> Arduino along serial port COM3
v1.0

********************************************************/

// select the pins used on the LCD panel
LiquidCrystal lcd(8, 9, 4, 5, 6, 7);
long odom = 123456;

void setup()
{
 Serial.begin(9600); // open serial port
  
 lcd.begin(16, 2);              // start the library
 lcd.setCursor(0,0);
 lcd.print("SlowLaneTrucking"); // print a simple message
 lcd.setCursor(0,1);            // move to the begining of the second(1) line
 lcd.print("Mileage:");
}
 
void loop()
{
 lcd.setCursor(10,0);            // move cursor to second line "1" and 9 spaces over
// lcd.print(millis()/1000);      // display seconds elapsed since power-up
 lcd.setCursor(9,1);             // move cursor 9 spaces over on second(1) line
 
 // get odometer reading from serial

/*******************************************************

Many thanks to (EconJack) for Arduino serial receive code: https://forum.arduino.cc/index.php?topic=498441.0

********************************************************/

int charsReceived;
  char inputBuffer[7];            // Size of buffer to read from serial, make whatever size you need plus one

  if (Serial.available()) 
  {
    charsReceived = Serial.readBytesUntil('\n', inputBuffer, sizeof(inputBuffer) - 1);  // Save room for NULL
    inputBuffer[charsReceived] = NULL;      // Make it a string
   
// useful to leave s/prints in for testing serial connection from IDE serial monitor to Ardunio UNO
// but prob want to comment out (turn off) when using ETS2
// sketch still works with them in though :)

    Serial.print("there were ");
    Serial.print(charsReceived);
    Serial.print(" chars received, which are: ");
    Serial.println(inputBuffer);   
  }
 
 lcd.print(inputBuffer);
}
