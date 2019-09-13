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
        //[ExpectedException(typeof(IllegalBowlingActionException))]
        public void BowlingFrameState_SpareTest()
        {
            BowlingFrame frame = new BowlingFrame();
            int firstTry = 4;
            FrameTypeEnum frameType = BowlingFrame.GetFramType(new Tuple<int, int>(firstTry, Game.NUM_OF_PINS-firstTry));

            Assert.AreEqual(FrameTypeEnum.Spare, frameType);                        
         }

        [TestMethod]
        public void BowlingFrameState_NormalTest()
        {
            BowlingFrame frame = new BowlingFrame();

            FrameTypeEnum frameType = BowlingFrame.GetFramType(new Tuple<int, int>(3, 6));

            Assert.AreEqual(FrameTypeEnum.Normal, frameType);
        }

        [TestMethod]
        public void BowlingFrameState_Strike1_Test()
        {
            BowlingFrame frame = new BowlingFrame();

            FrameTypeEnum frameType = BowlingFrame.GetFramType(new Tuple<int, int>(Game.NUM_OF_PINS,0));

            Assert.AreEqual(FrameTypeEnum.Strike, frameType);
        }

        [TestMethod]
        public void BowlingFrameState_Spare2_Test()
        {
            BowlingFrame frame = new BowlingFrame();

            FrameTypeEnum frameType = BowlingFrame.GetFramType(new Tuple<int, int>(0, Game.NUM_OF_PINS));

            Assert.AreEqual(FrameTypeEnum.Spare, frameType);
        }
    }
}
