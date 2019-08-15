using System.Collections.Generic;

namespace MazeGenerator.Models
{
    public class Cell
    {
        public int Row { get; }
        public int Column { get; }
        public List<Cell> Links { get; set; }
        public Cell North { get; set; }
        public Cell South { get; set; }
        public Cell East { get; set; }
        public Cell West { get; set; }

        public Cell(Point coordinates)
        {
            Column = coordinates.X;
            Row = coordinates.Y;
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
