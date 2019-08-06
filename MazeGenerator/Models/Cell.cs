using System;
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

        public void Link(Cell cell, bool bidirectional = false)
        {
            Links.Add(cell);

            if (bidirectional)
            {
                cell.Link(this);
            }
        }
    }
}
