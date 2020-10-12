using System;
using System.Threading;

namespace Snake
{
    class About
    {
        private int x, y;

        public About()
        {
            Console.Clear();
            x = 5;
            y = 20;
        }

        private void PrintItem(ConsoleColor Color, string text)
        {
            Console.ForegroundColor = Color;
            Show(text);
            Clear();
        }

        private void Show(string text)
        {
            Console.SetCursorPosition(x, --y);
            Console.Write(text);
        }

        private void Clear()
        {
            Console.SetCursorPosition(x - 5, y + 2);
            Console.Write("     ~       ~       ~         ");
        }

        public void Start()
        {
            do
            {
                PrintItem(ConsoleColor.Yellow, "Andrew Dmitrenko");
                PrintItem(ConsoleColor.DarkRed, "\tProgrammer");
                Thread.Sleep(500);
            } while (y != 4);

            Thread.Sleep(750);
            Console.Clear();
            x = 5; y = 20;
        }
    }
}
