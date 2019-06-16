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
    public class TestCard
    {
        [Test]
        public void TestToString()
        {
            Card c = new Person("Lisa");
            Assert.AreEqual("Lisa", c.ToString());
            c = new Weapon("Rope");
            Assert.AreEqual("Rope", c.ToString());
            c = new Room("Kitchen");
            Assert.AreEqual("Kitchen", c.ToString());
        }
    }
}
