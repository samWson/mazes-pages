using Xunit;
using MazeGenerator.Models;

namespace MazeGeneratorTest
{
  public class GridTest
  {
    private readonly Grid _grid;
    private readonly Point _firstCoordinate;
    private readonly Point _secondCoordinate;
    private const int columns = 3;
    private const int rows = 4;
    public GridTest()
    {
      _grid = new Grid(new Point(columns, rows));
      _firstCoordinate = new Point(1, 1);
      _secondCoordinate = new Point(2, 1);
    }

    [Fact]
    public void TestLinkCells()
    {
      _grid.LinkCells(_firstCoordinate, _secondCoordinate);

      Assert.True(_grid.AreCellsLinked(_firstCoordinate, _secondCoordinate));
    }

    [Fact]
    public void TestCellAtAccessingCell()
    {
      Cell cell = _grid.CellAt(new Point(1, 1));
      Assert.True(cell.Column == 1);
      Assert.True(cell.Row == 1);
    }

    [Theory]
    [InlineData(0, 1)] // Out of bounds west
    [InlineData(1, 0)] // Out of bounds north
    [InlineData(4, 5)] // Out of bounds south
    [InlineData(5, 3)] // Out of bounds east
    public void TestCellAtAccessingOutOfBounds(int x, int y)
    {
      Cell cell = _grid.CellAt(new Point(x, y));
      Assert.Null(cell);
    }

    [Fact]
    public void TestToString()
    {
      string expected = @"+---+---+---+
|   |   |   |
+---+---+---+
|   |   |   |
+---+---+---+
|   |   |   |
+---+---+---+
|   |   |   |
+---+---+---+
";

      Assert.Equal(expected, _grid.ToString());
    }
  }
}