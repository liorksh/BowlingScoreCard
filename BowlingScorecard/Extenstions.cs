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

    public static class Extenstions
    {       
        /// <summary>
        /// Print frame
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
        /// An extension method to display the tries in each frame of the game.
        /// </summary>
        /// <param name="scoreCard"></param>
        /// <returns></returns>
        public static string DisplayScoreCard(this ScoreCard scoreCard)
        {
            StringBuilder result = new StringBuilder();

            foreach (BowlingFrame frame in scoreCard.Frames)
            {
                result.Append($"{frame.PrintFrame()} ");
            }

            return result.ToString();
        }
    }
}
