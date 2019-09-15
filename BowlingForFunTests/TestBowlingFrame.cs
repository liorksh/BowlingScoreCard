using BowlingGame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingTests
{
    [TestClass]
    public class TestBowlingFrame
    {
        [TestMethod]
        public void BowlingFrameState_SpareTest()
        {
            int firstTry = 4;
            BowlingFrame frame = new BowlingFrame(firstTry, BowlingGameExtenstions.NUM_OF_PINS - firstTry);

            Assert.AreEqual(FrameTypeEnum.Spare, frame.FrameType);                        
         }

        [TestMethod]
        public void BowlingFrameState_NormalTest()
        {
            BowlingFrame frame = new BowlingFrame(3,6);
            
            Assert.AreEqual(FrameTypeEnum.Normal, frame.FrameType);
        }

        [TestMethod]
        public void BowlingFrameState_Strike1_Test()
        {
            BowlingFrame frame = new BowlingFrame(BowlingGameExtenstions.NUM_OF_PINS, 0);
            
            Assert.AreEqual(FrameTypeEnum.Strike, frame.FrameType);
        }

        [TestMethod]
        public void BowlingFrameState_Spare2_Test()
        {
            BowlingFrame frame = new BowlingFrame(0, BowlingGameExtenstions.NUM_OF_PINS);
            
            Assert.AreEqual(FrameTypeEnum.Spare, frame.FrameType);
        }

        [TestMethod]
        public void BowlingFrame_IsValid_Test()
        {
            int firstTry = 4;
            BowlingFrame frame = new BowlingFrame(firstTry, BowlingGameExtenstions.NUM_OF_PINS - firstTry+2);

            Assert.IsFalse(frame.IsValid());
        }

    }
}
