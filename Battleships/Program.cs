using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            Battleship battle = new Battleship();
            battle.Start();

            bool won = false;
            do
            {
                Console.Write("Enter coordinate: ");
                var key = Console.ReadLine();
                if (battle.ValidateCoordinates(key))
                {
                    battle.Fight(key.ToString(), out string result);
                    if (battle.AllHaveSunk())
                    {
                        won = true;
                        Console.WriteLine("You won. All ships are sunk.");
                    }
                    else
                    {
                        Console.WriteLine(result);
                    }
                }
                else
                {
                    Console.WriteLine("You're coordinates are not ok. Please try again");
                }
            }
            while (won == false);

            Console.ReadKey();
        }
    }
}
