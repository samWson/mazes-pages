namespace MazeGenerator.Models
{
  public class Grid
  {
    public Cell[,] Cells { get; set; }
    public int Columns { get; }
    public int Rows { get; }
    public Grid(Point extent)
    {
      Columns = extent.X;
      Rows = extent.Y;
      PrepareGrid();
    }

    private void PrepareGrid()
    {
      Cell[,] cells = new Cell [Columns, Rows];

      for (int i = 0; i < Columns; i++)
      {
        for (int j = 0; j < Rows; j++)
        {
          cells[i, j] = new Cell(new Point(i, j));
        }
      }

      Cells = cells;
    }

    public void LinkCells(Point firstCoordinate, Point secondCoordinate)
    {
      Cell firstCell = Cells[firstCoordinate.X, firstCoordinate.Y];
      Cell secondCell = Cells[secondCoordinate.X, secondCoordinate.Y];

      firstCell.LinkBidirectionally(secondCell);
    }

    public bool AreCellsLinked(Point firstCoordinate, Point secondCoordinate)
    {
      Cell firstCell = Cells[firstCoordinate.X, secondCoordinate.Y];
      Cell secondCell = Cells[secondCoordinate.X, secondCoordinate.Y];

      return firstCell.IsLinked(secondCell);
    }
  }
}