using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Xml;

namespace DirectTerminal
{
    public enum LineEnding { Nope, NL, CR };

    public struct PortInfo
    {
        public SerialPort sPort;
        public LineEnding lEnd;
    }

    class ConfigParser
    {

        private string COMPortName { get; set; } = null;

        private int PortBaudRate { get; set; } = -1;

        private Parity PortParity { get; set; } = Parity.None;

        private int PortDataBits { get; set; } = 8;

        private StopBits PortStopBits { get; set; } = StopBits.One;

        private LineEnding PortLineEnding { get; set; } = LineEnding.NL;

        /// <summary>
        /// Read xml file.
        /// </summary>
        /// <param name="file">XmlDocument class with loaded file.</param>
        private void ReadXML(XmlDocument file)
        {
            //string errObject = "";
            XmlElement config = file.DocumentElement;
            foreach (XmlNode node in config.ChildNodes)
            {
                switch (node.Name)
                {
                    case "COMPort":
                        //errObject = "COMPort";
                        if (node.Attributes["question"] != null && node.Attributes["question"].Value == "true")
                        {
                            Console.WriteLine("Config manager: Type COM port name");
                            COMPortName = Console.ReadLine();
                        }
                        else
                        {
                            COMPortName = node.InnerText;
                        }
                        break;
                    case "BaudRate":
                        //errObject = "BaudRate";
                        PortBaudRate = Convert.ToInt32(node.InnerText);
                        break;
                    case "Parity":
                        //errObject = "Parity";
                        PortParity = (Parity)Enum.Parse(typeof(Parity), node.InnerText);
                        break;
                    case "DataBits":
                        //errObject = "DataBits";
                        PortDataBits = Convert.ToInt32(node.InnerText);
                        break;
                    case "StopBits":
                        //errObject = "StopBits";
                        PortStopBits = (StopBits)Enum.Parse(typeof(StopBits), node.InnerText);
                        break;
                    case "LineEnding":
                        //errObject = "LineEnding";
                        PortLineEnding = (LineEnding)Enum.Parse(typeof(LineEnding), node.InnerText);
                        break;
                }
            }
        }

        /// <summary>
        /// User presets setting.
        /// </summary>
        /// <param name="path">Path of xml file.</param>
        /// <returns></returns>
        public bool UserPresetsEnabled(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            XmlElement config = xmlDocument.DocumentElement;
            foreach (XmlNode item in config)
            {
                if (item.Name == "DisableUserPreset")
                {
                    if (item.InnerText == "true")
                    {
                        return false;
                    }
                    else if (item.InnerText == "false")
                    {
                        return true;
                    }
                    throw new FormatException();
                }
            }
            return true;
        }

        /// <summary>
        /// PortInfo class builder.
        /// </summary>
        /// <param name="file">Path of xml file</param>
        /// <returns></returns>
        public PortInfo BuildPort(XmlDocument file)
        {
            ReadXML(file);
            return new PortInfo()
            {
                sPort = new SerialPort(COMPortName, PortBaudRate, PortParity, PortDataBits, PortStopBits),
                lEnd = PortLineEnding
            };
        }
    }
}
