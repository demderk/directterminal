using System;
using System.IO.Ports;
using System.IO;
using System.Xml;
using System.Linq;

namespace DirectTerminal
{
    class Program
    {
        static Draw Drw = new Draw();
        static Logic Logic = new Logic();
        static void Main(string[] args)
        {
            PortInfo portInfo = new PortInfo();

            Console.Title = "Direct Terminal 1.1";
            //Console.WriteLine("Type COMPortName");
            Console.WriteLine("Direct Terminal 1.1 | demderk 2018");
            Console.WriteLine();
            Console.CancelKeyPress += CancelKey;

            string[] userConfigs = Directory.GetFiles(Directory.GetCurrentDirectory(), "config_*.xml");
            // Find user presets in current directory.
            bool defaultCfgFound = File.Exists("config.xml");
            string[] allFilesPath = new string[] { };
            // All files array (includes default).
            ConfigParser cParser = new ConfigParser();
            if (defaultCfgFound && userConfigs.Length >= 0)
            {
                string errObject = null;
                XmlDocument xmlDoc = new XmlDocument();
                try
                {

                    string filePath = "";
                    if (userConfigs.Length > 0)
                    // If user presets find.
                    {
                        if (defaultCfgFound ? cParser.UserPresetsEnabled("config.xml") : true)
                        {
                            if (defaultCfgFound)
                            {
                                allFilesPath = new string[] { $"{Directory.GetCurrentDirectory()}\\config.xml" };
                                allFilesPath = allFilesPath.Concat(userConfigs).ToArray();
                            }
                            Console.WriteLine("Config manager: found several presets.\n\nSelect preset:\n");
                            for (int i = 0; i < allFilesPath.Length; i++)
                            {
                                string parsedName = allFilesPath[i]
                                    .Replace(Directory.GetCurrentDirectory(), "")
                                    .Replace(".xml", "");
                                if (parsedName.IndexOf("config_") != -1)
                                {
                                    parsedName = parsedName.Substring(8);
                                }
                                else if (parsedName == "\\config")
                                {
                                    parsedName = "Default config";
                                }

                                Console.WriteLine($"[{i}] {parsedName}");
                            }
                            while (true)
                            // Error format exeption handler.
                            {
                                Console.Write("\nSelected > ");
                                string rL = Console.ReadLine();
                                try
                                {
                                    int sI = Convert.ToInt32(rL);
                                    filePath = allFilesPath[sI];
                                    break;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("Wrong number");
                                }
                                catch (FormatException)
                                {
                                    if (rL.Replace(" ", "") != "")
                                    {
                                        Console.WriteLine("Incorrect format");
                                    }
                                }
                            }
                        }
                        else
                        {
                            filePath = "config.xml";
                        }
                    }
                    else
                    {
                        filePath = "config.xml";
                    }
                    xmlDoc.Load(filePath); 
                    // Load file by link
                    portInfo = cParser.BuildPort(xmlDoc);
                    // Sending file to parser
                }
                catch (XmlException ex)
                {
                    Drw.CriticalError(ex, errObject);
                    return;
                }
                catch (Exception ex)
                {
                    Drw.CriticalError(ex);
                    return;
                }
            }
            else
            // If default config not exists
            {
                string COMPort = null;
                int baudRate = -1;
                Console.WriteLine("Tip: Use default preset for fast connect. https://git.io/fxREa");
                Console.Write("COM port name > ");
                COMPort = Console.ReadLine();
                Console.Write("baud rate > ");
                baudRate = Convert.ToInt32(Console.ReadLine());
                portInfo.lEnd = LineEnding.NL;
                portInfo.sPort = new SerialPort(COMPort, baudRate);
            }

            try
            {
                Drw.DrawHeader();
                // Drawing header
                portInfo.sPort.Open();
                // Opening port
                Logic.PortLineEnding = portInfo.lEnd;
                // End of line mode setting
                Logic.Start(portInfo.sPort);
            }
            catch (Exception ex)
            {
                Drw.CriticalError(ex);
            }
        }

        private static void CancelKey(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Break");
            Logic.IsCanceled = true;
            Console.ReadLine(); // For vs debugger
        }
    }
}
