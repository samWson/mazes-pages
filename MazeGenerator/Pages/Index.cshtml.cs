using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MazeGenerator.Pages
{
    public class IndexModel : PageModel
    {
        public void OnPostGenerate(int rows, int columns)
        {
            // FIXME: This method keeps submitting the last entered values from the form
            // each time the page is refreshed.
            Console.WriteLine($"\n\nNumber of rows: {rows}\nNumber of columns: {columns}\n\n");
        }
    }
}
