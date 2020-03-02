using System;
using System.Linq;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace testing
{
    class Program
    {
        static Random rnd = new Random();
        public static string[,] fullMap = {
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" } ,
            { "empty", "empty", "empty", "empty", "empty" }
            };

        public static readonly string[] allowedCommands = { "up", "left", "down", "right", "hit" };
        
        public enum Direction
        {
            Up, Down, Left, Right
        }
        static void Main(string[] args)
        {



            Player MainPlayer = new Player(2, 2, Direction.Up);
            int i = 0;
            while(i < 5)
            {
                int randX = rnd.Next(0, 5);
                int randY = rnd.Next(0, 5);
                if (fullMap[randX, randY] == "empty")
                {
                    _ = new GenericObject(randX, randY);
                    i++;
                }
                
            }
            




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

                Console.WriteLine("[{0}]", string.Join(", ", allowedCommands));

                Console.Write("Enter a command: ");
                string command = Console.ReadLine();
                command = Regex.Replace(command, @"\s+", "").ToLower();
                if (allowedCommands.Contains(command))
                {
                    switch (command)
                    {
                        case "up":
                        case "down":
                        case "left":
                        case "right":
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
                            break;
                        case "hit":
                            switch (MainPlayer.Dir)
                            {
                                case Direction.Up:
                                    if (Program.fullMap[MainPlayer.PosX - 1, MainPlayer.PosY] != "empty")
                                    {
                                        Program.fullMap[MainPlayer.PosX - 1, MainPlayer.PosY] = "empty";
                                    } else
                                    {
                                        Console.WriteLine("Nothing to hit");
                                    }
                                    break;
                                case Direction.Down:
                                    if (Program.fullMap[MainPlayer.PosX + 1, MainPlayer.PosY] != "empty")
                                    {
                                        Program.fullMap[MainPlayer.PosX + 1, MainPlayer.PosY] = "empty";
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nothing to hit");
                                    }
                                    break;
                                case Direction.Left:
                                    if (Program.fullMap[MainPlayer.PosX, MainPlayer.PosY - 1] != "empty")
                                    {
                                        Program.fullMap[MainPlayer.PosX, MainPlayer.PosY - 1] = "empty";
                                    }
                                    else
                                    {
                                        Console.WriteLine("Nothing to hit");
                                    }
                                    break;
                                case Direction.Right:
                                    if (Program.fullMap[MainPlayer.PosX, MainPlayer.PosY + 1] != "empty")
                                    {
                                        Program.fullMap[MainPlayer.PosX, MainPlayer.PosY + 1] = "empty";
                                    }
                                    break;
                            }
                            break;

                    }

                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
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

            AddObject(x, y);
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
