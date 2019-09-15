using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Linq;

namespace BowlingGame
{
    /// <summary>
    /// General:
    /// This class manages a bowling game. 
    /// The game plays are managed in the ScoreCard object.
    /// </summary>
    public class Game
    {
        public static ScoreCard GenerteEmptyScoreCards()
        {
            return ScoreCard.GenerteEmptyScoreCards();
        }

        public static ScoreCard RollNewFrame(ScoreCard scoreCard, int tryNo1, int tryNo2)
        {
            return RollNewFrame(scoreCard, new BowlingFrame(tryNo1, tryNo2));
        }

        public static ScoreCard RollNewFrame(ScoreCard scoreCard, BowlingFrame newFrame)
        {
            if (!newFrame.IsValid())
            {
                throw new IllegalBowlingActionException("Invalid frame");
            }

            if (IsEligibleForAnotherTry(scoreCard) == false)
            {
                throw new IllegalBowlingActionException("The game has reach to its maximum rounds");
            }

            ScoreCard newScoreCard = scoreCard.Add(newFrame);

            // Validate the new set of frames
            if (Game.IsValid(newScoreCard) == false)
            {
                throw new IllegalBowlingActionException("An invalid frame was added");
            }
            
            return newScoreCard;
        }

        /// <summary>
        /// The method indicates whether the game is over or not.
        /// </summary>
        /// <param name="scoreCard"></param>
        /// <returns></returns>
        public static bool IsEligibleForAnotherTry(ScoreCard scoreCard)
        {
            if (IsIncludeExtraRound(scoreCard.Length))
                return false;

            if (IsInNormalRounds(scoreCard.Length))
                return true;
            
            // Get the type of the last round
            FrameTypeEnum previousFrameType = scoreCard.GetFrameType(BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS-1);

            // this is an extra frame and the first try is Strike
            if (previousFrameType == FrameTypeEnum.Spare ||
                  previousFrameType == FrameTypeEnum.Strike)
                return true;

            // this is an extra frame, but the score is not Spare
            return false;
        }
        
        public static bool IsValid(ScoreCard scoreCard)
        {
            if (IsBeyondExtraRound(scoreCard.Length))
            {
                return false;
            }

            // validate the extra round
            if (scoreCard.Length == BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS+ BowlingGameExtenstions.EXTRA_ROUNDS)
            {
                int currentFrameIndex = scoreCard.Length - 1;
                // get the frame's type of the previous round.
                FrameTypeEnum previousFrameType = GetFrameType(scoreCard, currentFrameIndex - 1);

                // if the last round is Spare, hen having a second try in the extra round is an invalid move.
                if(previousFrameType == FrameTypeEnum.Spare &&
                    scoreCard.GetFrame(currentFrameIndex).Try2 > 0)
                {
                    return false;
                }
            }

            return IsValid(scoreCard, scoreCard.Length - 1);
        }

        /// <summary>
        /// Validating a set of frames. 
        /// The validation is based on recursion, to adhere the Functional Programming principles.
        /// </summary>
        private static bool IsValid(ScoreCard scoreCards, int length)
        {
            if (length <= 0)
                return true;

            return (scoreCards.GetFrame(length)?.IsValid()??false) && IsValid(scoreCards, length - 1);
        }

        #region Score calculation methods
        public static int GetScore(ScoreCard scoreCard)
        {
            return scoreCard.GetFramesScores().Sum().Value;

            // Deprecated way to calculate the score
            //return GetScore(scoreCard, scoreCard.Length - 1);
        }

        // Calculate the Game's score backwards, by checking the values of the previous frames.
        // This approach is not accurate enough, as it changes the score of frames that were already completed;
        // thus, it provide inaccurate scores when the frame is strike or spare.
        //private static int GetScore(ScoreCard scoreCard, int index)
        //{
        //    if (index < 0 || index>= scoreCard.Length)
        //        return 0;

        //    int additionalScore;
        //    int score = GetFrameDroppedPins(scoreCard, index) + GetScore(scoreCard, index - 1);

        //    additionalScore = CalculateAdditionalScore(scoreCard, index);

        //    return score + additionalScore;
        //}

        /// <summary>
        /// This method returns the additional score due to Spare or Strikes.
        /// This calculation is going backwards.
        ///// </summary>
        //private static int CalculateAdditionalScore(ScoreCard scoreCard, int index)
        //{
        //    FrameTypeEnum previousFrameType = GetFrameType(scoreCard, index - 1);

        //    if (previousFrameType == FrameTypeEnum.Spare)
        //        return scoreCard.GetFrame(index).Try1;

        //    if (previousFrameType == FrameTypeEnum.Strike)
        //    {
        //        int additonalScore = scoreCard.GetFrame(index).Try1 + scoreCard.GetFrame(index).Try2;
        //        int scoreIfPreviousWasStrike = 0;

        //        // Check if the previous-previous round was Strike. If yes, need to add the first try to the score,
        //        // since a Strike rule is to add the consecutive two tries. 
        //        if ((GetFrameType(scoreCard, index - 2) == FrameTypeEnum.Strike))
        //        {
        //            scoreIfPreviousWasStrike = scoreCard.GetFrame(index).Try1;
        //        }

        //        // Add the additional score of the current round and the prev-prev score (if needed).
        //        return additonalScore + scoreIfPreviousWasStrike;
        //    }

        //    return 0;
        //}

        public static int? GetFrameScore(ScoreCard scoreCard, int index)
        {
            return scoreCard.GetFrameScore(index);
        }

        //private static int GetFrameDroppedPins(ScoreCard scoreCard, int index)
        //{
        //    // Do not return the score of the extra round (if exists), as it is calculated already.
        //    if (IsIndexExtraRound(index))
        //        return 0;

        //    return scoreCard.GetFrame(index).NumOfDroppedPins;
        //}
        #endregion

        #region Get game status methods
        private static bool IsBeyondExtraRound(int length)
        {
            return length > BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS + BowlingGameExtenstions.EXTRA_ROUNDS;
        }

        private static bool IsIncludeExtraRound(int length)
        {
            return length >= BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS+ BowlingGameExtenstions.EXTRA_ROUNDS;
        }

        // Deprecated method after changing the score calculation approach
        //private static bool IsIndexExtraRound(int length)
        //{
        //    return length == BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS + BowlingGameExtenstions.EXTRA_ROUNDS - 1;
        //}

        private static bool IsInNormalRounds(int length)
        {
            return (length < BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS);
        }
        #endregion


        /// <summary>
        /// The method returns the score of a frame, based on its index.
        /// If the index is invalid, the method returns 0.
        /// </summary>
        public static FrameTypeEnum GetFrameType(ScoreCard scoreCard, int index)
        {
            // The inefficient way:
            // return (scoreCard.GetFrame(index)?.FrameType) ?? FrameTypeEnum.Empty;

            // Returns the Frame based on the index. If the index is invalid, the returned value is Empty.
            return scoreCard.GetFrameType(index);
        }
    }
}
