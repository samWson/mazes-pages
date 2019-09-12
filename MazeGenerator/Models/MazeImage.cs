using System.ComponentModel.DataAnnotations;
using System.IO;
using SkiaSharp;

namespace MazeGenerator.Models
{
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

  /// <summary>
  /// MazeImage is a helper type with the responsibility of
  /// creating image files representing <c>Grid</c> instances.
  /// </summary>
  internal class MazeImage
  {
    private Grid grid;
    private const string imagePath = @"MazeImages/maze.png";
    internal MazeImage(Grid grid)
    {
      this.grid = grid;
    }

    /// <summary>
    /// ToPng() writes a representation of a Grid to a PNG file.
    /// </summary>
    internal void ToPng()
    {
      int cellSize = 10;
      int width = (cellSize * grid.Columns) + 1;
      int height = (cellSize * grid.Rows) + 1;

      SKBitmap bitmap = new SKBitmap(width, height);
      SKPaint wallPaint = new SKPaint();
      wallPaint.Color = SKColors.Black;

      using (SKCanvas canvas = new SKCanvas(bitmap))
      {
        setBackgroundToWhite(canvas);

        foreach (Cell cell in grid)
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

    private void setBackgroundToWhite(SKCanvas canvas)
    {
      canvas.Clear(SKColors.White);
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

    private void drawNorthBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (cell.North == null)
      {
        canvas.DrawLine(cellCorners.northWest, cellCorners.northEast, wallPaint);
      }
    }

    private void drawWestBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (cell.West == null)
      {
        canvas.DrawLine(cellCorners.northWest, cellCorners.southWest, wallPaint);
      }
    }

    private void drawEastBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (!cell.IsLinked(cell.East))
      {
        canvas.DrawLine(cellCorners.northEast, cellCorners.southEast, wallPaint);
      }
    }

    private void drawSouthBoundary(SKPaint wallPaint, SKCanvas canvas, Cell cell, (SKPoint northEast, SKPoint southEast, SKPoint southWest, SKPoint northWest) cellCorners)
    {
      if (!cell.IsLinked(cell.South))
      {
        canvas.DrawLine(cellCorners.southWest, cellCorners.southEast, wallPaint);
      }
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
  }
}