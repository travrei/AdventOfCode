using System;
using System.IO;

// Grabbing the input (Again Fullpath just because I'm on vacation)
string filePath = @"/home/andreie/dev/adventofcode/2025_c#/day4/input.txt";

int totalAccess = 0;

try
{
    // Read all lines from the input file
    string[] lines = File.ReadAllLines(filePath);

    // Get the number of rows and columns in the grid
    int rows = lines.Length;
    int cols = lines[0].Length;

    // Iterate through each cell in the grid
    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < cols; y++)
        {
            // Skip empty cells (marked with '.')
            if (lines[x][y] == '.')
            {
                continue;
            }

            // Check if the current cell is valid based on its neighbors
            if (IsValid(x, y, lines))
            {
                totalAccess += 1;
            }
        }
    }

    // Output the total count of valid cells
    Console.WriteLine(totalAccess);
}
catch (IOException e)
{
    // Handle file reading errors
    Console.WriteLine($"Can not open file -- ERR: {e.Message}");
}

// Function to check if a cell is valid based on its neighbors
bool IsValid(int x, int y, string[] lines)
{
    // Directions for 8 neighboring cells (all 8 possible directions)
    int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
    int[] dy = { 1, 1, 1, 0, 0, -1, -1, -1 };
    int amountOfPapers = 0;

    // Get grid dimensions
    int maxRows = lines.Length;
    int maxCols = lines[0].Length;

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
        if (lines[neighX][neighY] == '@')
        {
            amountOfPapers++;
        }
    }

    // Return true if less than 4 neighboring '@' symbols
    return amountOfPapers < 4;
}
