using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System.Threading;
using System.Timers;
using System.Diagnostics;


namespace GalaxianGame
{
    public class GameEngine
    {
        public static int line = 21;
        public static int column = 39;
        public static dynamic[,] mass = new dynamic[line, column];
        public static Random rnd = new Random();
        public static string[] temp = new string[column];
        public static string[] ship = new string[column];
        public static int counter;
        public static int k;

        public static dynamic? temp_x;
        public static dynamic? temp_y;
        public static ConsoleKeyInfo moveYourship;
        public enum ThreadPriority { Highest, Lowest }
        public static Thread t = new Thread(MoveYourShip);

        public static dynamic[] list = new dynamic[39] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', };

        public static int? LaunchGame()
        {
            Console.WriteLine("\t\t<GALAXY GAME MENU>");
            Console.WriteLine();
            Console.WriteLine("Spaceship control:");
            Console.WriteLine();
            Console.WriteLine("\t\tStart    - 7");
            Console.WriteLine();
            Console.WriteLine("\t\tExit     - Enter");
            Console.WriteLine();
            Console.WriteLine("\t\tGo Left  - LeftArrow");
            Console.WriteLine();
            Console.WriteLine("\t\tGo Right - RightArrow");
            Console.WriteLine();
            Console.WriteLine("\t\tFire     - 3");
            Console.WriteLine();

            int button = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine(); 
            
            switch (button)
            {
                case 7:
                    CopyListToMass();
                    Console.Clear();
                    break;
            }
            return 0;
        }

        public static int LinearSearch(dynamic[,] mass) 
        {
            for (int i = 0; i < line; i++) 
            {
                for (int j = 0; j < column; j++)
                {
                    if (mass[18, j] == "Y")
                        k = j;
                }
            }
            return k;
        }

        public static void MoveYourShip()
        {
            while (true)
            {
                ShowMyShipLine();
                moveYourship = Console.ReadKey(true);
                switch (moveYourship.Key)
                {
                    case ConsoleKey.LeftArrow:
                        temp_x = mass[19, 0];
                        for (int j = 0; j < column - 1; j++)
                        {
                            mass[19, j] = mass[19, j + 1];
                            list[j] = list[j + 1];
                        }
                        mass[19, column - 1] = temp_x;
                        list[38] = temp_x;
                        Console.Clear();
                        break;

                    case ConsoleKey.RightArrow:
                        temp_y = mass[19, column - 1];
                        for (int j = column - 2; j >= 0; j--)
                        {
                            mass[19, j + 1] = mass[19, j];
                            list[j + 1] = list[j];
                        }
                        mass[19, 0] = temp_y;
                        list[0] = temp_y;
                        Console.Clear();
                        break;

                    case ConsoleKey.Enter:
                        t.Join();
                        Console.WriteLine($"Exit Game!!!");
                        break;
                }
            }
        }

        public static void Showfield()
        {
            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write($"{mass[i, j]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public static void Loadfield() 
        {
            for (int i = 0; i < line - 1; i++)
            {
                for (int j = 0; j < column - 1; j++)
                {
                    mass[line - 1, j] = "_";
                }
                
                for (int j = 0; j < column - 1; j = j + rnd.Next(0, 500))
                {
                    ship[j] = "Y";
                    mass[0, j] = ship[j];
                }
            }
        }

        public static dynamic? ShiftFirstline(dynamic[,] mass, dynamic[] list)
        {
            Loadfield();
            for (int i = 0; i < line - 2; i++)
            {
                Console.Clear();
                counter++;
                Showfield();
                Thread.Sleep(2000);
                for (int j = 0; j < column; j++)
                {
                    temp[j] = " ";
                    mass[i + 1, j] = mass[i, j];
                    mass[(i + 1) - 1, j] = temp[j];

                    mass[i + 6, j] = mass[i + 5, j];
                    mass[i + 5, j] = temp[j];

                    mass[i + 11, j] = mass[i + 10, j];
                    mass[i + 10, j] = temp[j];

                    mass[i + 16, j] = mass[i + 15, j];
                    mass[i + 15, j] = temp[j];
                };

                CopyListToMass();
                LinearSearch(mass);

                if (mass[18, k] == "Y") { Console.WriteLine($"j = {k}     GAME OVER DUDE"); return null; }

                else { if (counter % 5 == 0) break; }
            }
            return ShiftFirstline(mass, list);
        }

        public static void CopyListToMass() 
        {
            for (int j = 0; j < column - 1; j++)
            {
                mass[19, j] = list[j];
            }
        }

        public static void ShowMyShipLine()
        {
            for (int i = 0; i < line; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write($"{mass[i, j]}");
                }
                Console.WriteLine();
            }
        }

        public static void Main(string[] args)
        {
            LaunchGame();
  
            t.Start();
            ShiftFirstline(mass, list);








        }
    }
}