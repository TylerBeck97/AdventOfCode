using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var seafloor = new List<(int, int)>();
                var lines = System.IO.File.ReadAllLines("input.txt");

                var index = 0;
                var numOfTilesInRow = 0;
                   
                foreach (var line in lines)
                {
                    numOfTilesInRow = line.Count();
                    foreach (var number in line)
                    {
                        seafloor.Add((int.Parse(number.ToString()), index));
                        index++;
                    }
                }
                var lowTilesScore = seafloor.Where(tile =>
                {
                    var minOfRow = (tile.Item2 / numOfTilesInRow) * numOfTilesInRow;
                    var maxOfRow = (tile.Item2 / numOfTilesInRow) * numOfTilesInRow + numOfTilesInRow - 1;

                    //check if the adjacent tile exists and if it doesnt give an arbitary value higher than any possible value
                    var right = tile.Item2 - 1 < minOfRow ? 99 : seafloor.ElementAt(tile.Item2 - 1).Item1;
                    var left = tile.Item2 + 1 > maxOfRow ? 99 : seafloor.ElementAt(tile.Item2 + 1).Item1;
                    var above = tile.Item2 - numOfTilesInRow < 0 ? 99 : seafloor.ElementAt(tile.Item2 - numOfTilesInRow).Item1;
                    var below = tile.Item2 + numOfTilesInRow > seafloor.Count - 1 ? 99 : seafloor.ElementAt(tile.Item2 + numOfTilesInRow).Item1;

                    if (right <= tile.Item1 )
                        return false;
                    if (left <= tile.Item1)
                        return false;
                    if (above <= tile.Item1)
                        return false;
                    if (below <= tile.Item1)
                        return false;

                    System.Console.WriteLine(tile);
                    return true;
                }).Aggregate(0, (total, next) => total += ++next.Item1);

                System.Console.WriteLine(lowTilesScore);

            }

            catch (FormatException e)
            {
                System.Console.WriteLine(e.Message);
            }

            catch (FileNotFoundException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
