using System;
//using System.Text;
//using System.Linq;

namespace Snake
{
    class Utility
    {
        //public static string ASCII = Encoding.GetEncoding(1250).GetString(Enumerable.Range(0, 256).Select(n => (byte)n).ToArray());

        public static void ConsoleStartValue(/*string WindowTitle*/)
        {
            Console.Title = "~~Snake~~";
            Console.WindowHeight = 20;
            Console.WindowWidth = 30;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorVisible = false;
        }
    }
}
