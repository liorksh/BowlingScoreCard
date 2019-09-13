using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    /// <summary>
    /// A container of a frame. Doesn't have logic inside, since the logic is set by the Game.
    /// </summary>
    public class BowlingFrame
    {
        public int Try1 { get; set; }
        public int Try2 { get; set; }

        public Tuple<int,int> GetTries
        {
            get { return new Tuple<int, int>(Try1, Try2); }
        }

        static public FrameTypeEnum GetFramType(BowlingFrame frame)
        {
            return GetFramType(new Tuple<int, int>(frame.Try1, frame.Try2));
        }

        static public FrameTypeEnum GetFramType(Tuple<int,int> tries)
        {
            int triesSum = tries.Item1 + tries.Item2;

            if (triesSum == 0)
             return FrameTypeEnum.Empty;

            if (tries.Item1 == Game.NUM_OF_PINS)
                return FrameTypeEnum.Strike;

            else if (triesSum < Game.NUM_OF_PINS)
                return FrameTypeEnum.Normal;
             
            return  FrameTypeEnum.Spare;
        }

        public int Score
        {
            get
            {
                return Try1 + Try2;
            }
        }

        internal static bool IsValid(BowlingFrame bowlingFrame)
        {
            if (bowlingFrame.Score > Game.NUM_OF_PINS)
                return false;

            if (bowlingFrame.Try1<0 || bowlingFrame.Try2 < 0)
                return false;

            return true;
        }
    }
}
