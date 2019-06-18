using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluedo2
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            Game game = new Game();
            game.InitBoard();
            game.HandOutCards();
            Console.WriteLine("Välkommen till Cluedo!");
            while (!game.Quit()) {
                Console.WriteLine("Spelare " + game.GetCurrentPlayer() + ":s tur. Vad vill du göra?" + "\n" + 
                                   "1. Gissa mördare." + "\n" + 
                                   "2. Slå tärning");
                string ans = Console.ReadLine();
                if (ans == "1")
                {
                    Console.WriteLine("Vem är mördaren?");
                    string killer = Console.ReadLine();
                    Console.WriteLine("Vart skedde mordet?");
                    string crimeScene = Console.ReadLine();
                    Console.WriteLine("Vad är mordvapnet?");
                    string killersWeapon = Console.ReadLine();
                    if (game.MakeFinalGuess(killer, crimeScene, killersWeapon))
                    {
                        Console.WriteLine(game.GetCurrentPlayer() + "vann spelet!");
                    }
                    else
                    {
                        Console.WriteLine(game.GetCurrentPlayer() + "förlorade spelet!");
                        game.NextPlayer();
                    }
                }
                else
                {
                    Console.WriteLine(game.BoardToString());
                    bool moved = false;
                    while (!moved)
                    {
                        int steps = game.RollDie();
                        Console.WriteLine("Du slog " + steps + ". Vart vill du gå?" + "\n" + "x-koordinat?");
                        int x = int.Parse(Console.ReadLine());
                        Console.WriteLine("y - koordinat ? ");
                        int y = int.Parse(Console.ReadLine());
                        moved = game.MoveCurrentPlayer(steps, x, y);
                    }
                    if (game.CanMakeGuess())
                    {
                        Console.WriteLine("Detta är dina kort:");
                        Console.WriteLine(game.GetCurrentPlayer().HandString());
                        Console.WriteLine("Vem är mördaren?");
                        string killer = Console.ReadLine();
                        Console.WriteLine("Vad är mordvapnet?");
                        string killersWeapon = Console.ReadLine();
                        Console.WriteLine("Dessa kort visade dina motståndare");
                        foreach (Card c in game.MakeGuess(killer, killersWeapon))
                        {
                            Console.WriteLine(c);
                        }
                    }
                    game.NextPlayer();
                }
            }
        }
    }
}
