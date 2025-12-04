using System;
using System.IO;

// Grabbing the input (Again Fullpath just because I'm on vacation)
string filePath = @"/home/andreie/dev/adventofcode/2025_c#/day4/input.txt";

int totalRemoved = 0;

try
{
    // Read all lines from the input file
    string[] lines = File.ReadAllLines(filePath);

    // Get the number of rows and columns in the grid
    int rows = lines.Length;
    int cols = lines[0].Length;

    // Initialize the grid and nextGrid arrays
    char[,] grid = new char[rows, cols];
    char[,] nextGrid = new char[rows, cols];

    // Populate the grid with characters from the input file
    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < cols; y++)
        {
            grid[x, y] = lines[x][y];
        }
    }

    // Flag to indicate if there's work to be done in the current iteration
    bool workToDo = true;
    while (workToDo)
    {
        // Reset the workToDo flag at the start of each iteration
        workToDo = false;

        // Copy the current grid to nextGrid to prepare for the next iteration
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                nextGrid[x, y] = grid[x, y];
            }
        }

        // Process each cell in the grid
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                // Skip cells that are already processed or empty
                if (grid[x, y] == '.' || grid[x, y] == '#')
                {
                    continue;
                }

                // Check if the current cell should be marked for removal
                if (IsValid(x, y, grid))
                {
                    nextGrid[x, y] = '#';
                    totalRemoved++;
                    workToDo = true;
                }
            }
        }

        // Update the grid with the changes from nextGrid
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                grid[x, y] = nextGrid[x, y];
            }
        }

        /*
        // Uncomment to print the grid after each iteration
        Console.WriteLine("GRID:");
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < cols; y++)
            {
                Console.Write(grid[x, y]);
            }
            Console.WriteLine();
        }
        */
    }

    // Output the total number of cells removed
    Console.WriteLine($"REMOVED: {totalRemoved}");
}
catch (IOException e)
{
    // Handle file reading errors
    Console.WriteLine($"Can not open file -- ERR: {e.Message}");
}

// Function to check if a cell is valid based on its neighbors
bool IsValid(int x, int y, char[,] lines)
{
    // Directions for 8 neighboring cells (all 8 possible directions)
    int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
    int[] dy = { 1, 1, 1, 0, 0, -1, -1, -1 };
    int amountOfPapers = 0;

    // Get grid dimensions
    int maxRows = lines.GetLength(0);
    int maxCols = lines.GetLength(1);

    // Check all 8 neighboring cells
    for (int k = 0; k < 8; k++)
    {
        int neighX = x + dx[k];
        int neighY = y + dy[k];

        // Skip if neighbor is out of bounds
        if (neighX < 0 || neighY < 0 || neighX >= maxRows || neighY >= maxCols)
        {
            continue;
        }

        // Count neighboring cells with '@' symbol
        if (lines[neighX, neighY] == '@')
        {
            amountOfPapers++;
        }
    }

    // Return true if less than 4 neighboring '@' symbols
    return amountOfPapers < 4;
}
