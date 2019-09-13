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
        ///  Check the validity of a frame
        /// </summary>
        public static bool IsValid(this BowlingFrame frame, int numOfPins)
        {
            return frame.Try1>=0 &&
                frame.Try2>=0 &&
                ((frame.Try1 + frame.Try2) <= numOfPins);
        }
       
        
        /// <summary>
        ///  Print the results of the game.
        ///// </summary>
        //public static string PrintResults(this BowlingGame game)
        //{
        //    StringBuilder results = new StringBuilder();
        //    List<BowlingFrame> frames = game.GetGameFrames();

        //    for(int i=0;i< frames.Count; i++)
        //    {
        //        results.AppendLine(frames[i]?.PrintFrame());
        //    }
            
        //    return results.ToString();
        //}

        /// <summary>
        /// Print frame
        /// </summary>
        public static string PrintFrame(this BowlingFrame frame)
        {
            FrameTypeEnum frameType = BowlingFrame.GetFramType(frame);
            if (frameType == FrameTypeEnum.Strike)
                return "X";

            if (frameType == FrameTypeEnum.Spare)
                return $"{frame.Try1} /";

            return $"{frame.Try1} | {frame.Try2}";
        }
    }
}
