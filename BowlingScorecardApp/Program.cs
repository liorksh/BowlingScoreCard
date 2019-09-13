using System;

namespace BowlingScorecardApp
{
    class Program
    {
        /*
         * 
         *       private static string QUIT = "-q";

        static void Main(string[] args)
        {
        1.	Create an empty score card
2.	Given a score card, score a frame
3.	Determine if a game is complete - if so, provide the final score


            Console.WriteLine($"Hello there, welcome to a Bowling game!\nPress Enter to start");
            Console.ReadLine();

            BowlingGame game = new BowlingGame();
            game.StartGame();

            int droppedPins;

            // run as long as the game runs
            while (game.IsGameOver==false)
            {
                string input;

                if(game.CurrentRound>game.Frames)
                {
                    PrintInformation("You have an extra try :). ");
                }
                else
                {
                    Console.Write($"Round no. {game.CurrentRound}. ");
                }
                
                Console.WriteLine("Throw the ball and enter the result (-q to quit). Let's Roll!");
                input = Console.ReadLine();

                if (input.ToLower() == QUIT)
                {
                    PrintError("Notice: You've chosen to stop the game. Sorry to see you leave");
                    break;
                }

                if (int.TryParse(input, out droppedPins)==false)
                {
                    PrintError("Invalid number! Please try again");
                    continue;
                }

                try
                {
                    game.Roll(droppedPins);

                    if(droppedPins==Game.NUM_OF_PINS)
                    {
                        PrintInformation("Strike!!! Well done!");
                    }
                }
                catch(Exception ex)
                {
                    PrintError($"An error occurred: {ex.Message}");
                }
       
                Console.WriteLine($"The score is: {Game.GetScore(frames)}.");
            }

            if(Game.GetScore(frames)==game.MaxPossibleScore)
            {
                PrintInformation("*** You are a the KING. Big Lebowski - behind you!");
            }

            Console.WriteLine($"\nThe game's frame were:\n{game.PrintResults()}\nPress Enter to exit, goodbye..");
            Console.ReadLine();
        }

        private static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void PrintInformation(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
         */
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
