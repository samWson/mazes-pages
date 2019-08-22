using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MazeGenerator.Models;

namespace MazeGenerator.Pages
{
  public class IndexModel : PageModel
  {
    [ViewData]
    public string Maze { get; set; }

    // FIXME: This method keeps submitting the last entered values from the form
    // each time the page is refreshed.
    public void OnPostGenerate(int rows, int columns)
    {
      Grid grid = new Grid(new Point(columns, rows));

      Maze += grid.ToString();
    }
  }
}
