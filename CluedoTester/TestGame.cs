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
            Assert.IsFalse(g.CanMakeGuess(), "Should not stand in a room on the first turn.");
            Assert.IsFalse(g.Quit(), "Game should not be quit at the beginning.");
            Assert.AreEqual(g.GetCurrentPlayer().GetNumber(), 1, "The first player should be player 1.");
        }

        [Test]
        public void TestNextPlayer()
        {
            g.NextPlayer();
            g.NextPlayer();
            Assert.AreEqual(g.GetCurrentPlayer().GetNumber(), 1, "Should start on player one again with 2 players");
            g.NextPlayer();
            Assert.AreEqual(g.GetCurrentPlayer().GetNumber(), 2, "Should now be player 2:s turn.");
        }

        [Test]
        public void TestMovePlayer()
        {
            string s1 = g.BoardToString();
            Assert.False(g.MoveCurrentPlayer(1, 0, 0), "Player one should not be able to move that far.");
            string s2 = g.BoardToString();
            Assert.AreEqual(s1, s2);
            Assert.True(g.MoveCurrentPlayer(10, 9, 19), "Player one should be able to move to position (9,19) with 10 steps.");
            string s3 = g.BoardToString();
            Assert.AreNotEqual(s1, s3);
        }

        [Test]
        public void TestMakeFinalGuess()
        {
            Assert.IsFalse(g.MakeFinalGuess("Kalle", "Frysrummet", ""), "This should be false since none of the Cards exist.");
        }

        [Test]
        public void TestCanGuess()
        {
            Assert.IsFalse(g.CanMakeGuess(), "Player one is not standning in a room at the start.");
        }

        [Test]
        public void TestMakeGuess()
        {
            g.GetCurrentPlayer().SetPos(100, 0, 0);
            Assert.IsEmpty(g.MakeGuess("",""), "Should be fine to guess, but no cards are empty so no cards will be shown.");
        }
    }
}
