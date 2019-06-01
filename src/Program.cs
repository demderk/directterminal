using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Xml;

namespace DirectTerminal
{
    internal static class ProgramIformation
    {
        internal const string _VERSION = "1.2";
    }

    internal class Program
    {
        private static readonly Logic Logic = new Logic();

        private static void Main()
        {
            Console.Title = $"Direct Terminal {ProgramIformation._VERSION}";
            Console.WriteLine($"Direct Terminal {ProgramIformation._VERSION} | demderk 2018-2019.");
            Console.WriteLine();
            Body();

        }

        private static void Body()
        {
            PortInfo portInfo = new PortInfo();
            string[] userConfigs = Directory.GetFiles(Directory.GetCurrentDirectory(), "config_*.xml");
            // Find user presets in current directory.
            bool defaultCfgFound = File.Exists("config.xml");
            List<string> allFilesPath = new List<string>();
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
                                allFilesPath.Add($"{Directory.GetCurrentDirectory()}\\config.xml");
                            }
                            allFilesPath.AddRange(userConfigs);
                            Console.WriteLine("Config manager: found several presets.\n\nSelect preset:\n");
                            for (int i = 0; i < allFilesPath.Count; i++)
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
                            // Wrong value exeption handler.
                            {
                                Console.Write("\nSelected > ");
                                string rL = Console.ReadLine();
                                try
                                {
                                    int sI = Convert.ToInt32(rL);
                                    if (sI > allFilesPath.Count)
                                    {
                                        throw new IndexOutOfRangeException();
                                    }
                                    filePath = allFilesPath[sI];
                                    break;
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("Wrong number.");
                                }
                                catch (FormatException)
                                {
                                    if (rL.Replace(" ", "") != "")
                                    {
                                        Console.WriteLine("Incorrect format.");
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
                    Draw.CriticalError(ex, errObject);
                    return;
                }
                catch (Exception ex)
                {
                    Draw.CriticalError(ex);
                    return;
                }
            }
            else
            // If default config not exists
            {
                Console.WriteLine("Tip: Use default preset for fast connect. https://github.com/demderk/directterminal#readme");
                Console.Write("COM port name > ");
                string COMPort = Console.ReadLine();
                Console.Write("Baud rate > ");
                int baudRate = Convert.ToInt32(Console.ReadLine());
                portInfo.lEnd = LineEnding.NL;
                portInfo.sPort = new SerialPort(COMPort, baudRate);
            }

            try
            {
                portInfo.sPort.Open();
                Draw.DrawHeader();
                Logic.PortLineEnding = portInfo.lEnd;
                Logic.Start(portInfo.sPort);
            }
            catch (IOException ex)
            {
                Draw.Error(ex);
                Body();
            }
            catch (Exception ex)
            {
                Draw.CriticalError(ex);
            }
        }
    }
}
