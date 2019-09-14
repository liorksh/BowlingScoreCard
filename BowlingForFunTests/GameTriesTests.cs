using BowlingGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BowlingTests
{
    [TestClass]
    public class GameTriesTests
    {
        [TestInitialize]
        public void InitializeTest()
        {
        }

        [TestCleanup]
        public void CleanupTest()
        {
        }

        [TestMethod]
        public void GenerateEmptyScoreCards_Test()
        {
            Game game = new Game();

            ScoreCard newScoreCards = ScoreCard.GenerteEmptyScoreCards().
                Add(new BowlingFrame(5, 5));

            Assert.AreEqual(10, Game.GetScore(newScoreCards));
        }

        [TestMethod]
        public void GenerateScoreCards_Test()
        {
            Game game = new Game();

            ScoreCard emptyScoreCard = ScoreCard.GenerteEmptyScoreCards();

            Assert.AreEqual(emptyScoreCard.Frames.Length, 0);
        }

        [TestMethod]
        public void CalculateScore_EmptyBaordGame_Test()
        {
            Game game = new Game();

            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();

            int score = Game.GetScore(scoreCard);
            Assert.AreEqual(score, 0);
        }

        [TestMethod]
        public void CalculateScore_OneFrame_Test()
        {
            Game game = new Game();

            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard newScoreCards = scoreCard.Add(new BowlingFrame(5, 5));

            int score = Game.GetScore(newScoreCards);
            Assert.AreEqual(10, score);
        }

        [TestMethod]
        public void CalculateScore1_Test()
        {
            Game game = new Game();

            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards();
            ScoreCard newScoreCards = scoreCard.Add(new BowlingFrame(4, 5));

            int score = Game.GetScore(newScoreCards);

            Assert.AreEqual(9, score);
        }

        [TestMethod]
        public void PlayFramesNoSpareNorStrikeTest()
        {
            BowlingFrame[] frames = GenerateFrames(
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 4));

            ScoreCard scoreCard = ScoreCard.GenerteEmptyScoreCards().AddRange(frames);

            int score = 36;

            Assert.AreEqual(score, Game.GetScore(scoreCard));;
        }

        [TestMethod]
        public void PlayFrames_OneSpareTest()
        {
            int firstTryAfterSpare = 5;
            int firstTry = 3;
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
            new Tuple<int, int>(firstTry, Game.NUM_OF_PINS - firstTry),
            new Tuple<int, int>(firstTryAfterSpare, Game.NUM_OF_PINS - firstTryAfterSpare),
            new Tuple<int, int>(6,2));

            score = Game.NUM_OF_PINS + firstTryAfterSpare+
                Game.NUM_OF_PINS+6+
                6+2;

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_TwoSpareTest()
        {
            int firstTryAfterSpare = 5;
            int firstTry = 3;
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
                new Tuple<int, int>(firstTry, Game.NUM_OF_PINS - firstTry),
                new Tuple<int, int>(firstTry, Game.NUM_OF_PINS - firstTry),
                new Tuple<int, int>(firstTryAfterSpare, Game.NUM_OF_PINS - firstTryAfterSpare));

            score = Game.NUM_OF_PINS +firstTry +
                Game.NUM_OF_PINS + firstTryAfterSpare +
                firstTryAfterSpare + Game.NUM_OF_PINS - firstTryAfterSpare;

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_OneSpare_2_Test()
        {
            int score = 0;
            int firstTry = 8;
            BowlingFrame[] frames = GenerateFrames(
                new Tuple<int, int>(0, Game.NUM_OF_PINS),
                new Tuple<int, int>(firstTry, Game.NUM_OF_PINS - firstTry - 1),
                new Tuple<int, int>(3, 4));

            score = Game.NUM_OF_PINS + firstTry +
                firstTry + (Game.NUM_OF_PINS - firstTry - 1) +
                3 +4;

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_TwoConsecutiveSpares_Test()
        {
            int score = 0;
            int firstTry = 8;
            BowlingFrame[] frames = GenerateFrames(
                new Tuple<int, int>(0, Game.NUM_OF_PINS),
                new Tuple<int, int>(firstTry, Game.NUM_OF_PINS - firstTry),
                new Tuple<int, int>(3, 4));

            score = Game.NUM_OF_PINS + firstTry +
              Game.NUM_OF_PINS + 3 +
              3 + 4;

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }


        [TestMethod]
        public void PlayFrames_StrikeAndSpare_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
              new Tuple<int, int>(Game.NUM_OF_PINS,0),
              new Tuple<int, int>(0, Game.NUM_OF_PINS),
              new Tuple<int, int>(3, 4));

            score = Game.NUM_OF_PINS + 0 + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + 3+
                3 + 4;

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_Strike_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(1,5),
             new Tuple<int, int>(3, 4));
            
            score = Game.NUM_OF_PINS + 1+5+
                1+5+
                3+4;


            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_TwoConsecutiveStrikes_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(3, 5),
             new Tuple<int, int>(3, 4));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + 3+
                Game.NUM_OF_PINS + 3+ 5 +
                3 + 5 +
                3 + 4;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }
        
        [TestMethod]
        public void PlayFrames_ThreeConsecutiveStrikes_1_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(3, 5),
             new Tuple<int, int>(3, 4));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS+
                Game.NUM_OF_PINS+ Game.NUM_OF_PINS+3+
                Game.NUM_OF_PINS + 3 +5 +
                3 +5 +
                3+ 4;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        
        [TestMethod]
        public void PlayFrames_ThreeConsecutiveStrikes__2_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(5,0),
             new Tuple<int, int>(5, 0));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + 5 +
                Game.NUM_OF_PINS + 5 + 0 +
                5 +
                5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }


        [TestMethod]
        public void PlayFrames_ThreeConsecutiveStrikes_3_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(0, 5),
             new Tuple<int, int>(5, 0));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + 0 +
                Game.NUM_OF_PINS + 0 + 5 +
                5 +
                5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }
        
        [TestMethod]
        public void PlayFrames_StrikeAfterSpare_Test()
        {
            int score = 0;
            int tryDroppedPins = 4;

            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(0, 5),
             new Tuple<int, int>(tryDroppedPins, Game.NUM_OF_PINS - tryDroppedPins),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(5, 4),
             new Tuple<int, int>(4, 5));

            score = 5 +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                4 + 5;

            Trace.WriteLine($"score is {score}");
            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_SpareAfterSpare_Test()
        {
            int score = 0;
            int tryDroppedPins = 4;

            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(0, 5),
             new Tuple<int, int>(tryDroppedPins, Game.NUM_OF_PINS - tryDroppedPins),
             new Tuple<int, int>(0, Game.NUM_OF_PINS),
             new Tuple<int, int>(5, 4),
             new Tuple<int, int>(4, 5));

            score = 5 +
                Game.NUM_OF_PINS + 0 +
                Game.NUM_OF_PINS + 5 +
                5 + 4 +
                4 + 5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        
        [TestMethod]
        public void PlayFrames_FourConsecutiveStrikes_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0), 
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(Game.NUM_OF_PINS, 0),
             new Tuple<int, int>(5, 4),
             new Tuple<int, int>(4, 5));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + 5 + 
                Game.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                4 + 5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }
        
        [TestMethod]
        public void PlayFrames_SixConsecutiveStrikes_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 4),
               new Tuple<int, int>(4, 5));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + 5 +
                Game.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                4 + 5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_LastRoundSpare_1_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 4),
               new Tuple<int, int>(5, 5),
               new Tuple<int, int>(5, 0));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + 5 +
                Game.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                5 + 5 + 5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_LastRoundStrike_1_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 4),
               new Tuple<int, int>(Game.NUM_OF_PINS,0),
               new Tuple<int, int>(5, 3));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + 5 +
                Game.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                Game.NUM_OF_PINS + 5+3;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_LastRoundStrike_2_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 3));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + 5 +
                Game.NUM_OF_PINS + 5 + 3;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_FullStrikes()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, Game.NUM_OF_PINS));

            score = Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS +
                Game.NUM_OF_PINS + Game.NUM_OF_PINS + Game.NUM_OF_PINS;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        public static BowlingFrame[] GenerateFrames(params Tuple<int, int>[] tuples)
        {
            BowlingFrame[] frames = new BowlingFrame[tuples.Length];
            int counter = 0;
            foreach (Tuple<int, int> t in tuples)
            {
                frames[counter++] = new BowlingFrame(t.Item1, t.Item2);
            }

            return frames;
        }
    }
}
