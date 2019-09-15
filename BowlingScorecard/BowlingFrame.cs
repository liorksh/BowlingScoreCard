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
        // Variables can be initiated only by the class itself. It enforces immutability 
        public int Try1 { get; private set; }
        public int Try2 { get; private set; }
        public FrameTypeEnum FrameType { get; private set; }

        /// <summary>
        /// The constructor sets the variables. From this point, they cannot be changed externally.
        /// </summary>
        public BowlingFrame(int try1, int try2)
        {
            Try1 = try1;
            Try2 = try2;

            // Once the frame is set, its type is fixed.
            FrameType = GetFramType(new Tuple<int, int>(Try1, Try2));
        }

        public BowlingFrame(BowlingFrame frame)
        {
            Try1 = frame.Try1;
            Try2 = frame.Try2;
            FrameType = frame.FrameType;
        }

        private FrameTypeEnum GetFramType(Tuple<int,int> tries)
        {
            int triesSum = tries.Item1 + tries.Item2;

            if (tries.Item1 == BowlingGameExtenstions.NUM_OF_PINS)
                return FrameTypeEnum.Strike;

            else if (triesSum < BowlingGameExtenstions.NUM_OF_PINS)
                return FrameTypeEnum.Normal;
             
            return  FrameTypeEnum.Spare;
        }

        public int NumOfDroppedPins
        {
            get
            {
                return Try1 + Try2;
            }
        }

        public bool IsValid()
        {
            if (NumOfDroppedPins > BowlingGameExtenstions.NUM_OF_PINS)
                return false;

            if (Try1<0 || Try2 < 0)
                return false;

            return true;
        }
    }
}
