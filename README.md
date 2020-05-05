# ETS2_projects
Code and instructions for piping EuroTruck Simulator 2 (ETS2) telemetry to an Arduino board

Updated 05MAY2020
=================
In this folder you'll find C# code to pipe ETS2 telemetry (odometer value) from ETS2 out through the COM3 serial
port of your local machine; together with an Arduino sketch for reading incoming serial data and printing it to 
an LCD screen (in this case Velleman LCD1602 shield, but any screen should work as long as it using the Arduino's 
main LCD library - <LiquidCrystal.h>

INSTRUCTIONS
------------
Grab the arduino sketch -
ETS2_projects\arduino_serial_read_in_jt_v1\arduino_serial_read_in_jt_v1.ino
Verify the code and then upload to Arduino UNO board using Arduino IDE
You can test that the serial read works by opening the serial monitor (in IDE's tools menu); typing in a 6digit
mileage and hitting send

Next grab the DLL from - https://github.com/nlhans/ets2-sdk-plugin/releases
It's nestled in 'release_v1_4_0.zip' inside the Win32 folder.
Thanks - nlhans :)

Now, move the DLL to the folding location on your machine (note - you may have to create a 'plugins' folder)
C:/Program Files (x86)/Steam/steamapps/common/Euro Truck Simulator 2/bin/win_x86/plugins/ets2-telemetry.dll
Thanks - mattfreire :)

Ok - time to fire up ETS2 (launching in 32-bit safe mode should grab the DLL; if all goes well, you'll see
a box saying that SDK is being used - click ok)

Last step, download the C# folder (source files included, if you want to modify/build up etc...) -
ETS2_projects/Serial_write_from_ETS2_odom_whileloop_jt_v2

And run the .exe file tucked away in the bin\Debug folder -
C:\Users\james\source\repos\Serial_write_from_ETS2_odom_whileloop_jt_v2\Serial_write_from_ETS2_odom_whileloop_jt_v2\bin\Debug

Boa sorte!
