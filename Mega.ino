// Ардуино МЕГА

String message;

void setup()
{
  Serial.begin(9600);
  Serial3.begin(9600);
  Serial.setTimeout(50);
  Serial3.setTimeout(50);
  
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
  Serial3.print(message);
 }

 if(Serial3.available())
 {
  message = Serial3.readString();
  Serial.print(message);
 }
}
