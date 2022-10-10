// Другие версии Ардуино

#include <SoftwareSerial.h>
SoftwareSerial mySerial(10,11); // RX, TX

String message;

void setup()
{
  Serial.begin(9600);
  mySerial.begin(9600);
  Serial.setTimeout(50);
  mySerial.setTimeout(50);
  
  pinMode(8, OUTPUT);
  pinMode(9, OUTPUT);
  digitalWrite(8, LOW);
  digitalWrite(9, LOW);
}
void loop()
{
 if(Serial.available())
 {
  message = Serial.readString();
  mySerial.print(message);
 }

 if(mySerial.available())
 {
  message = mySerial.readString();
  Serial.print(message);
 }
}
