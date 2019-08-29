using System;
using System.ComponentModel.DataAnnotations;
using MazeGenerator.Models;

namespace MazeGenerator.Algorithms
{
  /// <summary>
  /// Enum Algorithm. These enums are algorithm options available to generate a maze on a grid.
  /// </summary>
  public enum Algorithm
  {
    [Display(Name = "Binary Tree")]
    BinaryTree,
    [Display(Name = "Sidewinder")]
    Sidewinder
  }

  public class AlgorithmFactory
  {
    /// <summary>
    /// Returns an algorithm instance based on the provided name.
    /// </summary>
    /// <example>
    /// <code>
    /// </code>
    /// </example>
    /// <param name="algorithm">An enum value matching the name of the algorithm required.</param>
    /// <param name="grid">The <c>Grid</c> instance on which the algorithm is to be applied.</param>
    /// <exception cref="System.ArgumentException">
    /// Thrown when the name is not a known algorithm or is null.
    /// </exception>
    public static IAlgorithm GetAlgorithm(Algorithm algorithm, Grid grid)
    {
      switch (algorithm)
      {
        case Algorithm.BinaryTree:
          return new BinaryTree(grid);
        case Algorithm.Sidewinder:
          return new Sidewinder(grid);
        default:
          throw new ArgumentException("Given algorithm does not match a known enum.");
      }
    }
  }
}