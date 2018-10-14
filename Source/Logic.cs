using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;



namespace DirectTerminal
{
    class Logic
    {
        private string portLineEndingString = "";

        private LineEnding portLineEnding;

        public LineEnding PortLineEnding
        {
            get
            {
                return portLineEnding;
            }
            set
            {
                portLineEnding = value;
                switch (value)
                {
                    case LineEnding.Nope:
                        portLineEndingString = "";
                        break;
                    case LineEnding.NL:
                        portLineEndingString = "\n";
                        break;
                    case LineEnding.CR:
                        portLineEndingString = "\r";
                        break;
                }
            }
        }

        public bool IsCanceled { get; set; } = false;

        public void Start(SerialPort port)
        // Main logic body
        {

            bool pause = false;

            while (!IsCanceled)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo ski = Console.ReadKey(true);

                    if (ski.Modifiers == ConsoleModifiers.Shift && ski.Key == ConsoleKey.P)
                    {
                        pause = !pause;
                        // Turn on pause mode
                        if (pause)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write("\n  PAUSED  ");
                            Console.ResetColor();
                        }
                        else
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                //remove pause word
                                Console.Write("\b");
                                Console.Write(" ");
                                Console.Write("\b");

                            }
                        }
                    }
                    if (!pause)
                    {
                        if (ski.Modifiers == ConsoleModifiers.Control && ski.Key == ConsoleKey.C)
                        {
                            Environment.Exit(0);
                        }
                        else if (ski.Key == ConsoleKey.Spacebar || ski.Key == ConsoleKey.Enter)
                        {
                            // Send mode logic
                            string commandBuffer = "";

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\nSend> ");
                            Console.ForegroundColor = ConsoleColor.White;
                            commandBuffer = Console.ReadLine();

                            for (int i = 0; i < commandBuffer.Length + 6; i++)
                            {
                                Console.Write("\b");
                                Console.Write(" ");
                                Console.Write("\b");
                            }

                            for (int i = 0; i < port.BytesToRead; i++)
                            {
                                Console.Write((char)port.ReadByte());
                            }

                            if (commandBuffer != "")
                            {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Green;
                                port.Write($"{commandBuffer}{portLineEndingString}");
                                Console.Write("\nSENDED: ");
                                Console.WriteLine(commandBuffer);
                                Console.ResetColor();
                            }
                        }
                        else if (ski.Modifiers == ConsoleModifiers.Shift && ski.Key == ConsoleKey.C)
                        {
                            Console.Clear();
                            Draw drw = new Draw();
                            drw.DrawHeader();
                        }
                        else if (ski.Modifiers == ConsoleModifiers.Control && ski.Key == ConsoleKey.P)
                        {
                            port.Close();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine("\nTemp stopped... Press CTRL+P again for retry");
                            Console.ResetColor();
                            while (true)
                            {
                                ski = Console.ReadKey(true);
                                if (ski.Modifiers == ConsoleModifiers.Control && ski.Key == ConsoleKey.P)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine("Connecting...");
                                    Console.ResetColor();
                                    try
                                    {
                                        port.Open();
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.WriteLine($"{ex.Message} \nPress CTRL+P again for retry");
                                        Console.ResetColor();
                                    }
                                    if (port.IsOpen)
                                    {
                                        Console.ForegroundColor = ConsoleColor.DarkGray;
                                        Console.WriteLine("Success");
                                        Console.ResetColor();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                if (!pause && port.BytesToRead != 0)
                {
                    Console.Write((char)port.ReadByte());
                }
            }
        }
    }
}
