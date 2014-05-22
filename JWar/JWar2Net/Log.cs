using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JWar2Net
{
    public class Log
    {
        public static void Print(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public static void PrintGreen(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public static void PrintRed(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, args);
            Console.ResetColor();
        }

        public static void Debug(string tag, string message)
        {
            Console.WriteLine("debug\t{0}\t{1}", tag, message);
        }

        public static void Debug(string tag, string format, params object[] args)
        {
            Console.WriteLine("debug\t{0}\t{1}", tag, string.Format(format, args));
        }

        public static void Error(string tag, string message)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("debug\t{0}\t{1}", tag, message);
            //Console.ResetColor();
        }

        public static void Error(string tag, string format, params object[] args)
        {
            //Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("error\t{0}\t{1}", tag, string.Format(format, args));
            //Console.ResetColor();
        }
    }
}
