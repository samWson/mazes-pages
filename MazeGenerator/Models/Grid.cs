using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using SkiaSharp;
using System.IO;

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

    /// <summary>
    /// Enum Format are the formatting options available for how a grid can be visually represented.
    /// </summary>
    public enum Format
    {
      [Display(Name = "ASCII Art")]
      Ascii,
      [Display(Name = "PNG Image")]
      Png
    }

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
    private const string imagePath = @"MazeImages/maze.png";
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
      int cellSize = 10;
      int width = (cellSize * Columns) + 1;
      int height = (cellSize * Rows) + 1;

      SKBitmap bitmap = new SKBitmap(width, height);
      SKPaint wallPaint = new SKPaint();
      wallPaint.Color = SKColors.Black;

      using (SKCanvas canvas = new SKCanvas(bitmap))
      {
        setBackgroundToWhite(canvas);

        foreach (Cell cell in this)
        {
          var cellCorners = setCellCorners(cell, cellSize);

          drawNorthBoundary(wallPaint, canvas, cell, cellCorners);
          drawWestBoundary(wallPaint, canvas, cell, cellCorners);
          drawEastBoundary(wallPaint, canvas, cell, cellCorners);
          drawSouthBoundary(wallPaint, canvas, cell, cellCorners);
        }
      }

      writePNG(bitmap);
    }

    private void drawSouthBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (!cell.IsLinked(cell.South))
      {
        canvas.DrawLine(cellCorners.southWest, cellCorners.southEast, wallPaint);
      }
    }

    private void drawEastBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (!cell.IsLinked(cell.East))
      {
        canvas.DrawLine(cellCorners.northEast, cellCorners.southEast, wallPaint);
      }
    }

    private void drawWestBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (cell.West == null)
      {
        canvas.DrawLine(cellCorners.northWest, cellCorners.southWest, wallPaint);
      }
    }

    private void drawNorthBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (cell.North == null)
      {
        canvas.DrawLine(cellCorners.northWest, cellCorners.northEast, wallPaint);

      }
    }

    private (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) setCellCorners(Cell cell, int cellSize)
    {
      int x1 = (cell.Column - 1) * cellSize;
      int y1 = (cell.Row - 1) * cellSize;
      int x2 = cell.Column * cellSize;
      int y2 = cell.Row * cellSize;

      SKPoint northEast = new SKPoint(x2, y1);
      SKPoint southEast = new SKPoint(x2, y2);
      SKPoint southWest = new SKPoint(x1, y2);
      SKPoint northWest = new SKPoint(x1, y1);

      return (northEast, southEast, southWest, northWest);
    }

    private void setBackgroundToWhite(SKCanvas canvas)
    {
      canvas.Clear(SKColors.White);
    }

    private void writePNG(SKBitmap bitmap)
    {
      string path = Path.Combine(Directory.GetCurrentDirectory(), imagePath);
      deleteExistingMazeFile(path);

      using (FileStream fileStream = File.Create(path))
      using (SKManagedWStream writer = new SKManagedWStream(fileStream))
      {
        SKPixmap.Encode(writer, bitmap, SKEncodedImageFormat.Png, 100);
      }
    }

    private void deleteExistingMazeFile(string path)
    {
      if (File.Exists(path))
      {
        File.Delete(path);
      }
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