// thanks https://stackoverflow.com/questions/5891538/listen-for-key-press-in-net-console-app
// thanks https://gunnarpeipman.com/csharp-memory-mapped-files/
// thanks https://github.com/mattfreire/ETS_Telemetry_Plugin for file path info
// thanks https://github.com/nlhans/ets2-sdk-plugin for DLL with MMF output

using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.IO.MemoryMappedFiles;

namespace serial_send_net_framework_jtv1
{
    class Program
    {
        static SerialPort mySerialPort;
        static void Main(string[] args)
        {
            // configure serialport
            mySerialPort = new SerialPort("COM3", 9600);

            Console.WriteLine("Memory mapped file reader started");
            Console.WriteLine("Looking for MMF 'SimTelemetryETS2'");

            bool first_print = false;

            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
            {
                try
                {
                    // try to open MMF and return error messages if this fails
                    using (var file = MemoryMappedFile.OpenExisting("SimTelemetryETS2"))

                    {
                        if (first_print == false)
                        {
                            Console.WriteLine("Found it - nice one : )");
                            Console.WriteLine("Hit Esc to exit, or if that fails use Ctrl c to quit");
                        }

                        // put only the bits we need into mem for console program to use
                        // focus initially on odometer section - 4 bytes start, finish


                        using (var reader = file.CreateViewAccessor(668, 671))

                        {
                            // read memory stream into byte array
                            var bytes = new byte[4];
                            reader.ReadArray<byte>(0, bytes, 0, bytes.Length);

                            float odomF = BitConverter.ToSingle(bytes, 0);

                            if (first_print == false)
                            {
                                // pring value km --> miles conversion
                                Console.Write("Starting value in miles = ");
                                Console.Write(odomF * 0.621371);
                                Console.WriteLine(string.Empty);
                            }

                            // just print values once and then let it cycle
                            first_print = true;

                            // convert to integer value in miles ready for serial send to Ardunio 
                            int odomI = Convert.ToInt32(odomF * 0.621371);
                            // convert to string to get this to work as temp fix
                            string myString = odomI.ToString();

                            // start and finish serial send    
                            try
                            {
                                mySerialPort.Open(); //open the port
                                mySerialPort.WriteLine(myString); // write a message, ending with /n
                                mySerialPort.Close();
                            }
                            catch (IOException ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
                catch (System.IO.FileNotFoundException)
                {
                    Console.WriteLine("Can't find MMF...make sure that you have DLL installed in the following directory -");
                    Console.WriteLine("C:/Program Files (x86)/Steam/steamapps/common/Euro Truck Simulator 2/bin/win_x86/plugins/ets2-telemetry.dll");
                    Console.WriteLine("And have started the game.");
                    Console.WriteLine(string.Empty);
                }
                // pause so that LCD debug screen isn't overloaded
                Thread.Sleep(200);
            }
        }
    }
}
