using MazeGenerator.Models;
using System.Collections.Generic;
using System;

namespace MazeGenerator.Algorithms
{
  class Sidewinder : IAlgorithm
  {
    private Grid grid;
    private bool atEasternBoundary;
    private bool atNorthernBoundary;
    private Random random = new Random();
    private List<Cell> run = new List<Cell>();

    public Sidewinder(Grid grid)
    {
      this.grid = grid;
    }

    public void Apply()
    {
      foreach (Cell cell in grid)
      {
        run.Add(cell);

        atEasternBoundary = cell.East == null;
        atNorthernBoundary = cell.North == null;

        if (shouldCloseRun())
        {
          closeRun();
        }
        else
        {
          cell.LinkBidirectionally(cell.East);
        }
      }
    }

    private bool shouldCloseRun()
    {
      return atEasternBoundary || (!atNorthernBoundary && (random.Next(0, 2) == 0));
    }

    private void closeRun()
    {
      Cell randomCell = run[random.Next(0, run.Count)];

      if (randomCell.North != null)
      {
        randomCell.LinkBidirectionally(randomCell.North);
      }

      run.Clear();
    }
  }
}