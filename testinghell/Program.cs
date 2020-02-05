using System;
using System.Text.RegularExpressions;

namespace testing
{
    class Program
    {

        public static string[,] fullMap = {
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" }
            };

        public enum Direction
        {
            Up, Down, Left, Right
        }
        static void Main(string[] args)
        {



            Player MainPlayer = new Player(2, 2,Direction.Up);
            _ = new GenericObject(0, 2);
            _ = new GenericObject(4, 1);
            _ = new GenericObject(4, 3);



            while (true)
            {

                int drawX = 0;
                int drawY = 0;

                while (drawY != 5)
                {
                    while (drawX != 5)
                    {
                        switch (fullMap[drawY, drawX])
                        {
                            case "empty":
                                Console.Write("#");
                                //Console.Write(drawX);
                                //Console.Write(drawY);
                                break;

                            case "player":
                                switch (MainPlayer.Dir)
                                {
                                    case Direction.Up:
                                        Console.Write("A");
                                        break;
                                    case Direction.Down:
                                        Console.Write("V");
                                        break;
                                    case Direction.Left:
                                        Console.Write("<");
                                        break;
                                    case Direction.Right:
                                        Console.Write(">");
                                        break;
                                }
                                
                                break;

                            case "generic":
                                Console.Write("O");
                                break;
                        }

                        drawX++;

                    }
                    Console.WriteLine();
                    drawX = 0;
                    drawY++;
                }

                string command = Console.ReadLine();
                int dist = 0;
                try
                {
                    string rawDist = Regex.Match(Console.ReadLine(), @"\d+").Value;
                    dist = Int32.Parse(rawDist);
                }
                catch
                {
                    Console.WriteLine("Bad Input");
                }
                


                MainPlayer.Move(command, dist);

            }
        }
    }
    class Player : GenericObject
    {
        

        public Program.Direction Dir
        { get; set; }

        public Player(int x, int y, Program.Direction d) : base(x, y)
        {
            Dir = d;
            PosX = x;
            PosY = y;

            AddObject(x,y);
        }


        void AddObject(int tX, int tY)
        {
            this.PosX = tX;
            this.PosY = tY;
            Program.fullMap[PosX, PosY] = "player";
        }

        public void SetPosition(int x, int y)
        {
            if (Program.fullMap[x, y] == "empty")
            {
                int oldX = PosX;
                int oldY = PosY;
                AddObject(x, y);
                Console.WriteLine("Object Created At " + y + ", " + x);
                Program.fullMap[oldX, oldY] = "empty";
            }
            else
            {
                Console.WriteLine("Failed to create object");
            }
        }

        public void Move(string d, int a)
        {
            switch (d)
            {
                case "left":
                    if (this.PosY - a <= 5)
                    {
                        SetPosition(this.PosX, this.PosY - a);
                        Dir = Program.Direction.Left;
                    }
                    else
                    {
                        Console.WriteLine("Out Of Bounds");
                    }
                    break;

                case "down":
                    if (this.PosX + a <= 5)
                    {
                        SetPosition(this.PosX + a, this.PosY);
                        Dir = Program.Direction.Down;
                    }
                    else
                    {
                        Console.WriteLine("Out Of Bounds");
                    }
                    break;

                case "up":
                    if (this.PosX - a <= 5)
                    {
                        SetPosition(this.PosX - a, this.PosY);
                        Dir = Program.Direction.Up;
                    }
                    else
                    {
                        Console.WriteLine("Out Of Bounds");
                    }
                    break;

                case "right":
                    if (this.PosY + a <= 5)
                    {
                        SetPosition(this.PosX, this.PosY + a);
                        Dir = Program.Direction.Right;
                    }
                    else
                    {
                        Console.WriteLine("Out Of Bounds");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid Direction");
                    break;
            }

        }

    }

    class GenericObject
    {
        public int PosX
        { get; set; }
        public int PosY
        { get; set; }

        public GenericObject(int x, int y)
        {
            PosX = x;
            PosY = y;
            AddObject(PosX, PosY);

        }

        void AddObject(int tX, int tY)
        {
            this.PosX = tX;
            this.PosY = tY;
            Program.fullMap[tX, tY] = "generic";
        }

        

    }
}



