using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingGame
{
    public class Game
    {
        public const int EXTRA_ROUNDS = 1;
        public const int NUM_OF_REGULAR_ROUNDS = 10;
        public const int NUM_OF_PINS = 10;

        #region Unused functions

        private BowlingFrame[] GenerteEmptyScoreCards_Forward(int maxArrayLength, int currentIndex)
        {
            if (currentIndex == maxArrayLength)
            {
                return new BowlingFrame[currentIndex];
            }

            BowlingFrame[] scoreCard = GenerteEmptyScoreCards(maxArrayLength, currentIndex + 1);

            BowlingFrame newFrame = new BowlingFrame();
            scoreCard[currentIndex] = newFrame;

            return scoreCard;
        }

        /// <summary>
        /// Create an empty score card using for loop
        /// </summary>
        private BowlingFrame[] GenerteEmptyScoreCards_UsingLoop()
        {
            int maxNumOfRound = NUM_OF_REGULAR_ROUNDS + EXTRA_ROUNDS;
            BowlingFrame [] frames = new BowlingFrame[maxNumOfRound];
            for(int i=0;i< maxNumOfRound; i++)
            {
                frames[i] = new BowlingFrame();
            }

            return frames;
        }
        #endregion

        /// <summary>
        /// Generates an empty score cards and initiate all the frames (to avoid NULL).
        /// The score card is a short array that doesn't consume a lot of memory, thus it's easier to initiate it this way. 
        /// </summary>
        /// <returns></returns>
        public static BowlingFrame[] GenerteEmptyScoreCards()
        {
            return GenerteEmptyScoreCards(Game.NUM_OF_REGULAR_ROUNDS+EXTRA_ROUNDS, Game.NUM_OF_REGULAR_ROUNDS + EXTRA_ROUNDS);
        }

        /// <summary>
        /// Adhering to the functional programming concepts:
        /// 1. Avoid changing an existing variable
        /// 2. Avoid loops
        /// </summary>
        private static BowlingFrame[] GenerteEmptyScoreCards(int maxArrayLength, int currentIndex)
        {
            if (currentIndex == 0)
            {
                return new BowlingFrame[currentIndex];
            }

            // Creating a new array to avoid changing the array that was received as a parameter. 
            BowlingFrame[] scoreCard = GenerteEmptyScoreCards(maxArrayLength, currentIndex - 1);

            BowlingFrame[] newScoreCard = new BowlingFrame[currentIndex];

            // The Array.Copy operation is costly in C#, however, it was chosen to exemplify the Functional Programing principle.  
            Array.Copy(scoreCard, newScoreCard, currentIndex - 1);

            BowlingFrame newFrame = new BowlingFrame();
            newScoreCard[currentIndex-1] = newFrame;

            return newScoreCard;
        }

        public static BowlingFrame[] RollNewFrame(BowlingFrame[] scoreCard, int tryNo1, int tryNo2)
        {
            if(scoreCard.Length == NUM_OF_REGULAR_ROUNDS+EXTRA_ROUNDS)
            {
                throw new IllegalBowlingActionException("The game exceeds from its maximum number of frames");
            }

            if( IsEligibleForAnotherTry(scoreCard, scoreCard.Length)==false)
            {
                throw new IllegalBowlingActionException("The game has reach to its maximum rounds");
            }
            
            BowlingFrame[] newScoreCard = new BowlingFrame[scoreCard.Length+1];
            Array.Copy(scoreCard, newScoreCard, scoreCard.Length);

            BowlingFrame newFrame = new BowlingFrame() { Try1 = tryNo1, Try2 = tryNo2 };
            newScoreCard[newScoreCard.Length - 1] = newFrame;

            // Validate the new frame
            if (Game.IsValid(newScoreCard) == false)
            {
                throw new IllegalBowlingActionException("An invalid frame");
            }

            return newScoreCard;
        }

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

        public static bool IsValid(BowlingFrame[] frames)
        {
            if (frames.Length > NUM_OF_REGULAR_ROUNDS + EXTRA_ROUNDS)
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

        private static bool IsValid(BowlingFrame[] frames, int length)
        {
            if (length <= 0)
                return true;

            return BowlingFrame.IsValid(frames[length]) && IsValid(frames, length - 1);
        }

        public static int GetScore(BowlingFrame[] frames)
        {
            return GetScore(frames, frames.Length - 1);
        }

        private static int GetScore(BowlingFrame[] frames, int length)
        {
            if (length < 0 || length>=frames.Length)
                return 0;

            int additionalScore;
            int score = GetFrameScore(frames, length) + GetScore(frames, length - 1);

            additionalScore = CalculateAdditionalScore(frames, length);

            return score+ additionalScore;
        }

        private static int GetFrameScore(BowlingFrame[] frames, int index)
        {
            if (IsExtraRound(index))
                return 0;

            return frames[index].Score;
        }

        private static bool IsExtraRound(int length)
        {
            return (length > NUM_OF_REGULAR_ROUNDS-1);
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

                if ((GetFrameType(frames, index - 2) == FrameTypeEnum.Strike))
                {
                    scoreIfPreviousWasStrike = frames[index].Try1;
                }
                
                return additonalScore + scoreIfPreviousWasStrike;
            }

            return 0;
        }

        private static FrameTypeEnum GetFrameType(BowlingFrame[] frames, int index)
        {
            if (index < 0 || index >= frames.Length)
                return FrameTypeEnum.Empty;

            return BowlingFrame.GetFramType(frames[index]);
        }

        public int Roll(BowlingFrame[] frames)
        {
            return 0;
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

        public string DisplayScoreCard(BowlingFrame[] frames)
        {
            StringBuilder result = new StringBuilder();
            int counter = 0;
            foreach(BowlingFrame frame in frames)
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
