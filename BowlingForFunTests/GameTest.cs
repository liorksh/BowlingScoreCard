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
        //[ExpectedException(typeof(IllegalBowlingActionException))]
        public void IllegalFrames_1_Test()
        {
            BowlingFrame[] frames = GameTriesTests.GenerateFrames(
               new Tuple<int, int>(Game.NUM_OF_PINS, Game.NUM_OF_PINS),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, 0),
               new Tuple<int, int>(Game.NUM_OF_PINS, Game.NUM_OF_PINS));

            Assert.IsFalse(Game.IsValid(frames));
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

            Assert.IsFalse(Game.IsValid(frames));
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

            Assert.IsFalse(Game.IsValid(frames));
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

            Assert.IsFalse(Game.IsValid(frames));
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

            Assert.IsFalse(Game.IsValid(frames));
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

            Assert.IsFalse(Game.IsValid(frames));
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

            Assert.IsTrue(Game.IsValid(frames));
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

            Assert.IsFalse(Game.IsEligibleForAnotherTry(frames, frames.Length));
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

            Assert.IsTrue(Game.IsEligibleForAnotherTry(frames, frames.Length));
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

            Assert.IsTrue(Game.IsEligibleForAnotherTry(frames, frames.Length));

        }


        [TestMethod]
        public void PlayGame_1_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);
            Assert.AreEqual(8,Game.GetScore(frames2));


            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, 2, 4);
            Assert.AreEqual(14, Game.GetScore(frames3));

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);
            Assert.AreEqual(20, Game.GetScore(frames4));

            Assert.IsTrue(Game.IsEligibleForAnotherTry(frames4, frames4.Length));
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGameExceedFrames_1_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);

            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, 2, 4);

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);

            BowlingFrame[] frames5 = Game.RollNewFrame(frames4, 2, 4);

            BowlingFrame[] frames6 = Game.RollNewFrame(frames5, 2, 4);

            BowlingFrame[] frames7 = Game.RollNewFrame(frames6, 2, 4);

            BowlingFrame[] frames8 = Game.RollNewFrame(frames7, 2, 4);

            BowlingFrame[] frames9 = Game.RollNewFrame(frames8, 2, 4);

            BowlingFrame[] frames10 = Game.RollNewFrame(frames9, 2, 4);

            BowlingFrame[] frames11 = Game.RollNewFrame(frames10, 2, 4);

            BowlingFrame[] frames12 = Game.RollNewFrame(frames11, 2, 4);

            BowlingFrame[] frames13 = Game.RollNewFrame(frames12, 2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidFrame_1_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);

            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, Game.NUM_OF_PINS, Game.NUM_OF_PINS); // invalid input

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);

            BowlingFrame[] frames5 = Game.RollNewFrame(frames4, 2, 4);

            BowlingFrame[] frames6 = Game.RollNewFrame(frames5, 2, 4);

        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidFrame_2_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);

            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, -2, 4); //invalid input

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);

            BowlingFrame[] frames5 = Game.RollNewFrame(frames4, 2, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidLastFrame_1_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);

            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, 2, 4);

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);

            BowlingFrame[] frames5 = Game.RollNewFrame(frames4, 2, 4);

            BowlingFrame[] frames6 = Game.RollNewFrame(frames5, 2, 4);

            BowlingFrame[] frames7 = Game.RollNewFrame(frames6, 2, 4);

            BowlingFrame[] frames8 = Game.RollNewFrame(frames7, 2, 4);

            BowlingFrame[] frames9 = Game.RollNewFrame(frames8, 2, 4);

            BowlingFrame[] frames10 = Game.RollNewFrame(frames9, 2, 4);

            BowlingFrame[] frames11 = Game.RollNewFrame(frames10, 2, 8); // Spare

            BowlingFrame[] frames12 = Game.RollNewFrame(frames11, 2, 4); // This frame is invalid


        }

        [TestMethod]
        [ExpectedException(typeof(IllegalBowlingActionException))]
        public void PlayGame_InvalidLastFrame_2_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);

            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, 2, 4);

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);

            BowlingFrame[] frames5 = Game.RollNewFrame(frames4, 2, 4);

            BowlingFrame[] frames6 = Game.RollNewFrame(frames5, 2, 4);

            BowlingFrame[] frames7 = Game.RollNewFrame(frames6, 2, 4);

            BowlingFrame[] frames8 = Game.RollNewFrame(frames7, 2, 4);

            BowlingFrame[] frames9 = Game.RollNewFrame(frames8, 2, 4);

            BowlingFrame[] frames10 = Game.RollNewFrame(frames9, 2, 4);

            BowlingFrame[] frames11 = Game.RollNewFrame(frames10, 0, Game.NUM_OF_PINS); // Spare

            BowlingFrame[] frames12 = Game.RollNewFrame(frames11, 2, 4); // This frame is invalid   
        }

        [TestMethod]
        public void PlayGame_ValidLastFrame_1_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);

            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, 2, 4);

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);

            BowlingFrame[] frames5 = Game.RollNewFrame(frames4, 2, 4);

            BowlingFrame[] frames6 = Game.RollNewFrame(frames5, 2, 4);

            BowlingFrame[] frames7 = Game.RollNewFrame(frames6, 2, 4);

            BowlingFrame[] frames8 = Game.RollNewFrame(frames7, 2, 4);

            BowlingFrame[] frames9 = Game.RollNewFrame(frames8, 2, 4);

            BowlingFrame[] frames10 = Game.RollNewFrame(frames9, 2, 4);

            BowlingFrame[] frames11 = Game.RollNewFrame(frames10, Game.NUM_OF_PINS, 0); // Strike

            BowlingFrame[] frames12 = Game.RollNewFrame(frames11, 2, 6); // This frame is valid

            int score = 8 + 8 * 6 + Game.NUM_OF_PINS + 2 + 6;

            Trace.WriteLine($"score is {score}");
            Assert.AreEqual(score, Game.GetScore(frames12));
        }

        [TestMethod]
        public void PlayGame_ValidLastFrame_2_Test()
        {
            BowlingFrame[] frames = new BowlingFrame[0];
            BowlingFrame[] frames2 = Game.RollNewFrame(frames, 3, 5);

            BowlingFrame[] frames3 = Game.RollNewFrame(frames2, 2, 4);

            BowlingFrame[] frames4 = Game.RollNewFrame(frames3, 2, 4);

            BowlingFrame[] frames5 = Game.RollNewFrame(frames4, 2, 4);

            BowlingFrame[] frames6 = Game.RollNewFrame(frames5, 2, 4);

            BowlingFrame[] frames7 = Game.RollNewFrame(frames6, 2, 4);

            BowlingFrame[] frames8 = Game.RollNewFrame(frames7, 2, 4);

            BowlingFrame[] frames9 = Game.RollNewFrame(frames8, 2, 4);

            BowlingFrame[] frames10 = Game.RollNewFrame(frames9, 2, 4);

            BowlingFrame[] frames11 = Game.RollNewFrame(frames10, 0, Game.NUM_OF_PINS); // Spare

            BowlingFrame[] frames12 = Game.RollNewFrame(frames11, 2,0); // This frame is valid

            int score = 8 + 8 * 6 + Game.NUM_OF_PINS + 2 ;

            Trace.WriteLine($"score is {score}");
            Assert.AreEqual(score, Game.GetScore(frames12));
        }
    }
}
