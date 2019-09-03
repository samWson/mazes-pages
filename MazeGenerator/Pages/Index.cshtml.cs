using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MazeGenerator.Models;
using MazeGenerator.Algorithms;
using System.Drawing;
using static MazeGenerator.Models.Grid;
using System;

namespace MazeGenerator.Pages
{
  public class IndexModel : PageModel
  {
    [ViewData]
    public string Maze { get; set; }

    [ViewData]
    public string ImageSource { get; set; }

    [ViewData]
    public Format DisplayFormat { get; set; }

    public Algorithm Algorithms { get; set; }
    public Format Formats { get; set; }

    // FIXME: This method keeps submitting the last entered values from the form
    // each time the page is refreshed.
    public void OnPostGenerate(int rows, int columns, Algorithm algorithm, Format format)
    {
      Grid grid = new Grid(new Point(columns, rows));
      IAlgorithm linkingAlgorithm = AlgorithmFactory.GetAlgorithm(algorithm, grid);
      linkingAlgorithm.Apply();

      DisplayFormat = format;

      switch (format)
      {
        case Format.Ascii:
          Maze += grid.ToString();
          break;
        case Format.Png:
          grid.ToPng();
          // FIXME: Check that this URL is correct.
          ImageSource = "~/StaticFiles/MazeImages/maze.png";
          break;
        default:
          // REVIEW: What would be better here is displaying a flash message at the GUI.
          throw new ArgumentException("Given format does not match a known enum.");
      }
    }
  }
}
