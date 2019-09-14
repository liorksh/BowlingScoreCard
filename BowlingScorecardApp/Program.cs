using BowlingGame;
using System;

namespace BowlingScorecardApp
{
    public class Program
    {
        private static string QUIT = "-q";
        private static char DELIMITER = ',';

        static void Main(string[] args)
        {
            Console.WriteLine($"Hello there, welcome to a Bowling game!\nPress Enter to start");
            Console.ReadLine();

            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
          
            int try1, try2;

            // run as long as the game runs
            while (Game.IsEligibleForAnotherTry(scoreCard))
            {
                string input;
                
                Console.WriteLine("Throw the ball and enter the result (-q to quit). Let's Roll!");
                input = Console.ReadLine();

                if (input.ToLower() == QUIT)
                {
                    PrintError("Notice: You've chosen to stop the game. Sorry to see you leave");
                    break;
                }

                if(input.Split(DELIMITER).Length !=2)
                {
                    PrintError("Invalid input! Please try again");
                    continue;
                }

                
                if (int.TryParse(input.Split(DELIMITER)[0], out try1)==false ||
                    int.TryParse(input.Split(DELIMITER)[1], out try2) == false)
                {
                    PrintError("Invalid number! Please try again");
                    continue;
                }

                try
                {
                    scoreCard = Game.RollNewFrame(scoreCard, try1, try2);

                    if(try1==Game.NUM_OF_PINS)
                    {
                        PrintInformation("Strike!!! Well done!");
                    }
                }
                catch(Exception ex)
                {
                    PrintError($"An error occurred: {ex.Message}");
                }
       
                Console.WriteLine($"The score is: {Game.GetScore(scoreCard)}.");
            }

            if(Game.GetScore(scoreCard) ==Game.NUM_OF_PINS*30)
            {
                PrintInformation("*** You are a the KING. Big Lebowski - behind you!");
            }

            Console.WriteLine($"\nThe game's frame were:\n{Game.DisplayScoreCard(scoreCard)}\nPress Enter to exit, goodbye..");
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
    }
}
