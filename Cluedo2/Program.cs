﻿using System;
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
                Console.WriteLine("Spelare " + (game.GetCurrentPlayer() + 1) + ":s tur. Vad vill du göra?" + "\n" + 
                                   "1. Gissa mördare." + "\n" + 
                                   "2. Slå tärning");
                string ans = Console.ReadLine();
                if (ans == "1")
                {
                    // makeGuess();
                }
                else
                {
                    game.printBoard();
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
                    game.NextPlayer();
                }
              
            }
        }
    }
}
