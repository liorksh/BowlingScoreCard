using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using BowlingGame;
using System.Diagnostics;

namespace BowlingTests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void IllegalFrames_1_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
               new Tuple<int, int>(Game.NUM_OF_PINS, Game.NUM_OF_PINS),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, Game.NUM_OF_PINS));

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsFalse(Game.IsValid(scoreCard));
        }

        [TestMethod]
        public void IllegalFrames_2_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(5, 7),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0));

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsFalse(Game.IsValid(scoreCard));
        }

        [TestMethod]
        public void IllegalFrames_3_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(5, 7),
             new Tuple<int, int>(-3, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0));

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsFalse(Game.IsValid(scoreCard));
        }


        [TestMethod]
        public void OverrunGameTest()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
              new Tuple<int, int>(Game.NUM_OF_PINS, 0),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(5, 7),
              new Tuple<int, int>(Game.NUM_OF_PINS, 0),
              new Tuple<int, int>(Game.NUM_OF_PINS, 0),
              new Tuple<int, int>(Game.NUM_OF_PINS, 0),
              new Tuple<int, int>(Game.NUM_OF_PINS, 0));

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsFalse(Game.IsValid(scoreCard));
        }

        [TestMethod]
        public void IllegealLastRound_Spare_1_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
              new Tuple<int, int>(Game.NUM_OF_PINS, 0),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 3),
              new Tuple<int, int>(5, 4),
              new Tuple<int, int>(5, 2),
              new Tuple<int, int>(5, 2),
              new Tuple<int, int>(5, 3),
              new Tuple<int, int>(3, 7), // spare
              new Tuple<int, int>(5, 3));

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsFalse(Game.IsValid(scoreCard));
        }

        [TestMethod]
        public void IllegealLastRound_Spare_2_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
              new Tuple<int, int>(Game.NUM_OF_PINS, 0),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 3),
              new Tuple<int, int>(5, 4),
              new Tuple<int, int>(5, 2),
              new Tuple<int, int>(5, 2),
              new Tuple<int, int>(5, 3),
              new Tuple<int, int>(0, Game.NUM_OF_PINS), // Spare
              new Tuple<int, int>(5, 3));

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsFalse(Game.IsValid(scoreCard));
        }

        [TestMethod]
        public void LegealLastRound_Strike_1_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
              new Tuple<int, int>(Game.NUM_OF_PINS, 0),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 5),
              new Tuple<int, int>(5, 3),
              new Tuple<int, int>(5, 4),
              new Tuple<int, int>(5, 2),
              new Tuple<int, int>(5, 2),
              new Tuple<int, int>(5, 3),
              new Tuple<int, int>(Game.NUM_OF_PINS,0), // Strike
              new Tuple<int, int>(5, 5));

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsTrue(Game.IsValid(scoreCard));
        }

        [TestMethod]
        public void CheckIfReachedToEndGame_1_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
            new Tuple<int, int>(Game.NUM_OF_PINS, 0),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(Game.NUM_OF_PINS, 0), // Strike
            new Tuple<int, int>(5, 5));

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.IsTrue(Game.IsEligibleForAnotherTry(scoreCard));
        }

        [TestMethod]
        public void CheckIfReachedToEndGame_2_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
            new Tuple<int, int>(Game.NUM_OF_PINS, 0),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(0, Game.NUM_OF_PINS)); // Spare

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.IsTrue(Game.IsEligibleForAnotherTry(scoreCard));
        }

        [TestMethod]
        public void CheckIfReachedToEndGame_3_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
            new Tuple<int, int>(Game.NUM_OF_PINS, 0),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(Game.NUM_OF_PINS,0)); // Strike

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.IsTrue(Game.IsEligibleForAnotherTry(scoreCard));
        }

        [TestMethod]
        public void CheckIfReachedToEndGame_4_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
            new Tuple<int, int>(Game.NUM_OF_PINS, 0),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 3), // regular round
            new Tuple<int, int>(5, 5));

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.IsFalse(Game.IsEligibleForAnotherTry(scoreCard));
        }

        [TestMethod]
        public void CheckIfReachedToEndGame_5_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
            new Tuple<int, int>(Game.NUM_OF_PINS, 0),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 3)); // regular round

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.IsFalse(Game.IsEligibleForAnotherTry(scoreCard));
        }

        [TestMethod]
        public void CheckIfReachedToEndGame_6_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
            new Tuple<int, int>(Game.NUM_OF_PINS, 0),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 5),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 2),
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(Game.NUM_OF_PINS, 0), // Strike
            new Tuple<int, int>(5, 3),
            new Tuple<int, int>(5, 3));

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.IsFalse(Game.IsEligibleForAnotherTry(scoreCard));

        }

        [TestMethod]
        public void PlayGame_1_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];

            ScoreCard scoreCard = new ScoreCard(frames);

            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);
            Assert.AreEqual(8,Game.GetScore(scoreCard2));


            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, 2, 4);
            Assert.AreEqual(14, Game.GetScore(scoreCard3));

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);
            Assert.AreEqual(20, Game.GetScore(scoreCard4));

            Assert.IsTrue(Game.IsEligibleForAnotherTry(scoreCard4));
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGameExceedFrames_1_Test()
        {
            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);

            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, 2, 4);

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);

            ScoreCard scoreCard5 = Game.RollNewFrame(scoreCard4, 2, 4);

            ScoreCard scoreCard6 = Game.RollNewFrame(scoreCard5, 2, 4);

            ScoreCard scoreCard7 = Game.RollNewFrame(scoreCard6, 2, 4);

            ScoreCard scoreCard8 = Game.RollNewFrame(scoreCard7, 2, 4);

            ScoreCard scoreCard9 = Game.RollNewFrame(scoreCard8, 2, 4);

            ScoreCard scoreCard10 = Game.RollNewFrame(scoreCard9, 2, 4);

            ScoreCard scoreCard11 = Game.RollNewFrame(scoreCard10, 2, 4);

            ScoreCard scoreCard12 = Game.RollNewFrame(scoreCard11, 2, 4);

            ScoreCard scoreCard13 = Game.RollNewFrame(scoreCard12, 2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidFrame_1_Test()
        {
            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);

            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, Game.NUM_OF_PINS, Game.NUM_OF_PINS); // invalid input

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);

            ScoreCard scoreCard5 = Game.RollNewFrame(scoreCard4, 2, 4);

            ScoreCard scoreCard6 = Game.RollNewFrame(scoreCard5, 2, 4);

        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidFrame_2_Test()
        {
            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);

            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, -2, 4); //invalid input

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);

            ScoreCard scoreCard5 = Game.RollNewFrame(scoreCard4, 2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidLastFrame_1_Test()
        {
            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);

            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, 2, 4);

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);

            ScoreCard scoreCard5 = Game.RollNewFrame(scoreCard4, 2, 4);

            ScoreCard scoreCard6 = Game.RollNewFrame(scoreCard5, 2, 4);

            ScoreCard scoreCard7 = Game.RollNewFrame(scoreCard6, 2, 4);

            ScoreCard scoreCard8 = Game.RollNewFrame(scoreCard7, 2, 4);

            ScoreCard scoreCard9 = Game.RollNewFrame(scoreCard8, 2, 4);

            ScoreCard scoreCard10 = Game.RollNewFrame(scoreCard9, 2, 4);

            ScoreCard scoreCard11 = Game.RollNewFrame(scoreCard10, 2, 8); // Spare

            ScoreCard scoreCard12 = Game.RollNewFrame(scoreCard11, 2, 4); // This frame is invalid


        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidLastFrame_2_Test()
        {
            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);

            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, 2, 4);

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);

            ScoreCard scoreCard5 = Game.RollNewFrame(scoreCard4, 2, 4);

            ScoreCard scoreCard6 = Game.RollNewFrame(scoreCard5, 2, 4);

            ScoreCard scoreCard7 = Game.RollNewFrame(scoreCard6, 2, 4);

            ScoreCard scoreCard8 = Game.RollNewFrame(scoreCard7, 2, 4);

            ScoreCard scoreCard9 = Game.RollNewFrame(scoreCard8, 2, 4);

            ScoreCard scoreCard10 = Game.RollNewFrame(scoreCard9, 2, 4);

            ScoreCard scoreCard11 = Game.RollNewFrame(scoreCard10, 0, Game.NUM_OF_PINS); // Spare

            ScoreCard scoreCard12 = Game.RollNewFrame(scoreCard11, 2, 4); // This frame is invalid   
        }

        [TestMethod]
        public void PlayGame_ValidLastFrame_1_Test()
        {
            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);

            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, 2, 4);

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);

            ScoreCard scoreCard5 = Game.RollNewFrame(scoreCard4, 2, 4);

            ScoreCard scoreCard6 = Game.RollNewFrame(scoreCard5, 2, 4);

            ScoreCard scoreCard7 = Game.RollNewFrame(scoreCard6, 2, 4);

            ScoreCard scoreCard8 = Game.RollNewFrame(scoreCard7, 2, 4);

            ScoreCard scoreCard9 = Game.RollNewFrame(scoreCard8, 2, 4);

            ScoreCard scoreCard10 = Game.RollNewFrame(scoreCard9, 2, 4);

            ScoreCard scoreCard11 = Game.RollNewFrame(scoreCard10, Game.NUM_OF_PINS, 0); // Strike

            ScoreCard scoreCard12 = Game.RollNewFrame(scoreCard11, 2, 6); // This frame is valid

            int score = 8 + 8 * 6 + Game.NUM_OF_PINS + 2 + 6;

            Trace.WriteLine($"score is {score}");
            Assert.AreEqual(score, Game.GetScore(scoreCard12));
        }

        [TestMethod]
        public void PlayGame_ValidLastFrame_2_Test()
        {
            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard scoreCard2 = Game.RollNewFrame(scoreCard, 3, 5);

            ScoreCard scoreCard3 = Game.RollNewFrame(scoreCard2, 2, 4);

            ScoreCard scoreCard4 = Game.RollNewFrame(scoreCard3, 2, 4);

            ScoreCard scoreCard5 = Game.RollNewFrame(scoreCard4, 2, 4);

            ScoreCard scoreCard6 = Game.RollNewFrame(scoreCard5, 2, 4);

            ScoreCard scoreCard7 = Game.RollNewFrame(scoreCard6, 2, 4);

            ScoreCard scoreCard8 = Game.RollNewFrame(scoreCard7, 2, 4);

            ScoreCard scoreCard9 = Game.RollNewFrame(scoreCard8, 2, 4);

            ScoreCard scoreCard10 = Game.RollNewFrame(scoreCard9, 2, 4);

            ScoreCard scoreCard11 = Game.RollNewFrame(scoreCard10, 0, Game.NUM_OF_PINS); // Spare

            ScoreCard scoreCard12 = Game.RollNewFrame(scoreCard11, 2,0); // This frame is valid

            int score = 8 + 8 * 6 + Game.NUM_OF_PINS + 2 ;

            Trace.WriteLine($"score is {score}");
            Assert.AreEqual(score, Game.GetScore(scoreCard12));
        }
    }
}
