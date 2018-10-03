using System;
using System.Collections.Generic;
using System.Text;

namespace DirectTerminal
{
    class Draw
    {
        public void DrawHeader()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" SHIFT+X ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Execute command ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" SHIFT+C ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Clear console ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" CTRL+C ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Exit program ");
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" SHIFT+P ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Pause recieve ");
            Console.ResetColor();
            Console.BufferHeight = Console.BufferHeight + 1;
            Console.WriteLine();
            Console.WriteLine();
        }
        public void CriticalError(Exception ex)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR!");
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Unknown error.");
            Console.WriteLine($"{ex.Message}");
            Console.ResetColor();
            Console.BufferHeight = Console.BufferHeight + 1;
            Console.WriteLine("Press any button for exit");
            Console.ReadLine();
            return;
        }
        public void CriticalError(Exception ex,string errObject)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.WriteLine("ERROR!");
            Console.WriteLine("An error has occurred while reading the file (config.xml - default settings file).");
            Console.WriteLine($"{ex.Message} {(errObject == null ? "" : $"Property name is {errObject}")}");
            Console.WriteLine();
            Console.WriteLine("Press any button for exit");
            Console.ReadLine();
            return;
        }
    }
}
