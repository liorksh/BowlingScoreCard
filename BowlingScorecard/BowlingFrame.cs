using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    /// <summary>
    /// A container of a frame. Doesn't have any logic inside, since the logic is set by the Game.
    /// </summary>
    public class BowlingFrame
    {
        // Variables can be initiated only by the class itself. It enforces immutability 
        public int Try1 { get; private set; }
        public int Try2 { get; private set; }
        public FrameTypeEnum FrameType { get; private set; }
        public int NumOfDroppedPins {  get{  return Try1 + Try2; }    }

        /// <summary>
        /// The constructor sets the variables. From this point, they cannot be changed externally.
        /// </summary>
        public BowlingFrame(int try1, int try2, FrameTypeEnum frameType)
        {
            Try1 = try1;
            Try2 = try2;

            // Once the frame is set, its type is fixed.
            FrameType = frameType;
        }

        public BowlingFrame(BowlingFrame frame)
        {
            Try1 = frame.Try1;
            Try2 = frame.Try2;
            FrameType = frame.FrameType;
        }
       
        public bool IsValid()
        {
            if (NumOfDroppedPins > BowlingGameExtenstions.NUM_OF_PINS &&
                FrameType!= FrameTypeEnum.BonusFrame)
                return false;

            if (Try1<0 || Try2 < 0)
                return false;

            return true;
        }
    }
}
