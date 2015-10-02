using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGameOfSpeed
{
    class Program
    {
        static bool playAgain = true;

        public static int speed = 70;
        public static int score = 0;
        public static Random r = new Random();
        public static int minSpeed = 50;
        public static int maxSpeed = 100;
        public static bool faultyBreak = false;
        public static System.Media.SoundPlayer player = null;


        static void Main(string[] args)
        {
            while (playAgain)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Clear();
                StartScreen();
                GameLogic();
                Console.WriteLine("Whant to play agin  ?");
                string again = Console.ReadLine();
                if(again == "y" || again == "Y" || again == "YES")
                {
                    playAgain = true;
                }
                else
                {
                    playAgain= false;
                }
            }
        }
        static void StartScreen()
        {

            Console.WriteLine(@"
 _______  __   __  _______    __    _  _______  _______  ______            
|       ||  | |  ||       |  |  |  | ||       ||       ||      |           
|_     _||  |_|  ||    ___|  |   |_| ||    ___||    ___||  _    |          
  |   |  |       ||   |___   |       ||   |___ |   |___ | | |   |          
  |   |  |       ||    ___|  |  _    ||    ___||    ___|| |_|   |          
  |   |  |   _   ||   |___   | | |   ||   |___ |   |___ |       |          
  |___|  |__| |__||_______|  |_|  |__||_______||_______||______|           
 _______  _______  ______      _______  _______  _______  _______  ______  
|       ||       ||    _ |    |       ||       ||       ||       ||      | 
|    ___||   _   ||   | ||    |  _____||    _  ||    ___||    ___||  _    |
|   |___ |  | |  ||   |_||_   | |_____ |   |_| ||   |___ |   |___ | | |   |
|    ___||  |_|  ||    __  |  |_____  ||    ___||    ___||    ___|| |_|   |
|   |    |       ||   |  | |   _____| ||   |    |   |___ |   |___ |       |
|___|    |_______||___|  |_|  |_______||___|    |_______||_______||______| 

");

            Console.WriteLine("The Game of Speed");
            Console.WriteLine("You are driving a bus, on board is a bomb that will go off");
            Console.WriteLine("if the speed of the bus goes under 50 miles/hour");
            Console.WriteLine("Controls:");
            Console.WriteLine("Use up arrow to increase the speed and use spacebar to inject nitro");
            Console.WriteLine("Use down arrow to break and use Z to pull the handbrake");
            Console.WriteLine("Be carefull");
            Console.WriteLine("PRESS ANY KEY TO START");
            Console.ReadKey();
            Console.Clear();
        }

        static void GameLogic()
        {

            while (speed > minSpeed && speed < maxSpeed)
            {
                
                bool wasRightButtonPressed = true;
                Console.WriteLine("Carefull dont go under {0} or over {1}", minSpeed, maxSpeed);
                Console.WriteLine("Current speed:" + speed + "                                                          Score " + score);

                ConsoleKeyInfo keypressed = Console.ReadKey();
                if (keypressed.Key == ConsoleKey.UpArrow)
                {
                    speed++;
                    score++;
                    Console.Clear();
                }
                else if (keypressed.Key == ConsoleKey.DownArrow && !faultyBreak)
                {
                    speed--;
                    score++;
                    Console.Clear();
                }
                else if(keypressed.Key == ConsoleKey.Spacebar)
                {
                    speed += 10;
                    score++;
                }
                else if(keypressed.Key == ConsoleKey.Z)
                {
                    speed -= 10;
                    score++;
                }
                else
                {
                    Console.WriteLine("Wrong button pressed");
                   
                    wasRightButtonPressed = false;
                }
                if (wasRightButtonPressed)
                {
                    Event();
                }
            }
            EndGame();
        }

        private static void Event()
        {

            int random = r.Next(20);

            if (random == 0)
            {
                Console.WriteLine("Speedbump!!!");

                speed -= (5 * (score / 10));
            }
            else if (random == 1)
            {
                Console.WriteLine("Downwardslope ahead!!!");

                speed += (5 * (score / 10));
            }
            else if (random == 2)
            {
                Console.WriteLine("WARNIG!!! \n A wormhola a head no one knows how that will affect youre speed !!!");

                minSpeed = r.Next(0,100);
                maxSpeed = r.Next(minSpeed + 30, minSpeed + 100);

                speed = r.Next(minSpeed + 1, maxSpeed - 1);
            }
            else if (random == 4)
            {
                player = new System.Media.SoundPlayer("DragonSound.wav");
                player.Play();
                Console.WriteLine("A DRAGON APROCHES!!! \n DRIVE DRIVE DRIVE !!!");
                maxSpeed = speed + 100;
                minSpeed = speed - 1;
            }
            else if (random == 5 && !faultyBreak)
            {
                Console.WriteLine("A fault has come up in the breaks you can only use the handbreak");
                faultyBreak = true;
            }
            else if (random == 6 && faultyBreak)
            {
                Console.WriteLine("Someone has fixed your breaks, thank gods");
                faultyBreak = false;
            }
        }


        static void EndGame()

        {
            player = new System.Media.SoundPlayer("crash.wav");
            player.Play();
            
            for (int i = 0; i < 3; i++)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Clear();
                System.Threading.Thread.Sleep(200);
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Clear();
                System.Threading.Thread.Sleep(200);
            }



            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(35, 10);



            Console.WriteLine(@"

 _______  _______  __   __  _______    _______  __   __  _______  ______   
|       ||   _   ||  |_|  ||       |  |       ||  | |  ||       ||    _ |  
|    ___||  |_|  ||       ||    ___|  |   _   ||  |_|  ||    ___||   | ||  
|   | __ |       ||       ||   |___   |  | |  ||       ||   |___ |   |_||_ 
|   ||  ||       ||       ||    ___|  |  |_|  ||       ||    ___||    __  |
|   |_| ||   _   || ||_|| ||   |___   |       | |     | |   |___ |   |  | |
|_______||__| |__||_|   |_||_______|  |_______|  |___|  |_______||___|  |_|     ");
        }
    }
}
