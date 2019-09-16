# Mazes Pages

A Maze generating web site built with ASP.NET Core Razor Pages.

## Usage

.NET Core 2.2 will need to be installed to run this project.

1. Clone this git repository to your computer
2. At the terminal change directories into the cloned repository
3. Run the MazeGenerator project: `dotnet run --project MazeGenerator/`
4. Open the URL `https://localhost:5001` in a web browser
5. Enter the number of rows and columns for the maze
6. Select the algorithm used to generate the maze
7. Select the target output for displaying the maze

### Viewing the Maze

The generated maze can be viewed on the website either as ASCII art or as an image. Currently displaying the image is broken but the generated PNG file can still be found and opened at the location `MazeGenerator/MazeImages/maze.png` from the repository root.

## Reference

Inspired by the ebook [Mazes for Programmers](http://www.mazesforprogrammers.com/) by Jamis Buck.

## Licence

This is open source software under the MIT licence.
