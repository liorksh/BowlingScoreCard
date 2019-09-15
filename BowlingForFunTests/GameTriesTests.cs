using BowlingGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public void GenerateScoreCards_Test()
        {
            Game game = new Game();

            ScoreCard emptyScoreCard = Game.GenerteEmptyScoreCards();

            Assert.AreEqual(emptyScoreCard.Length, 0);
        }

        [TestMethod]
        public void CalculateScore_EmptyBaordGame_Test()
        {
            Game game = new Game();

            ScoreCard scoreCard = Game.GenerteEmptyScoreCards();

            int score = Game.GetScore(scoreCard);
            Assert.AreEqual(score, 0);
        }
        
        [TestMethod]
        public void CalculateScore_OneFrame_2_Test()
        {
            Game game = new Game();

            ScoreCard scoreCards = Game.GenerteEmptyScoreCards().Add(new BowlingFrame(5, 5));

            int?[] scores = scoreCards.GetFramesScores();
            Assert.AreEqual(1, scores.Length);

            Assert.IsNull(scores[0]);
        }
             

        [TestMethod]
        public void CalculateScore_1_Test()
        {
            Game game = new Game();

            ScoreCard scoreCards = Game.GenerteEmptyScoreCards().Add(new BowlingFrame(4, 5));

            int score = Game.GetScore(scoreCards);

            Assert.AreEqual(9, score);
        }

        [TestMethod]
        public void CalculateScore_2_Test()
        {
            Game game = new Game();

            ScoreCard scoreCards = Game.GenerteEmptyScoreCards().Add(new BowlingFrame(4, 5));
            
            int?[] scores = scoreCards.GetFramesScores();
            Assert.AreEqual(1, scores.Length);
            
            Assert.AreEqual(9, scores[0]);
        }

        [TestMethod]
        public void PlayFramesNoSpareNorStrikeTest()
        {
            BowlingFrame[] frames = GenerateFrames(
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 4),
            new Tuple<int, int>(5, 4));

            ScoreCard scoreCard = Game.GenerteEmptyScoreCards().AddRange(frames);

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
            new Tuple<int, int>(firstTry, BowlingGameExtenstions.NUM_OF_PINS - firstTry),
            new Tuple<int, int>(firstTryAfterSpare, BowlingGameExtenstions.NUM_OF_PINS - firstTryAfterSpare),
            new Tuple<int, int>(6,2));

            score = BowlingGameExtenstions.NUM_OF_PINS + firstTryAfterSpare+
                BowlingGameExtenstions.NUM_OF_PINS+6+
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
                new Tuple<int, int>(firstTry, BowlingGameExtenstions.NUM_OF_PINS - firstTry),
                new Tuple<int, int>(firstTry, BowlingGameExtenstions.NUM_OF_PINS - firstTry),
                new Tuple<int, int>(firstTryAfterSpare, BowlingGameExtenstions.NUM_OF_PINS - firstTryAfterSpare));

            score = BowlingGameExtenstions.NUM_OF_PINS + firstTry +
                BowlingGameExtenstions.NUM_OF_PINS + firstTryAfterSpare;
                //firstTryAfterSpare + BowlingGameExtenstions.NUM_OF_PINS - firstTryAfterSpare;

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
            Assert.AreEqual((BowlingGameExtenstions.NUM_OF_PINS + firstTry), Game.GetFrameScore(scoreCard,0));
            Assert.AreEqual((BowlingGameExtenstions.NUM_OF_PINS + firstTryAfterSpare), Game.GetFrameScore(scoreCard, 1));

            Assert.IsNull(Game.GetFrameScore(scoreCard,2));
        }

        [TestMethod]
        public void PlayFrames_OneSpare_2_Test()
        {
            int score = 0;
            int firstTry = 8;
            BowlingFrame[] frames = GenerateFrames(
                new Tuple<int, int>(0, BowlingGameExtenstions.NUM_OF_PINS),
                new Tuple<int, int>(firstTry, BowlingGameExtenstions.NUM_OF_PINS - firstTry - 1),
                new Tuple<int, int>(3, 4));

            score = BowlingGameExtenstions.NUM_OF_PINS + firstTry +
                firstTry + (BowlingGameExtenstions.NUM_OF_PINS - firstTry - 1) +
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
                new Tuple<int, int>(0, BowlingGameExtenstions.NUM_OF_PINS),
                new Tuple<int, int>(firstTry, BowlingGameExtenstions.NUM_OF_PINS - firstTry),
                new Tuple<int, int>(3, 4));

            score = BowlingGameExtenstions.NUM_OF_PINS + firstTry +
              BowlingGameExtenstions.NUM_OF_PINS + 3 +
              3 + 4;

            ScoreCard scoreCard = new ScoreCard(frames);
                        
            Assert.AreEqual(score, scoreCard.GetFramesScores().Sum());
            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }


        [TestMethod]
        public void PlayFrames_StrikeAndSpare_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
              new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS,0),
              new Tuple<int, int>(0, BowlingGameExtenstions.NUM_OF_PINS),
              new Tuple<int, int>(3, 4));

            score = BowlingGameExtenstions.NUM_OF_PINS + 0 + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + 3+
                3 + 4;

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_Strike_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(1,5),
             new Tuple<int, int>(3, 4));
            
            score = BowlingGameExtenstions.NUM_OF_PINS + 1+5+
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
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(3, 5),
             new Tuple<int, int>(3, 4));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 3+
                BowlingGameExtenstions.NUM_OF_PINS + 3+ 5 +
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
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(3, 5),
             new Tuple<int, int>(3, 4));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS+
                BowlingGameExtenstions.NUM_OF_PINS+ BowlingGameExtenstions.NUM_OF_PINS+3+
                BowlingGameExtenstions.NUM_OF_PINS + 3 +5 +
                3 +5 +
                3+ 4;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        
        [TestMethod]
        public void PlayFrames_ThreeConsecutiveStrikes_2_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(5,0),
             new Tuple<int, int>(5, 0));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 5 +
                BowlingGameExtenstions.NUM_OF_PINS + 5 + 0 +
                5 +
                5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);
            Assert.AreEqual(score, Game.GetScore(scoreCard));

            Assert.AreEqual(30, Game.GetFrameScore(scoreCard, 0));
            Assert.AreEqual(25, Game.GetFrameScore(scoreCard, 1));

        }


        [TestMethod]
        public void PlayFrames_ThreeConsecutiveStrikes_3_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(0, 5),
             new Tuple<int, int>(5, 0));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 0 +
                BowlingGameExtenstions.NUM_OF_PINS + 0 + 5 +
                5 +
                5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }
        /*------------ Test frames calculation ----------------- */
        [TestMethod]
        public void PlayFrames_TestFrameScore_AfterStrikes_1_Test()
        {
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0));

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.IsNull(scoreCard.GetFrameScore(0));

            Assert.IsNull(scoreCard.GetFrameScore(1));
        }

        [TestMethod]
        public void PlayFrames_TestFrameScore_AfterStrikes_2_Test()
        {
            int score = 0;
            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(5, 3));

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);
            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 5 +
               BowlingGameExtenstions.NUM_OF_PINS +5 + 3 +
               5 + 3;

            Assert.AreEqual(10+10+5, scoreCard.GetFrameScore(0));
            Assert.AreEqual(10 + 5+3, scoreCard.GetFrameScore(1));
            Assert.AreEqual(score, scoreCard.GetFramesScores().Sum());
            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        /*------------ End: Test frames calculation ----------------- */

        [TestMethod]
        public void PlayFrames_StrikeAfterSpare_Test()
        {
            int score = 0;
            int tryDroppedPins = 4;

            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(0, 5),
             new Tuple<int, int>(tryDroppedPins, BowlingGameExtenstions.NUM_OF_PINS - tryDroppedPins),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(5, 4),
             new Tuple<int, int>(4, 5));

            score = 5 +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                4 + 5;

            Trace.WriteLine($"score is {score}");
            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
            Assert.AreEqual(20, Game.GetFrameScore(scoreCard,1));
            Assert.AreEqual(19, Game.GetFrameScore(scoreCard, 2));


        }

        [TestMethod]
        public void PlayFrames_SpareAfterSpare_Test()
        {
            int score = 0;
            int tryDroppedPins = 4;

            BowlingFrame[] frames = GenerateFrames(
             new Tuple<int, int>(0, 5),
             new Tuple<int, int>(tryDroppedPins, BowlingGameExtenstions.NUM_OF_PINS - tryDroppedPins),
             new Tuple<int, int>(0, BowlingGameExtenstions.NUM_OF_PINS),
             new Tuple<int, int>(5, 4),
             new Tuple<int, int>(4, 5));

            score = 5 +
                BowlingGameExtenstions.NUM_OF_PINS + 0 +
                BowlingGameExtenstions.NUM_OF_PINS + 5 +
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
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0), 
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
             new Tuple<int, int>(5, 4),
             new Tuple<int, int>(4, 5));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 5 + 
                BowlingGameExtenstions.NUM_OF_PINS + 5 + 4 +
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
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 4),
               new Tuple<int, int>(4, 5));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 5 +
                BowlingGameExtenstions.NUM_OF_PINS + 5 + 4 +
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
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 4),
               new Tuple<int, int>(5, 5),
               new Tuple<int, int>(5, 0));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 5 +
                BowlingGameExtenstions.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                5 + 5 + 5;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
            Assert.AreEqual(15, Game.GetFrameScore(scoreCard, BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS-1));

        }

        [TestMethod]
        public void PlayFrames_LastRoundStrike_1_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 4),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS,0),
               new Tuple<int, int>(5, 3));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 5 +
                BowlingGameExtenstions.NUM_OF_PINS + 5 + 4 +
                5 + 4 +
                BowlingGameExtenstions.NUM_OF_PINS + 5+3;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_LastRoundStrike_2_Test()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(5, 3));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + 5 +
                BowlingGameExtenstions.NUM_OF_PINS + 5 + 3;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
        }

        [TestMethod]
        public void PlayFrames_FullStrikes()
        {
            int score = 0;

            BowlingFrame[] frames = GenerateFrames(
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, 0),
               new Tuple<int, int>(BowlingGameExtenstions.NUM_OF_PINS, BowlingGameExtenstions.NUM_OF_PINS));

            score = BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS +
                BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS + BowlingGameExtenstions.NUM_OF_PINS;

            Trace.WriteLine($"score is {score}");

            ScoreCard scoreCard = new ScoreCard(frames);

            Assert.AreEqual(score, Game.GetScore(scoreCard));
            Assert.AreEqual(30, Game.GetFrameScore(scoreCard, BowlingGameExtenstions.NUM_OF_REGULAR_ROUNDS-1));

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
