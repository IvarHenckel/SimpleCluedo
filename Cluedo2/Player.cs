using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{
    public class Player
    {
        private int number;
        private Person person;
        private int x;
        private int y;
        private List<Card> cards = new List<Card>();
        public Player(List<Card> cards, Person person, int x, int y, int number)
        {
            this.cards = cards;
            this.person = person;
            this.x = x;
            this.y = y;
            this.number = number;
        }

        public int GetNumber()
        {
            return number;
        }

        public override string ToString()
        {
            return "spelare " + number;
        }

        public string HandString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Card c in cards)
            {
                sb.Append(c);
                sb.AppendLine();
            }
            return sb.ToString();
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

        public void PickCard(List<Card> ret, List<Card> guess)
        {
            foreach (Card g in guess)
            {
                foreach (Card c in cards)
                {
                    if (g.Equals(c))
                    {
                        ret.Add(c);
                        return;
                    }
                }
            }
        }

        public int getX() { return x; }
        public int getY() { return y; }
    }
}
