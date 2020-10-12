using System;
namespace Snake
{
    class ExitMenuItem : IMenu
    {
        public void Action()
        {
            Console.ResetColor();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public string Name()
        {
            return "Exit";
        }
    }
}
