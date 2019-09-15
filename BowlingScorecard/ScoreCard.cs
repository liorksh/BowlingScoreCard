using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace BowlingGame
{
    public class ScoreCard
    {
        // Hold the frames a private list, which is not accessible externally.
        private BowlingFrame[] Frames { get; set; }

        public int Length { get { return (Frames == null) ? 0 : Frames.Length; } }

        public ScoreCard(BowlingFrame[] frames)
        {
            // copy the array into a mutable array, to avoid any changes of the frames' values (by-ref);
            Frames = ImmutableArray.Create<BowlingFrame>().
                AddRange(frames).
                ToBuilder().
                ToArray();
        }

        /// <summary>
        /// Private constructor to create a list from an immutable array.
        /// To enforce immutability, this class uses immutable collections:  https://msdn.microsoft.com/en-us/magazine/mt795189.aspx
        /// </summary>
        private ScoreCard(ImmutableArray<BowlingFrame> frames)
        {
            Frames = frames.ToBuilder().ToArray();
        }

        /// <summary>
        /// Return a copy of all the frames.
        /// </summary>
        public ImmutableArray<BowlingFrame> GetAllFrames()
        {
            return ImmutableArray.Create<BowlingFrame>().
                AddRange(Frames);
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
        public ScoreCard AddRange(BowlingFrame[] newFrames)
        {
            return new ScoreCard(ImmutableArray.Create<BowlingFrame>().
                AddRange(Frames).
                AddRange(newFrames));
        }

        /// <summary>
        /// Returns a copy of a frame based on its index. If the index is not valid, the method returns NULL.
        /// The return value is a copy of the original object to ensure it is immutable.
        /// </summary>
        public BowlingFrame GetFrame(int index)
        {
            // Check if the given index is valid. If yes - return a copy of BowlingFrame, otherwise, return NULL.
            return (BowlingGameExtenstions.IsIndexExists(Frames, index)) ?
                new BowlingFrame(BowlingGameExtenstions.GetItemFromArray<BowlingFrame>(Frames, index)) :
                null;
        }

        /// <summary>
        /// Returns the frame type, based on the requested index. If the requested index is invalid, the return value is Empty.
        /// This method is more efficient than calling GetFrame, as it avoids creation of a new BowlingFrame instance.
        /// </summary>
        public FrameTypeEnum GetFrameType(int index)
        {
            return BowlingGameExtenstions.GetItemFromArray<BowlingFrame>(Frames, index)?.FrameType?? FrameTypeEnum.Empty;

            /// This is the inefficient way to return the frame's type.
            //return (GetFrame(BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS - 1)?.FrameType) ?? FrameTypeEnum.Empty;
        }

        /// <summary>
        /// Generates an empty score cards. The score card will include more frames as long as the game continues.
        /// </summary>
        public static ScoreCard GenerteEmptyScoreCards()
        {
            return new ScoreCard(new BowlingFrame[0]);
        }

        #region Calculate Scores
        public int? GetFrameScore(int index)
        {
            return BowlingGameExtenstions.GetItemFromArray<int?>(GetFramesScores(), index);
            
            // The previous way to return a value:
            //if (index < 0 || index >= Frames.Length)
            //    return null;

            //return GetFramesScores()[index];
        }

        /// <summary>
        /// Calculate the score of each frame, starting from the first until the last.
        /// If the frame's score cannot be determined yet (Split or Strike) - keep the score value as NULL.
        /// </summary>
        public int?[] GetFramesScores()
        {
            int?[] scores = new int?[Frames.Length];

            for (int i = 0; i < Frames.Length && i < BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS; i++)
            {
                if (Frames[i].FrameType == FrameTypeEnum.Normal)
                {
                    scores[i] = Frames[i].NumOfDroppedPins;
                }
                else if (Frames[i].FrameType == FrameTypeEnum.Spare && 
                    BowlingGameExtenstions.IsIndexExists<BowlingFrame>(Frames, i + 1))
                {
                    scores[i] = ((GetFrame(i + 1)?.Try1) ?? 0) + Frames[i].NumOfDroppedPins;
                }
                else if (Frames[i].FrameType == FrameTypeEnum.Strike &&
                    GetFrameType(i + 1) == FrameTypeEnum.Strike &&
                    BowlingGameExtenstions.IsIndexExists<BowlingFrame>(Frames, i + 2))
                {
                    scores[i] = ((GetFrame(i + 1)?.Try1) ?? 0) + ((GetFrame(i + 2)?.Try1) ?? 0) + Frames[i].NumOfDroppedPins;
                }
                else if (Frames[i].FrameType == FrameTypeEnum.Strike &&
                    GetFrameType(i + 1) != FrameTypeEnum.Strike &&
                   BowlingGameExtenstions.IsIndexExists<BowlingFrame>(Frames, i + 1))
                {
                    scores[i] = ((GetFrame(i + 1)?.Try1) ?? 0) + ((GetFrame(i + 1)?.Try2) ?? 0) + Frames[i].NumOfDroppedPins;
                }
                else
                    scores[i] = null;
            }

            // Handle the edge case when the last round is Strike.
            if(Frames.Length == (BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS + BowlingGameExtenstions.EXTRA_ROUNDS))
            {
                if (GetFrameType(BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS - 1) == FrameTypeEnum.Strike)
                {
                    scores[BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS - 1] = Frames[BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS + BowlingGameExtenstions.EXTRA_ROUNDS - 1].NumOfDroppedPins +
                        Frames[BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS - 1].NumOfDroppedPins;
                }
            }

            return scores;
        }
        #endregion
    }
}
