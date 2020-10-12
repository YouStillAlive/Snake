namespace Snake
{
    class MenuNewGameItem : IMenu
    {
        Snake Game;

        public void Action()
        {
            Game = new Snake();
            Game.Start();
        }

        public string Name()
        {
            return "New Game";
        }
    }
}
