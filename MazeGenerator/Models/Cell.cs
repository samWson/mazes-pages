using System.Collections.Generic;

namespace MazeGenerator.Models
{
    public class Cell
    {
        public List<Cell> Links { get; set; }

        public Cell()
        {
            Links = new List<Cell>();
        }

        public void LinkBidirectionally(Cell cell)
        {
            this.Link(cell);
            cell.Link(this);
        }

        public bool IsLinked(Cell cell)
        {
            return Links.Contains(cell);
        }

        private void Link(Cell cell)
        {
            Links.Add(cell);
        }
    }
}
