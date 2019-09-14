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
            int inputOption = 0;
            Console.WriteLine($"Hello there, welcome to a Bowling game!\nPlease select one of the following:\n1) Enter the frames manually\n2) Auto generated values\nPlease select and press Enter to start");
            string result = Console.ReadLine();

            bool isManual = Int32.TryParse(result, out inputOption) ? inputOption == 1 : false;

            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
          
            // run as long as the game runs
            while (Game.IsEligibleForAnotherTry(scoreCard))
            {   
                Tuple<int, int> tries = GetNextFrame(isManual, scoreCard);
                if (tries == null)
                    break;

                try
                {
                    PrintInformation($"Try1 {tries.Item1}, Try2: {tries.Item2}");

                    scoreCard = Game.RollNewFrame(scoreCard, tries.Item1, tries.Item2);

                    if(tries .Item1== Game.NUM_OF_PINS)
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

            Console.WriteLine($"\nThe game's frame were:\n{scoreCard.DisplayScoreCard()}\nPress Enter to exit, goodbye..");
            Console.ReadLine();
        }

        /// <summary>
        /// Returns the next frame, which can be auto-generated or interactive (user's input).
        /// </summary>
        /// <param name="isManual"></param>
        /// <param name="scoreCard"></param>
        /// <returns></returns>
        private static Tuple<int, int> GetNextFrame(bool isManual, ScoreCard scoreCard)
        {
            int try1, try2;

            if (isManual)
            {
                Console.WriteLine("Throw the ball and enter the result (-q to quit). Let's Roll!");

                string input = Console.ReadLine();

                if (input.ToLower() == QUIT)
                {
                    PrintError("Notice: You've chosen to stop the game. Sorry to see you leave");
                    return null;
                }

                if (input.Split(DELIMITER).Length != 2)
                {
                    PrintError("Invalid input!");
                    return null;
                }


                if (int.TryParse(input.Split(DELIMITER)[0], out try1) == false ||
                    int.TryParse(input.Split(DELIMITER)[1], out try2) == false)
                {
                    PrintError("Invalid number!");
                    return null;
                }
            }
            else
            {
                Random rnd = new Random();
                try1 = rnd.Next(0, Game.NUM_OF_PINS+1);

                try2 = (Game.GetFrameType(scoreCard, Game.NUM_OF_REGULAR_ROUNDS) == FrameTypeEnum.Spare) ? 0 : rnd.Next(0, Game.NUM_OF_PINS - try1 + 1);
            }

            return new Tuple<int, int>(try1, try2);
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
