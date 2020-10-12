using System;
using System.Collections.Generic;

namespace Snake
{
    class GameMenu
    {
        private List<IMenu> Menu;
        private int CurrentItem;
        private int x, y;

        public GameMenu()
        {
            Menu = new List<IMenu>();
            DefaultValue();
        }

        private void DefaultValue()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            CurrentItem = 0;
            x = 5;
            y = 4;
        }

        private void ShowItem(ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(Menu[CurrentItem].Name());
        }

        private void Back()
        {
            ShowItem(ConsoleColor.Yellow);
            if (CurrentItem > 0)
                CurrentItem--;
            else
                CurrentItem = Menu.Count - 1;

            y = y > 6 ? y -= 2 : y = Menu.Count * 2 + 4;

            ShowItem(ConsoleColor.DarkRed);
        }

        private void Next()
        {
            ShowItem(ConsoleColor.Yellow);
            if (CurrentItem < Menu.Count - 1)
                CurrentItem++;
            else
                CurrentItem = 0;

            y = y < (Menu.Count - 1) * 2 + 6 ? y += 2 : y = 6;
            ShowItem(ConsoleColor.DarkRed);
        }

        private void Entry()
        {
            Menu[CurrentItem].Action();
        }

        public void Add(IMenu MenuItem)
        {
            Menu.Add(MenuItem);
        }

        public void Show()
        {
            Console.Clear();
            DefaultValue();

            foreach (var Item in Menu)
            {
                Console.SetCursorPosition(x, y += 2);
                Console.WriteLine(Item.Name());
            }

            y = 6;
            ShowItem(ConsoleColor.DarkRed);
        }

        public void Start()
        {
            ConsoleKeyInfo Input;
            do
            {
                Input = Console.ReadKey(true);
                switch (Input.Key)
                {
                    case ConsoleKey.UpArrow:
                        Back();
                        break;
                    case ConsoleKey.DownArrow:
                        Next();
                        break;
                    case ConsoleKey.Enter:
                        Entry();
                        Show();
                        break;
                }
            } while (Input.Key != ConsoleKey.Escape);
            Console.ResetColor();
        }
    }
}