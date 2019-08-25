using Xunit;
using MazeGenerator.Models;
using System.Drawing;

namespace MazeGeneratorTest
{
  public class CellTest
  {
    private readonly Cell centerCell;
    private readonly Cell eastCell;
    private readonly Cell northCell;

    public CellTest()
    {
      centerCell = new Cell(new Point(1, 2));
      eastCell = new Cell(new Point(2, 2));
      northCell = new Cell(new Point(1, 2));
    }

    [Fact]
    public void TestLink()
    {
      centerCell.LinkBidirectionally(eastCell);

      Assert.Collection<Cell>(
          centerCell.Links,
          item => Assert.Equal<Cell>(eastCell, item)
          );

      Assert.Collection<Cell>(
          eastCell.Links,
          item => Assert.Equal<Cell>(centerCell, item)
          );
    }

    [Fact]
    public void TestIsLinked()
    {
      centerCell.LinkBidirectionally(eastCell);

      Assert.True(centerCell.IsLinked(eastCell));
    }
  }
}
