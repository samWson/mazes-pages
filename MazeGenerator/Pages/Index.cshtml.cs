using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MazeGenerator.Models;
using MazeGenerator.Algorithms;
using System.Drawing;

namespace MazeGenerator.Pages
{
  public class IndexModel : PageModel
  {
    [ViewData]
    public string Maze { get; set; }

    public Algorithm Algorithms { get; set; }

    // FIXME: This method keeps submitting the last entered values from the form
    // each time the page is refreshed.
    public void OnPostGenerate(int rows, int columns, Algorithm algorithm)
    {
      Grid grid = new Grid(new Point(columns, rows));
      IAlgorithm linkingAlgorithm = AlgorithmFactory.GetAlgorithm(algorithm, grid);
      linkingAlgorithm.Apply();

      Maze += grid.ToString();
    }
  }
}
