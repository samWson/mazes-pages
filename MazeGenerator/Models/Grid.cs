namespace MazeGenerator.Models
{
    /// Grid abstracts a collection of cells arranged in a rectangle.
    /// Access to cells in the grid is through X and Y coordinates.
    /// The grid position of X = 1 and Y = 1 starts in the top left,
    /// or north western corner.
    public class Grid
    {
      private Cell[,] _cells;
      public int Columns { get; }
      public int Rows { get; }
      public Grid(Point extent)
      {
        Columns = extent.X;
        Rows = extent.Y;
        PrepareGrid();
        ConfigureCells();
      }

      public void LinkCells(Point firstCoordinate, Point secondCoordinate)
      {
        Cell firstCell = _cells[firstCoordinate.X, firstCoordinate.Y];
        Cell secondCell = _cells[secondCoordinate.X, secondCoordinate.Y];

        firstCell.LinkBidirectionally(secondCell);
      }

      public bool AreCellsLinked(Point firstCoordinate, Point secondCoordinate)
      {
        Cell firstCell = _cells[firstCoordinate.X, secondCoordinate.Y];
        Cell secondCell = _cells[secondCoordinate.X, secondCoordinate.Y];

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

        return _cells[coordinates.X - 1, coordinates.Y - 1];
      }

      private void PrepareGrid()
      {
        Cell[,] cells = new Cell [Columns, Rows];

        for (int i = 0; i < Columns; i++)
        {
          for (int j = 0; j < Rows; j++)
          {
            cells[i, j] = new Cell(new Point(i + 1, j + 1));
          }
        }

        _cells = cells;
      }

      private void ConfigureCells()
      {
          for (int i = 0; i < Columns; i++)
          {
              for (int j = 0; j < Rows; j++)
              {
                  Cell cell = this.CellAt(new Point(i + 1, j + 1));

                  int column = cell.Column;
                  int y = cell.Row;

                  // Need to account for out of bounds access here
                  // On OutOfBounds set to a nullable cell e.g. Cell?
                  cell.North = this.CellAt(new Point(column, y - 1));
                  cell.South = this.CellAt(new Point(column, y + 1));
                  cell.East = this.CellAt(new Point(column + 1, y));
                  cell.West = this.CellAt(new Point(column - 1, y));
              }
          }
      }
    }
}