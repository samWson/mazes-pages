using MazeGenerator.Models;
using System;
using System.Collections.Generic;

namespace MazeGenerator.Algorithms
{
  public class BinaryTree
  {
    private Grid grid;

    public BinaryTree(Grid grid)
    {
      this.grid = grid;
    }

    public void Apply()
    {
      foreach (Cell cell in grid)
      {
        List<Cell> neighbors = new List<Cell>();

        if (cell.North != null)
        {
          neighbors.Add(cell.North);
        }

        if (cell.East != null)
        {
          neighbors.Add(cell.East);
        }

        if (neighbors.Count > 0)
        {
          Random random = new Random();
          int index = random.Next(0, neighbors.Count);

          Cell neighbor = neighbors[index];
          cell.LinkBidirectionally(neighbor);
        }
      }
    }
  }
}
