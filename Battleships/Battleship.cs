using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    public class Battleship
    {
        public int[,] grid = new int[10, 10];

        public enum ShipPosition
        {
            Vertical = 1,
            Horizontal
        }

        public void Start()
        {
            GenerateShips();

        }

        public void Fight(string coordinates, out string result)
        {
            var column = coordinates.First();
            var positionColumn = (int)column % 32;
            Int32.TryParse(coordinates.Last().ToString(), out int positionRow);
            result = ShotTarget(positionColumn - 1, positionRow - 1);
        }

        public bool ValidateCoordinates(string coordinates)
        {
            if (string.IsNullOrEmpty(coordinates) || coordinates.Length > 2)
            {
                return false;
            }

            var column = coordinates.First();
            var positionColumn = (int)column % 32;
            if(positionColumn > 10)
            {
                return false;
            }

            if (!Int32.TryParse(coordinates.Last().ToString(), out int positionRow))
            {
                return false;
            }

            return true;
        }

        public bool AllHaveSunk()
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private string ShotTarget(int column, int row)
        {
            int typeOfShip = 0;
            if (grid[row, column] != 0)
            {
                typeOfShip = grid[row, column];
                grid[row, column] = 0;

                if (IsTheLastShot(typeOfShip))
                {
                    return "Sinks";
                }
                else
                {
                    return "Hits";
                }

                //for (int i = 0; i < grid.GetLength(0); i++)
                //{
                //    for (int j = 0; j < grid.GetLength(1); j++)
                //    {
                //        if(grid[i,j] == typeOfShip)
                //        {
                //            grid[i, j] = 0;
                //        }
                //    }
                //}
            }
            else
            {
                return "Misses";
            }
        }

        private bool IsTheLastShot(int typeOfShip)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == typeOfShip)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void GenerateShips()
        {
            var random = new Random();
            var positionRandom = random.Next(1, 3);
            GenerateBattleship(positionRandom);
            GenerateDestroyer(random.Next(1, 3), 2);
            GenerateDestroyer(random.Next(1, 3), 3);

            DisplayShips(); // just to see them 
        }

        private void GenerateBattleship(int positionType)
        {
            var random = new Random();
            int positionHeadColumn = 0;
            int positionHeadRow = 0;

            switch (positionType)
            {
                case (int)ShipPosition.Horizontal:
                    positionHeadColumn = random.Next(0, 6);
                    positionHeadRow = random.Next(0, 10);

                    for (int i = positionHeadColumn; i <= positionHeadColumn + 4; i++)
                    {
                        grid[i, positionHeadRow] = 1;
                    }
                    break;
                case (int)ShipPosition.Vertical:
                    positionHeadColumn = random.Next(0, 10);
                    positionHeadRow = random.Next(0, 6);

                    for (int i = positionHeadRow; i <= positionHeadRow + 4; i++)
                    {
                        grid[positionHeadColumn, i] = 1;
                    }
                    break;
            }
        }

        private void GenerateDestroyer(int positionType, int currentNumber)
        {
            var random = new Random();
            int positionHeadColumn = 0;
            int positionHeadRow = 0;

            switch (positionType)
            {
                case (int)ShipPosition.Horizontal:
                    do
                    {
                        positionHeadColumn = random.Next(0, 6);
                        positionHeadRow = random.Next(0, 10);
                    }
                    while (!CanCreateDestroyer(positionType, positionHeadColumn, positionHeadRow));

                    for (int i = positionHeadColumn; i <= positionHeadColumn + 3; i++)
                    {
                        grid[i, positionHeadRow] = currentNumber;
                    }
                    break;
                case (int)ShipPosition.Vertical:
                    do
                    {
                        positionHeadColumn = random.Next(0, 10);
                        positionHeadRow = random.Next(0, 6);
                    }
                    while (!CanCreateDestroyer(positionType, positionHeadColumn, positionHeadRow));

                    for (int i = positionHeadRow; i <= positionHeadRow + 3; i++)
                    {
                        grid[positionHeadColumn, i] = currentNumber;
                    }
                    break;
            }
        }

        private bool CanCreateDestroyer(int positionType, int headColumn, int headRow)
        {
            switch (positionType)
            {
                case (int)ShipPosition.Horizontal:
                    for (int i = headColumn; i <= headColumn + 3; i++)
                    {
                        if (grid[i, headRow] != 0)
                        {
                            return false;
                        }
                    }
                    break;
                case (int)ShipPosition.Vertical:
                    for (int i = headRow; i <= headRow + 3; i++)
                    {
                        if (grid[headColumn, i] != 0)
                        {
                            return false;
                        }
                    }
                    break;
            }

            return true;
        }

        private void DisplayShips()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
