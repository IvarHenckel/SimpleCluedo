using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{
    public abstract class Card
    {
        protected string name;
        protected Card(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
