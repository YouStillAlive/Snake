using System;
using System.Collections.Generic;
using System.Timers;
//using System.Linq;

namespace Snake
{
    class Snake
    {
        private char HeadSymbol;
        //private char SnakeBody;

        private const int LeftWall = 16, BottomWall = 25;

        private bool Lost;

        private Random rand;

        private List<int> x;
        private List<int> y;
        private int old_x, old_y;

        private Timer Time;

        private delegate void Movement(object sender, ElapsedEventArgs arg);
        private Movement Move;

        private int Eat_x, Eat_y;
        private int Score = 0;

        public Snake()
        {
            Console.Clear();
            Time = new Timer();
            x = new List<int>();
            y = new List<int>();
            HeadSymbol = (char)2;
            //SnakeBody = (char)15;
            Lost = false;

            ShowTable();
            DefaultValue();
            PrintScore();
            x.Add(old_x);
            y.Add(old_y);
            rand = new Random();
            RandomValue();
        }

        private void DefaultValue()
        {
            Score = 0;
            x.Add(6);
            y.Add(5);
            Print(x[0], y[0], HeadSymbol);
            Time.Interval = 400;
            Time.Elapsed += Lose;
            Time.Elapsed += CheckEat;
        }

        private void RandomValue()
        {
            Eat_x = rand.Next(BottomWall - 1);
            Eat_y = rand.Next(LeftWall - 1);

            CheckInvalid(x, Eat_x, BottomWall);
            CheckInvalid(y, Eat_y, LeftWall);

            Print(Eat_x, Eat_y, (char)1);
        }

        private void CheckInvalid(List<int> Value, int Coord, int Diapos)
        {
            for (int i = 0; i < Value.Count; i++)
                while (Coord == Value[i])
                    Coord = rand.Next(Diapos - 1);
        }

        private void ShowTable()
        {
            for (int i = 0; i < LeftWall; i++)
            {
                Console.SetCursorPosition(BottomWall, 0 + i);
                Console.Write("|");
            }
            for (int i = 0; i < BottomWall; i++)
            {
                Console.SetCursorPosition(0 + i, LeftWall);
                Console.Write((char)22);
            }
            Console.SetCursorPosition(BottomWall, LeftWall);
            Console.Write('*');
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        private void PrintScore()
        {
            Console.SetCursorPosition(5, LeftWall + 1);
            Console.WriteLine("Score - " + Score);
            ++Score;
        }

        private void Clear()
        {
            Console.SetCursorPosition(old_x = x[x.Count - 1], old_y = y[y.Count - 1]);
            Console.Write(" ");
        }

        private void Print(int x, int y, char symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        private void CheckBodyWall()
        {
            for (int i = 1; i < x.Count; i++)
                if (x[0] == x[i] && y[0] == y[i])
                    Lost = true;
        }

        private void CheckEat(object sender, ElapsedEventArgs arg)
        {
            if (Eat_x == x[0] && Eat_y == y[0])
            {
                RandomValue();
                PrintScore();

                x.Add(old_x);
                y.Add(old_y);
            }
            ChangeBodyPosition();
        }

        private void ChangeBodyPosition()
        {
            for (int i = x.Count - 1, j = x.Count - 2; i > 0; i--, j--)
            {
                x[i] = x[j];
                y[i] = y[j];
            }
        }

        private void Lose(object sender, ElapsedEventArgs arg)
        {
            CheckBodyWall();
            if (Lost)
            {
                Console.Write("YOU LOSE!! Try again!");

                Unsubscribe();
                Time.Elapsed -= Lose;
                Time.Elapsed -= CheckEat;
            }
        }

        private void MoveDown(object sender, ElapsedEventArgs arg)
        {
            if (x.Count > 1 && y[0] != y[1] - 1)
            {
                Clear();
                Print(x[0], y[0] = y[0] < LeftWall ? ++y[0] : Convert.ToInt32(Lost = true), HeadSymbol);
            }
        }

        private void MoveLeft(object sender, ElapsedEventArgs arg)
        {
            Clear();
            Print(x[0] = x[0] > 0 ? --x[0] : Convert.ToInt32(Lost = true), y[0], HeadSymbol);
        }

        private void MoveRight(object sender, ElapsedEventArgs arg)
        {
            Clear();
            Print(x[0] = x[0] < BottomWall - 1 ? ++x[0] : Convert.ToInt32(Lost = true), y[0], HeadSymbol);
        }

        private void MoveUp(object sender, ElapsedEventArgs arg)
        {
            Clear();
            Print(x[0], y[0] = y[0] > 0 ? --y[0] : Convert.ToInt32(Lost = true), HeadSymbol);
        }

        void Unsubscribe()
        {
            if (Move != null)
                Time.Elapsed -= new ElapsedEventHandler(Move);
        }

        private bool CanDown()
        {
            return x.Count > 2 && y[0] == y[1] - 1 ? false : true;
        }

        private bool CanUp()
        {
            return x.Count > 2 && y[0] == y[1] + 1 ? false : true;
        }

        private bool CanLeft()
        {
            return x.Count > 2 && x[0] == x[1] + 1 ? false : true;
        }

        private bool CanRight()
        {
            return x.Count > 2 && x[0] == x[1] - 1 ? false : true;
        }

        public void Start()
        {
            ConsoleKeyInfo Input;
            do
            {
                Input = Console.ReadKey(true);
                Time.Start();
                switch (Input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (CanUp())
                        {
                            Unsubscribe();
                            Time.Elapsed += new ElapsedEventHandler(Move = MoveUp);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (CanDown())
                        {
                            Unsubscribe();
                            Time.Elapsed += new ElapsedEventHandler(Move = MoveDown);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (CanLeft())
                        {
                            Unsubscribe();
                            Time.Elapsed += new ElapsedEventHandler(Move = MoveLeft);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (CanRight())
                        {
                            Unsubscribe();
                            Time.Elapsed += new ElapsedEventHandler(Move = MoveRight);
                        }
                        break;
                }
            } while (Input.Key != ConsoleKey.Escape && !Lost);
            Time.Dispose();
        }
    }
}