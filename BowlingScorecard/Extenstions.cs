using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    /// <summary>
    /// Holds the type of one try
    /// </summary>
    public enum FrameTypeEnum
    {
        Empty=0,
        Normal,
        Spare,
        Strike
    }

    /// <summary>
    /// Holds the game's status
    /// </summary>
    public enum GameStatusEnum
    {
        NotInitialized = 1,
        GameStarted,
        ExtraRound,
        GameOver
    }
    
    public static class BowlingGameExtenstions
    {
        // Constant parameters of a bowling game.
        public const int EXTRA_ROUNDS = 1;
        public const int NUM_OF_REGULAR_ROUNDS = 10;
        public const int NUM_OF_PINS = 10;

        /// <summary>
        /// Returns indication whether a given index is within the boundaries of the array.
        /// </summary>
        public static bool IsIndexExists<T>(T[] array, int index)
        {
            if (array == null || index < 0 || index >= array.Length)
                return false;

            return true;
        }

        /// <summary>
        /// Returns an item from a given array and index. If the index is invalid, the return value is default.
        /// </summary>
        public static T GetItemFromArray<T>(T[] array, int index)
        {
            if (array == null || index < 0 || index >= array.Length)
                return default(T);

            return array[index];
        }

        /// <summary>
        /// Print a single frame
        /// </summary>
        public static string PrintFrame(this BowlingFrame frame)
        {
            if (frame.FrameType == FrameTypeEnum.Strike)
                return "(X)";

            if (frame.FrameType == FrameTypeEnum.Spare)
                return $"({frame.Try1}, /)";

            return $"({frame.Try1} , {frame.Try2})";
        }

        /// <summary>
        /// An extension method to display the frames and their scores.
        /// </summary>
        public static string DisplayScoreCard(this ScoreCard scoreCard)
        {
            StringBuilder result = new StringBuilder();

            foreach (BowlingFrame frame in scoreCard.GetAllFrames())
            {
                result.Append($"{frame.PrintFrame()}\t");
            }
            result.AppendLine();
            foreach (int? score in scoreCard.GetFramesScores())
            {
                result.Append($"{score}\t");
            }

            return result.ToString();
        }
    }
}
