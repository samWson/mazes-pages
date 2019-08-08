using Xunit;
using MazeGenerator.Models;

namespace MazeGeneratorTest
{
  public class GridTest
  {
    private readonly Grid _grid;
    private readonly Point _firstCoordinate;
    private readonly Point _secondCoordinate;

    public GridTest()
    {
      _grid = new Grid(new Point(4, 4));
      _firstCoordinate = new Point(1,1);
      _secondCoordinate = new Point(2,1);

    }

    [Fact]
    public void TestLinkCells()
    {
      _grid.LinkCells(_firstCoordinate, _secondCoordinate);

      Assert.True(_grid.AreCellsLinked(_firstCoordinate, _secondCoordinate));
    }

    [Fact]
    public void TestAreCellsLinked()
    {

    }
  }
}