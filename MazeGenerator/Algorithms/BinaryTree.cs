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
      for (int y = 0; y < grid.Rows; y++)
      {
        for (int x = 0; x < grid.Columns; x++)
        {
          Cell cell = grid.CellAt(new Point(x + 1, y + 1));
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
}