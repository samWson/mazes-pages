using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;

namespace MazeGenerator.Models
{
  /// <summary>
  /// Grid abstracts a collection of cells arranged in a rectangle.
  /// Access to cells in the grid is through X and Y coordinates.
  /// The grid position of X = 1 and Y = 1 starts in the top left
  /// i.e. the north western corner.
  /// </summary>
  public class Grid : IEnumerable<Cell>
  {
    public int Columns { get; }
    public int Rows { get; }
    private Cell[,] cells;

    public Grid(Point extent)
    {
      Columns = extent.X;
      Rows = extent.Y;
      PrepareGrid();
      ConfigureCells();
    }

    private Cell[,] cells;

    private const string cellCorner = "+";
    private const string cellBody = "   "; // Three whitespaces.
    private const string horizontalBoundary = "---";
    private const string unlinkedVerticalBoundary = "|";
    private const string linkedVerticalBoundary = " "; // One whitespace.
    private const string linkedHorizontalBoundary = "   "; // Three whitespaces.
    private const string unlinkedHorizontalBoundary = "---";
    private StringBuilder asciiArt;

    public void LinkCells(Point firstCoordinate, Point secondCoordinate)
    {
      Cell firstCell = cells[firstCoordinate.X, firstCoordinate.Y];
      Cell secondCell = cells[secondCoordinate.X, secondCoordinate.Y];

      firstCell.LinkBidirectionally(secondCell);
    }

    public bool AreCellsLinked(Point firstCoordinate, Point secondCoordinate)
    {
      Cell firstCell = cells[firstCoordinate.X, secondCoordinate.Y];
      Cell secondCell = cells[secondCoordinate.X, secondCoordinate.Y];

      return firstCell.IsLinked(secondCell);
    }

    public Cell CellAt(Point coordinates)
    {
      if ((coordinates.X < 1) || (coordinates.X > Columns))
      {
        return null;
      }

      if ((coordinates.Y < 1) || (coordinates.Y > Rows))
      {
        return null;
      }

      return cells[coordinates.X - 1, coordinates.Y - 1];
    }

    /// <summary>
    /// ToString() is overriden to output an ASCII art representation
    /// of the grid.
    /// </summary>
    public override string ToString()
    {
      asciiArt = new StringBuilder();
      appendNorthBoundary();
      buildRows();

      return asciiArt.ToString();
    }

    /// <summary>
    /// ToPng() writes a representation of the grid to PNG image file.
    /// </summary>
    public void ToPng()
    {
      new MazeImage(this).ToPng();
    }

    public IEnumerator<Cell> GetEnumerator()
    {
      for (int y = 0; y < Rows; y++)
      {
        for (int x = 0; x < Columns; x++)
        {
          yield return this.CellAt(new Point(x + 1, y + 1));
        }
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    private void appendNorthBoundary()
    {
      asciiArt.Append(cellCorner);

      for (int x = 0; x < Columns; x++)
      {
        asciiArt.Append($"{horizontalBoundary}{cellCorner}");
      }

      asciiArt.Append("\n");
    }

    private void buildRows()
    {
      for (int y = 0; y < Rows; y++)
      {
        StringBuilder rowTopBuilder = new StringBuilder(unlinkedVerticalBoundary);
        StringBuilder rowBottomBuilder = new StringBuilder(cellCorner);

        for (int x = 0; x < Columns; x++)
        {
          Cell cell = this.CellAt(new Point(x + 1, y + 1));

          rowTopBuilder.Append(buildCellTopHalf(cell));
          rowBottomBuilder.Append(buildCellBottomHalf(cell));
        }

        asciiArt.AppendLine(rowTopBuilder.ToString());
        asciiArt.AppendLine(rowBottomBuilder.ToString());
      }
    }

    private string buildCellTopHalf(Cell cell)
    {
      string eastBoundary = (cell.IsLinked(cell.East)) ? linkedVerticalBoundary : unlinkedVerticalBoundary;
      return $"{cellBody}{eastBoundary}";
    }

    private string buildCellBottomHalf(Cell cell)
    {
      string southBoundary = (cell.IsLinked(cell.South)) ? linkedHorizontalBoundary : unlinkedHorizontalBoundary;
      return $"{southBoundary}{cellCorner}";
    }

    private void PrepareGrid()
    {
      Cell[,] cells = new Cell[Columns, Rows];

      for (int x = 0; x < Columns; x++)
      {
        for (int y = 0; y < Rows; y++)
        {
          cells[x, y] = new Cell(new Point(x + 1, y + 1));
        }
      }

      this.cells = cells;
    }

    private void ConfigureCells()
    {
      foreach (Cell cell in this)
      {
        int column = cell.Column;
        int row = cell.Row;

        cell.North = this.CellAt(new Point(column, row - 1));
        cell.South = this.CellAt(new Point(column, row + 1));
        cell.East = this.CellAt(new Point(column + 1, row));
        cell.West = this.CellAt(new Point(column - 1, row));
      }
    }
  }
}