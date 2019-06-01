using System;

namespace DirectTerminal
{
    internal static class Draw
    {
        static public void DrawHeader()
        {
            Console.WriteLine();
            Console.WriteLine("#---------------Controls--------------#");
            Console.WriteLine("| [SPACE] or [ENTER] - execute command|");
            Console.WriteLine("| [SHIFT+C] - clear console           |");
            Console.WriteLine("| [SHIFT+P] - pause recieve           |");
            Console.WriteLine("| [CTRL+P] - temporary disconnect     |");
            Console.WriteLine("| [CTRL+C] - exit program             |");
            Console.WriteLine("#-------------------------------------#");
            Console.WriteLine();

        }
        static public void CriticalError(Exception ex)
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
            Console.WriteLine("Press any button for exit.");
            Console.ReadKey();
            return;
        }
        static public void CriticalError(Exception ex, string errObject)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.WriteLine("ERROR!");
            Console.WriteLine("An error has occurred while reading the file (config.xml - default settings file).");
            Console.WriteLine($"{ex.Message} {(errObject == null ? "" : $"Property name is {errObject}")}");
            Console.WriteLine();
            Console.WriteLine("Press any button for exit");
            Console.ReadKey();
            return;
        }
        static public void Error(Exception ex, string beforeString = "Press any button for continue.")
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("ERROR!");
            Console.ResetColor();
            Console.WriteLine($"{ex.Message}");
            Console.ResetColor();
            if (!string.IsNullOrEmpty(beforeString))
            {
                Console.WriteLine(beforeString);
                Console.ReadKey();
            }
        }
    }
}
