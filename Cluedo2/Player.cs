using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{
    class Player
    {
        private Person person;
        private int x;
        private int y;
        private List<Card> cards = new List<Card>();
        public Player(List<Card> cards, Person person, int x, int y)
        {
            this.cards = cards;
            this.person = person;
            this.x = x;
            this.y = y;
        }

        public bool SetPos(int steps, int x, int y)
        {
            if (Math.Abs(this.x - x) + Math.Abs(this.y - y) <= steps)
            {
                this.x = x;
                this.y = y;
                return true;
            }
            return false;
            
        }

        public int getX() { return x; }
        public int getY() { return y; }
    }
}
