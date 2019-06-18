using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Cluedo2;

namespace CluedoTester
{
    [TestFixture]
    class TestGame
    {
        Game g;
        [SetUp]
        public void SetUp()
        {
            g = new Game();
            g.InitBoard();
            g.HandOutCards();
        }

        [Test]
        public void TestInit()
        {
            Assert.IsFalse(g.Quit(), "Game should not be quit at the beginning.");
            Assert.AreEqual(g.GetCurrentPlayer(), 1, "The first player should be player 1.");
        }

        [Test]
        public void TestNextPlayer()
        {
            g.NextPlayer();
            g.NextPlayer();
            Assert.AreEqual(g.GetCurrentPlayer(), 1, "Should start on player one again with 2 players");
            g.NextPlayer();
            Assert.AreEqual(g.GetCurrentPlayer(), 2, "Should now be player 2:s turn.");
        }

        [Test]
        public void TestMovePlayer()
        {
            Assert.False(g.MoveCurrentPlayer(1, 0, 0), "Player one should not be able to move that far.");
            Assert.True(g.MoveCurrentPlayer(10, 9, 19), "Player one should be able to move to position (9,19) with 10 steps.");
        }
    }
}
