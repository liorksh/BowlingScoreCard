using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace BowlingGame
{
    /// <summary>
    /// General:
    /// 1. To enforce immutability, the program uses immutable collections:  https://msdn.microsoft.com/en-us/magazine/mt795189.aspx
    /// </summary>
    public class Game
    {
        public const int EXTRA_ROUNDS = 1;
        public const int NUM_OF_REGULAR_ROUNDS = 10;
        public const int NUM_OF_PINS = 10;

        public static ScoreCard RollNewFrame(ScoreCard scoreCard, int tryNo1, int tryNo2)
        {
            BowlingFrame newframe = new BowlingFrame(tryNo1, tryNo2);
            if(!BowlingFrame.IsValid(newframe))
            {
                throw new IllegalBowlingActionException("Invalid frame");
            }

            if (IsEligibleForAnotherTry(scoreCard) == false)
            {
                throw new IllegalBowlingActionException("The game has reach to its maximum rounds");
            }

            ScoreCard newScoreCard = scoreCard.Add(new BowlingFrame(tryNo1, tryNo2));

            // Validate the new set of frames
            if (Game.IsValid(newScoreCard) == false)
            {
                throw new IllegalBowlingActionException("An invalid frame was added");
            }

            return newScoreCard;
        }

        /*
         * This method is redundant
        private static BowlingFrame[] RollNewFrame(BowlingFrame[] scoreCard, int tryNo1, int tryNo2)
        {
            if(scoreCard.Length == NUM_OF_REGULAR_ROUNDS+EXTRA_ROUNDS)
            {
                throw new IllegalBowlingActionException("The game exceeds from its maximum number of frames");
            }

            if( IsEligibleForAnotherTry(scoreCard, scoreCard.Length)==false)
            {
                throw new IllegalBowlingActionException("The game has reach to its maximum rounds");
            }
            
            ImmutableArray<BowlingFrame> arrScoreCard = ImmutableArray.Create<BowlingFrame>().AddRange(scoreCard);
           
            BowlingFrame newFrame = new BowlingFrame(tryNo1, tryNo2);
            ImmutableArray<BowlingFrame> newScoreCard = arrScoreCard.Add(newFrame);
            
            // Validate the new set of frames
            if (Game.IsValid(newScoreCard.ToBuilder().ToArray()) == false)
            {
                throw new IllegalBowlingActionException("An invalid frame");
            }

            return newScoreCard.ToBuilder().ToArray();
        }
        */

        public static bool IsEligibleForAnotherTry(ScoreCard scoreCard)
        {
            if (IsBeyondExtraRound(scoreCard.Length))
                return false;

            if (IsInNormalRounds(scoreCard.Length))
                return true;
            
            // Get the type of the last round
            FrameTypeEnum previousFrameType = (scoreCard.GetFrame(Game.NUM_OF_REGULAR_ROUNDS-1)?.FrameType)?? FrameTypeEnum.Empty;

            // this is an extra frame and the first try is Strike
            if (previousFrameType == FrameTypeEnum.Spare ||
                  previousFrameType == FrameTypeEnum.Strike)
                return true;

            // this is an extra frame, but the score is not Spare
            return false;
        }
        
        /*
         * This method is redundant
        public static bool IsEligibleForAnotherTry(BowlingFrame[] frames, int frameNumber)
        {
            if (frameNumber < NUM_OF_REGULAR_ROUNDS)
                return true;

            FrameTypeEnum previousFrameType = GetFrameType(frames, frameNumber - 1);

            // this is an extra frame and the first try is Strike
            if (frameNumber == NUM_OF_REGULAR_ROUNDS &&
                 ((previousFrameType == FrameTypeEnum.Spare) ||
                  previousFrameType == FrameTypeEnum.Strike))
                return true;

            // this is an extra frame, but the score is not Spare
            return false;
        }
        */

        public static bool IsValid(ScoreCard scoreCard)
        {
            return IsValid(scoreCard.Frames);
        }

        private static bool IsValid(BowlingFrame[] frames)
        {
            if (IsBeyondExtraRound(frames.Length))
            {
                return false;
            }

            // validate the extra round
            if (frames.Length == NUM_OF_REGULAR_ROUNDS+EXTRA_ROUNDS)
            {
                int currentFrameIndex = frames.Length - 1;
                FrameTypeEnum previousFrameType = GetFrameType(frames, currentFrameIndex - 1);
                if(previousFrameType == FrameTypeEnum.Spare &&
                    frames[currentFrameIndex].Try2>0)
                {
                    return false;
                }
            }

            return IsValid(frames, frames.Length - 1);
        }

        /// <summary>
        /// Validating a set of frames. 
        /// The validation is based on recursion, to adhere the Functional Programming principles.
        /// </summary>
        private static bool IsValid(BowlingFrame[] frames, int length)
        {
            if (length <= 0)
                return true;

            return BowlingFrame.IsValid(frames[length]) && IsValid(frames, length - 1);
        }

        public static int GetScore(ScoreCard scoreCard)
        {
            return GetScore(scoreCard.Frames);
        }

        private static int GetScore(BowlingFrame[] frames)
        {
            return GetScore(frames, frames.Length - 1);
        }

        private static int GetScore(BowlingFrame[] frames, int index)
        {
            if (index < 0 || index>=frames.Length)
                return 0;

            int additionalScore;
            int score = GetFrameScore(frames, index) + GetScore(frames, index - 1);

            additionalScore = CalculateAdditionalScore(frames, index);

            return score+ additionalScore;
        }

        private static int GetFrameScore(BowlingFrame[] frames, int index)
        {
            // Do not return the score of the extra round (if exists), as it is calculated already.
            if (IsIndexExtraRound(index))
                return 0;

            return frames[index].Score;
        }

        private static bool IsBeyondExtraRound(int length)
        {
            return length > Game.NUM_OF_REGULAR_ROUNDS + EXTRA_ROUNDS;
        }

        private static bool IsIncludeExtraRound(int length)
        {
            return length >= Game.NUM_OF_REGULAR_ROUNDS+ EXTRA_ROUNDS;
        }

        private static bool IsIndexExtraRound(int length)
        {
            return length == Game.NUM_OF_REGULAR_ROUNDS + EXTRA_ROUNDS-1;
        }

        //private static bool IsExtraRound(int length)
        //{
        //    return (length == Game.NUM_OF_REGULAR_ROUNDS);
        //}

        private static bool IsInNormalRounds(int length)
        {
            return (length < Game.NUM_OF_REGULAR_ROUNDS);
        }

        private static int CalculateAdditionalScore(BowlingFrame[] frames, int index)
        {
            FrameTypeEnum previousFrameType = GetFrameType(frames, index - 1);

            if (previousFrameType == FrameTypeEnum.Spare)
                return frames[index].Try1;

            if (previousFrameType == FrameTypeEnum.Strike)
            {
                int additonalScore = frames[index].Try1 + frames[index].Try2;
                int scoreIfPreviousWasStrike = 0;

                // Check if the previous-previous round was Strike. If yes, need to add the first try to the score,
                // since a Strike rule is to add the consecutive two tries. 
                if ((GetFrameType(frames, index - 2) == FrameTypeEnum.Strike))
                {
                    scoreIfPreviousWasStrike = frames[index].Try1;
                }
                
                // Add the additional score of the current round and the prev-prev score (if needed).
                return additonalScore + scoreIfPreviousWasStrike;
            }

            return 0;
        }

        /// <summary>
        /// The method returns the score of a frame, based on its index.
        /// If the index is invalid, the method returns 0.
        /// </summary>
        public static FrameTypeEnum GetFrameType(BowlingFrame[] frames, int index)
        {
            if (index < 0 || index >= frames.Length)
                return FrameTypeEnum.Empty;

            return BowlingFrame.GetFramType(frames[index]);
        }

        //public GameStatusEnum GetGameStatus(BowlingFrame[] frames)
        //{
        //    GameStatusEnum gameStatus;
        //    FrameTypeEnum firstFrameeType = BowlingFrame.GetFramType(frames[0].GetTries);
        //    int maxNumOfFrames = frames.Length;

        //    if (firstFrameeType == FrameTypeEnum.Empty)
        //        gameStatus = GameStatusEnum.NotInitialized;

        //    FrameTypeEnum lastFrameeType = BowlingFrame.GetFramType(frames[maxNumOfFrames - EXTRA_ROUNDS].GetTries);

        //    if (firstFrameeType == FrameTypeEnum.Empty)
        //        gameStatus = GameStatusEnum.NotInitialized;

        //    return null;
        //}

        public static string DisplayScoreCard(ScoreCard scoreCard)
        {
            StringBuilder result = new StringBuilder();
            int counter = 0;

            foreach(BowlingFrame frame in scoreCard.Frames)
            {
                if (counter % 3 == 0)
                    result.AppendLine(Environment.NewLine);
                    
                result.AppendLine(frame.ToString());
                counter = counter + 1;
            }

            return result.ToString();
        }
    }
}
