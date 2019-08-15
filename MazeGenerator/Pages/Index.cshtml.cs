using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MazeGenerator.Models;

namespace MazeGenerator.Pages
{
    public class IndexModel : PageModel
    {
        // FIXME: This method keeps submitting the last entered values from the form
        // each time the page is refreshed.
        public void OnPostGenerate(int rows, int columns)
        {
            Point extent = new Point(columns, rows);
            Grid grid = new Grid(extent);

            Console.WriteLine($"\n\nNumber of rows: {rows}\nNumber of columns: {columns}\n\n");
            Console.WriteLine($"\n\n{grid.ToString()}\n\n");
        }
    }
}
