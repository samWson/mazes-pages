using Xunit;
using MazeGenerator.Models;

namespace MazeGeneratorTest
{
  public class CellTest
  {
    private readonly Cell _firstCell;
    private readonly Cell _secondCell;

    public CellTest()
    {
      _firstCell = new Cell(new Point(1, 1));
      _secondCell = new Cell(new Point(2, 1));
    }

    [Fact]
    public void TestLink()
    {
      _firstCell.LinkBidirectionally(_secondCell);

      Assert.Collection<Cell>(
          _firstCell.Links,
          item => Assert.Equal<Cell>(_secondCell, item)
          );

      Assert.Collection<Cell>(
          _secondCell.Links,
          item => Assert.Equal<Cell>(_firstCell, item)
          );
    }

    [Fact]
    public void TestIsLinked()
    {
      _firstCell.LinkBidirectionally(_secondCell);

      Assert.True(_firstCell.IsLinked(_secondCell));
    }
  }
}
