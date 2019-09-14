using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace BowlingGame
{
    public class ScoreCard
    {
        public BowlingFrame[] Frames { get; private set; }
        public int Length{ get { return (Frames == null) ? 0 : Frames.Length; }   }

        public ScoreCard(BowlingFrame[] frames)
        {
            Frames = frames;
        }

        public ScoreCard(ImmutableArray<BowlingFrame> frames)
        {
            Frames = frames.ToBuilder().ToArray();
        }

        /// <summary>
        /// Create a new score card with additional new frame.
        /// Copy the existing frames and add the new ones.
        /// </summary>
        public ScoreCard Add(BowlingFrame frame)
        {
            return new ScoreCard(ImmutableArray.Create<BowlingFrame>().
                AddRange(Frames).
                Add(frame));
        }

        /// <summary>
        /// Create a new score card with additional new frames.
        /// Copy the existing frames and add the new ones.
        /// </summary>
        public ScoreCard AddRange(BowlingFrame[] frames)
        {
            return new ScoreCard(ImmutableArray.Create<BowlingFrame>().
                AddRange(Frames).
                AddRange(frames));
        }

        /// <summary>
        /// Returns a copy of a frame based on its index. If the index is not valid, the method returns NULL.
        /// The return value is a copy of the original object to ensure it is immutable.
        /// </summary>
        public BowlingFrame GetFrame(int index)
        {
            if (index < 0 || index >= Frames.Length)
                return null;

            return new BowlingFrame(Frames[index]);
        }

        /// <summary>
        /// Returns the frame type, based on the requested index. If the requested index is invalid, the return value is Empty.
        /// This method is more efficient than calling GetFrame, as it avoids creation of a new BowlingFrame instance.
        /// </summary>
        public FrameTypeEnum GetFrameType(int index)
        {
            if (index < 0 || index >= Frames.Length)
                return FrameTypeEnum.Empty;

            /// This is the inefficient way to return the frame's type.
            //return (GetFrame(Game.NUM_OF_REGULAR_ROUNDS - 1)?.FrameType) ?? FrameTypeEnum.Empty;
            return Frames[index].FrameType;
        }

        /// <summary>
        /// Generates an empty score cards. The score card will include more frames as long as the game continues.
        /// </summary>
        public static ScoreCard GenerteEmptyScoreCards()
        {
            return new ScoreCard(new BowlingFrame[0]);
        }
    }
}
