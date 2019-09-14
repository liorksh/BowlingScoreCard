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
        /// Create a new score card with additional new frame
        /// </summary>
        public ScoreCard Add(BowlingFrame frame)
        {
            return new ScoreCard(ImmutableArray.Create<BowlingFrame>().
                AddRange(Frames).
                Add(frame));
        }

        public BowlingFrame GetFrame(int index)
        {
            if (index < 0 || index >= Frames.Length)
                return null;

            return Frames[index];
        }

    


        /// <summary>
        /// Generates an empty score cards and initiate all the frames (to avoid NULL).
        /// The score card is a short array that doesn't consume a lot of memory, thus it's easier to initiate it this way. 
        /// </summary>
        /// <returns></returns>
        public static ScoreCard GenerteEmptyScoreCards()
        {
            //ImmutableArray<BowlingFrame>.Builder builder = new ImmutableArray<BowlingFrame>().ToBuilder();
            //builder.Capacity = Game.NUM_OF_REGULAR_ROUNDS + Game.EXTRA_ROUNDS;

            return new ScoreCard(new BowlingFrame[0]);
        }
    }
}
