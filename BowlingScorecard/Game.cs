using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Linq;

namespace BowlingGame
{
    /// <summary>
    /// This class manages a bowling game. The game's frames and score are managed in the ScoreCard object.
    /// This class do not have any members, it's only purpose is to activate and run scorecard operations.
    /// </summary>
    public class Game
    {
        // private contractor to avoid creation of Game instances. 
        private Game() { }

        /// <summary>
        /// Generates an empty scorecard
        /// </summary>
        /// <returns></returns>
        public static ScoreCard GenerteEmptyScoreCards()
        {
            return ScoreCard.GenerteEmptyScoreCards();
        }

        /// <summary>
        /// Rolls a new frame and returns a new instance of the scorecard
        /// </summary>
        public static ScoreCard RollNewFrame(ScoreCard scoreCard, int tryNo1, int tryNo2)
        {
            // creates a new frame (calculate its type based on the tries and the number of rounds (scorecard's length).
            BowlingFrame newFrame = new BowlingFrame(tryNo1, tryNo2, 
                GetFrameType(tryNo1, tryNo2, scoreCard.Length));

            // Check the validity of the new frame
            if (!newFrame.IsValid())
            {
                throw new IllegalBowlingActionException("Invalid frame");
            }

            // Ensure the scorecard is eligible or another frame, otherwise, an error is thrown
            if (IsEligibleForAnotherTry(scoreCard) == false)
            {
                throw new IllegalBowlingActionException("The game has reach to its maximum rounds");
            }

            // Add the new frame to the scorecard and returns a new instance of a scorecard.
            // The goal is to keep the instances immutable.
            ScoreCard newScoreCard = scoreCard.Add(newFrame);

            // Validate the new set of frames
            if (Game.IsValid(newScoreCard) == false)
            {
                throw new IllegalBowlingActionException("An invalid frame was added");
            }
            
            return newScoreCard;

            // Inner functions that returns the frame's type based on the number of round or the dropped pins.
            FrameTypeEnum GetFrameType(int item1, int item2, int count)
            {
                if (count == BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS)
                    return FrameTypeEnum.BonusFrame;
                if ((item1) == BowlingGameExtenstions.NUM_OF_PINS)
                    return FrameTypeEnum.Strike;
                if ((item1 + item2) == BowlingGameExtenstions.NUM_OF_PINS)
                    return FrameTypeEnum.Spare;

                return FrameTypeEnum.Normal;
            }
        }

        /// <summary>
        /// The method indicates whether the game is over or not.
        /// </summary>
        public static bool IsEligibleForAnotherTry(ScoreCard scoreCard)
        {
            // if the scorecard already "utilized" an extra round, then an additional round is not allowed.
            if (IsIncludeExtraRound(scoreCard.Length))
                return false;

            // if the round falls under the regular rounds, then it is a valid round
            if (IsInNormalRounds(scoreCard.Length))
                return true;
            
            // If reached to this condition, then it is an extra round; thus, gets the type of the last legit round
            FrameTypeEnum previousFrameType = scoreCard.GetFrameType(BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS-1);

            // Ensures the previous frame (which is the last regular round) falls under the category of strike or spare
            if (previousFrameType == FrameTypeEnum.Spare ||
                  previousFrameType == FrameTypeEnum.Strike)
                return true;

            // This is an extra frame, but the score is not spare or strike, therefore another round is not legit.
            return false;
        }
        
        /// <summary>
        /// Validates the scorecard.
        /// </summary>
        public static bool IsValid(ScoreCard scoreCard)
        {
            // Check if the number of frames has exceeded the maximum allowed.
            if (IsBeyondExtraRound(scoreCard.Length))
            {
                return false;
            }

            // Validates the extra round
            if (scoreCard.Length == BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS + BowlingGameExtenstions.EXTRA_ROUNDS)
            {
                int currentFrameIndex = scoreCard.Length - 1;
                // get the frame's type of the previous round.
                FrameTypeEnum previousFrameType = GetFrameType(scoreCard, currentFrameIndex - 1);

                // if the last round is Spare, then having a second try in the extra round is an invalid move.
                if(previousFrameType == FrameTypeEnum.Spare &&
                    scoreCard.GetFrame(currentFrameIndex).Try2 > 0)
                {
                    return false;
                }
            }

            // validate rest of the frames 
            return AreFramesValid(scoreCard, scoreCard.Length - 1);
        }

        /// <summary>
        /// Validating a set of frames. 
        /// The validation is based on recursion, to adhere the Functional Programming principles.
        /// </summary>
        private static bool AreFramesValid(ScoreCard scoreCards, int length)
        {
            // if the scorecard is empty - it is valid.
            if (length <= 0)
                return true;

            // return the validity status of the current frames and the rest of the following frames.
            return (scoreCards.GetFrame(length)?.IsValid()??false) && 
                AreFramesValid(scoreCards, length - 1);
        }
        
        #region Score calculation methods
        /// <summary>
        /// Returns the current score of a given scorecard
        /// </summary>
        public static int GetScore(ScoreCard scoreCard)
        {
            // Receives an array of all frames' scores and summarize it
            return scoreCard.GetFramesScores().Sum().Value;
        }

        /// <summary>
        /// Returns a score of an individual frame. If the given index or frame doesn't exist, the return value is null.
        /// </summary>
        public static int? GetFrameScore(ScoreCard scoreCard, int index)
        {
            return scoreCard.GetFrameScore(index);
        }
        #endregion

        #region Get game status methods
        private static bool IsBeyondExtraRound(int length)
        {
            return length > BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS + 
                BowlingGameExtenstions.EXTRA_ROUNDS;
        }

        private static bool IsIncludeExtraRound(int length)
        {
            return length >= BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS + 
                BowlingGameExtenstions.EXTRA_ROUNDS;
        }

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
