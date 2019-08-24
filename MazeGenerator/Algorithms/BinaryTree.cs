using MazeGenerator.Models;
using System;
using System.Collections.Generic;

namespace MazeGenerator.Algorithms
{
  public class BinaryTree
  {
    private Grid grid;
    private List<Cell> neighbors;

    public BinaryTree(Grid grid)
    {
      this.grid = grid;
      neighbors = new List<Cell>();
    }

    public void Apply()
    {
      foreach (Cell cell in grid)
      {
        addNeighbors(cell);
        linkRandomNeighbor(cell);
      }
    }

    private void addNeighbors(Cell cell)
    {
      if (cell.North != null)
      {
        neighbors.Add(cell.North);
      }

      if (cell.East != null)
      {
        neighbors.Add(cell.East);
      }
    }

    private void linkRandomNeighbor(Cell cell)
    {
      if (neighbors.Count > 0)
      {
        Random random = new Random();
        int index = random.Next(0, neighbors.Count);

        Cell neighbor = neighbors[index];
        cell.LinkBidirectionally(neighbor);
        neighbors.Clear();
      }
    }
  }
}
