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
            Gameboard.Init();
            Assert.AreEqual(Gameboard.Instance.Cells.Length, 40, "problem initializing the cells");
        }

        [TestMethod]
        public void CheckPlayerFactory()
        {
            Player p1 = Player.GetNewPlayer("Quentin");
            Player p2 = Player.GetNewPlayer("Brounounours");
        }
    }
}
