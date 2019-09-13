using System.Drawing;
using System.Text;

namespace MazeGenerator.Models
{
  // <summary>
  // MazeString builds a string representation of a <c>Grid</c> instance.
  // </summary>
  internal class MazeString
  {
    private readonly Grid grid;
    private StringBuilder stringBuilder = new StringBuilder();
    private const string cellCorner = "+";
    private const string horizontalBoundary = "---";
    private const string unlinkedVerticalBoundary = "|";
    private const string linkedVerticalBoundary = " "; // One whitespace.
    private const string linkedHorizontalBoundary = "   "; // Three whitespaces.
    private const string cellBody = "   "; // Three whitespaces.
    private const string unlinkedHorizontalBoundary = "---";

    internal MazeString(Grid grid)
    {
      this.grid = grid;
    }

    public override string ToString()
    {
      appendNorthBoundary();
      buildRows();

      return stringBuilder.ToString();
    }

    private void appendNorthBoundary()
    {
      stringBuilder.Append(cellCorner);

      for (int x = 0; x < grid.Columns; x++)
      {
        stringBuilder.Append($"{horizontalBoundary}{cellCorner}");
      }

      stringBuilder.Append("\n");
    }

    private void buildRows()
    {
      for (int y = 0; y < grid.Rows; y++)
      {
        StringBuilder rowTopBuilder = new StringBuilder(unlinkedVerticalBoundary);
        StringBuilder rowBottomBuilder = new StringBuilder(cellCorner);

        for (int x = 0; x < grid.Columns; x++)
        {
          Cell cell = grid.CellAt(new Point(x + 1, y + 1));

          rowTopBuilder.Append(buildCellTopHalf(cell));
          rowBottomBuilder.Append(buildCellBottomHalf(cell));
        }

        stringBuilder.AppendLine(rowTopBuilder.ToString());
        stringBuilder.AppendLine(rowBottomBuilder.ToString());
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
  }
}