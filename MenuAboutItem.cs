namespace Snake
{
    class MenuAboutItem : IMenu
    {
        About Author;

        public void Action()
        {
            Author = new About();
            Author.Start();
        }

        public string Name()
        {
            return "About";
        }
    }
}
