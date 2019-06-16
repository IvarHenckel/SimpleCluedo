using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{
    public class Program
    {
        private static Room[,] gameBoard = new Room[25, 25];
        private static List<Player> players = new List<Player>();
        private static Card crimeScene;
        private static Card killer;
        private static Card killersWeapon;
        static void Main(string[] args)
        {
            InitBoard();
            HandOutCards();
        }

        public static void InitBoard()
        {
            gameBoard[0, 0] = new Room("Arbetsrummet");
            gameBoard[11, 7] = new Room("Hallen");
            gameBoard[18, 6] = new Room("Lounge?");
            gameBoard[3, 11] = new Room("Biblioteket");
            gameBoard[5, 16] = new Room("Biljardrummet");
            gameBoard[5, 20] = new Room("Vinterträdgården");
            gameBoard[9, 17] = new Room("Danssalongen");
            gameBoard[18, 10] = new Room("Lounge?");
            gameBoard[21, 18] = new Room("Lounge?");
        }

        public static void HandOutCards()
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
            killer = personCards.ElementAt(rnd.Next(personCards.Count()));
            killersWeapon = weaponCards.ElementAt(rnd.Next(weaponCards.Count()));

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

            players.Add(new Player(p1Cards));
            players.Add(new Player(p2Cards));
        }
    }
}
