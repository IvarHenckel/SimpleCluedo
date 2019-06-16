using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{
    public class Program
    {
        private static Card[,] gameBoard = new Card[25, 25];
        private static List<Player> players = new List<Player>();
        private static Card crimeScene;
        private static Card killer;
        private static Card killersWeapon;
        static void Main(string[] args)
        {
            InitBoard();
            HandOutCards();
            Console.WriteLine("Välkommen till Cluedo!");
            bool quit = false;
            int currentPlayer = 0;
            while (!quit) {
                Console.WriteLine("Spelare " + currentPlayer + ":s tur. Vad vill du göra?" + "\n" + 
                                   "1. Gissa mördare." + "\n" + 
                                   "2. Slå tärning");
                string ans = Console.ReadLine();
                if (ans == "1")
                {
                    // makeGuess();
                }
                else
                {
                    printBoard();
                    bool moved = false;
                    while (!moved)
                    {
                        int steps = rollDie();
                        Console.WriteLine("Du slog " + steps + ". Vart vill du gå?" + "\n" + "x-koordinat?");
                        int x = int.Parse(Console.ReadLine());
                        Console.WriteLine("y - koordinat ? ");
                        int y = int.Parse(Console.ReadLine());
                        moved = players[currentPlayer].SetPos(steps, x, y);
                    }
                    currentPlayer = (currentPlayer + 1) % players.Count();
                }
              
            }
        }

        public static void InitBoard()
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

            players.Add(new Player(p1Cards, new Person("Fru Vit"), 14, 24));
            players.Add(new Player(p2Cards, new Person("Överste Senap"), 24, 7));
            
        }

        private static int rollDie()
        {
            Random rnd = new Random();
            return (rnd.Next(6) + 1) + (rnd.Next(6) + 1);
        }

        private static void printBoard()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
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
            Console.WriteLine(sb.ToString());
        }
    }
}
