using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly_Project;

namespace MonopolyUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckBoardCreator()
        {
            Gameboard board = new Gameboard();
            Assert.AreEqual(board.cells.length, 40, "problem initializing the cells");
        }

        [TestMethod]
        public void CheckPlayerFactory()
        {
            Player p1 = PlayerFactory.getNewPlayer();
            Player p2 = PlayerFactory.getNewPlayer();
        }
    }
}
