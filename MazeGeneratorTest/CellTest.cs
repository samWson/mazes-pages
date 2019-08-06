using System;
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
            _firstCell = new Cell();
            _secondCell = new Cell();
        }

        [Fact]
        public void TestLink()
        {
            _firstCell.Link(_secondCell, true);

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
            _firstCell.Link(_secondCell, true);

            Assert.True(_firstCell.IsLinked(_secondCell));
        }
    }
}
