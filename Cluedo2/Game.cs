using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{

    public class Game
    {
        private int index = 0;
        private Player currentPlayer;
        private bool quit = false;
        private Card[,] gameBoard = new Card[25, 25];
        private List<Player> players = new List<Player>();
        private Card crimeScene;
        private Card killer;
        private Card killersWeapon;

        public Game()//man borde kunnaange antalet spelare
        {
        }

        public bool Quit(){
            return quit;
        }

        public Player GetCurrentPlayer()
        {
            return currentPlayer;
        }

        public void NextPlayer()
        {
            index = (index + 1) % players.Count();
            currentPlayer = players[index];
        }

        public bool MoveCurrentPlayer(int steps, int x, int y)
        {
            return currentPlayer.SetPos(steps, x, y);
        }

        public int RollDie()
        {
            Random rnd = new Random();
            return (rnd.Next(6) + 1) + (rnd.Next(6) + 1);
        }

        public void InitBoard()
        {
            gameBoard[0, 0] = new Room("Arbetsrummet");
            gameBoard[7, 11] = new Room("Hallen");
            gameBoard[6, 18] = new Room("Lounge?");
            gameBoard[11, 3] = new Room("Biblioteket");
            gameBoard[16, 5] = new Room("Biljardrummet");
            gameBoard[20, 5] = new Room("Vinterträdgården");
            gameBoard[17, 9] = new Room("Danssalongen");
            gameBoard[10, 18] = new Room("Lounge?");
            gameBoard[18, 21] = new Room("Lounge?");
        }

        public void HandOutCards()
        {
            List<Card> personCards = new List<Card>();
            personCards.Add(new Person("Fru vit"));
            personCards.Add(new Person("Professor Plommon"));
            personCards.Add(new Person("Pastor Grön"));
            personCards.Add(new Person("Överste Senap"));
            personCards.Add(new Person("Fröken Scharlakan"));
            personCards.Add(new Person("Fru Kråm"));

            List<Card> weaponCards = new List<Card>();
            weaponCards.Add(new Weapon("Repet"));
            weaponCards.Add(new Weapon("Rörtången"));
            weaponCards.Add(new Weapon("Ljusstaken"));
            weaponCards.Add(new Weapon("Dolken"));
            weaponCards.Add(new Weapon("Revolven"));
            weaponCards.Add(new Weapon("Blyröret"));

            List<Card> roomCards = new List<Card>();
            roomCards.Add(new Room("Köket"));
            roomCards.Add(new Room("Hallen"));
            roomCards.Add(new Room("Biljardrummet"));
            roomCards.Add(new Room("Biblioteket"));
            roomCards.Add(new Room("Vinterträdgården"));
            roomCards.Add(new Room("Arbetsrummet"));
            roomCards.Add(new Room("Matsalen"));
            roomCards.Add(new Room("Danssalongen"));
            roomCards.Add(new Room("Lounge?"));

            Random rnd = new Random();
            crimeScene = roomCards.ElementAt(rnd.Next(roomCards.Count()));
            roomCards.Remove(crimeScene);
            killer = personCards.ElementAt(rnd.Next(personCards.Count()));
            personCards.Remove(killer);
            killersWeapon = weaponCards.ElementAt(rnd.Next(weaponCards.Count()));
            weaponCards.Remove(killersWeapon);

            List<Card> cards = new List<Card>(roomCards);
            cards.AddRange(weaponCards);
            cards.AddRange(personCards);

            List<Card> p1Cards = new List<Card>(); //We start with only two players
            List<Card> p2Cards = new List<Card>();
            while (cards.Any())
            {
                int rndIndex = rnd.Next(cards.Count);
                p1Cards.Add(cards[rndIndex]);
                cards.RemoveAt(rndIndex);
                if (cards.Any())
                {
                    rndIndex = rnd.Next(cards.Count);
                    p2Cards.Add(cards[rndIndex]);
                    cards.RemoveAt(rndIndex);
                }
            }

            players.Add(new Player(p1Cards, new Person("Fru Vit"), 14, 24, 1));
            players.Add(new Player(p2Cards, new Person("Överste Senap"), 24, 7, 2));
            currentPlayer = players[0];
        }

        public string BoardToString() // Borde lägga till vid sidan rad och kulomn så ma  slipper räkna
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("    0  1  2  3  4  5  6  7  8  9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24");
            sb.AppendLine();
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                sb.Append(i);
                sb.Append(" ");
                if (i < 10)
                {
                    sb.Append(" ");
                }
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == null)
                    {
                        for (int p = 0; p < players.Count(); p++)
                        {
                            if (players[p].getX() == j && players[p].getY() == i)
                            {
                                sb.Append("|P|"); //Om jag tänkt rätt nu så kommer två spleare på samma ruta bara stå som |P|
                                break;
                            }
                            else if (p == players.Count - 1)
                            {
                                sb.Append("|_|");
                            }
                        }
                    }
                    else if (gameBoard[i, j].GetType() == typeof(Room))
                    {
                        sb.Append("|R|");
                    }
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        public bool CanMakeGuess()
        {
            Card currentRoom = gameBoard[currentPlayer.getY(), currentPlayer.getX()];
            return (currentRoom != null);
        }

        public List<Card> MakeGuess(string killer, string weapon)
        {
            Card crimeScene = gameBoard[currentPlayer.getY(), currentPlayer.getX()];
            List<Card> ret = new List<Card>();
            foreach(Player p in players )
            {
                if (!p.Equals(currentPlayer))// än så länge får man inte välja vilket kort man skall skicka
                {
                    List<Card> guess = new List<Card>();
                    guess.Add(new Person("Killer"));
                    guess.Add(crimeScene);
                    guess.Add(new Weapon("weapon"));
                    p.PickCard(ret, guess);
                }
            }
            return ret;
        }

        public bool MakeFinalGuess(string killer, string crimeScene, string weapon)
        {
            if ((new Person(killer)).Equals(this.killer) && (new Room(crimeScene)).Equals(this.crimeScene) && (new Weapon(weapon)).Equals(this.killersWeapon))
            {
                quit = true;
                return true;
            }
            return false; // Om gissningen är fel borde jag ju även egentligen ta bort spelaren

        }
    }
}
