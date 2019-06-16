using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{
    class Player
    {
        private List<Card> cards = new List<Card>();
        public Player(List<Card> cards)
        {
            this.cards = cards;
        }
    }
}
