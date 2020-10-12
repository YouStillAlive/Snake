namespace Snake
{
    class Program
    {
        static void Main()
        {
            Utility.ConsoleStartValue();

            GameMenu Menu = new GameMenu();
            Menu.Add(new MenuNewGameItem());
            Menu.Add(new MenuAboutItem());
            Menu.Add(new ExitMenuItem());
            Menu.Show();
            Menu.Start();
        }
    }
}
