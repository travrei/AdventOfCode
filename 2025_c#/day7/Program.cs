using System;
using System.IO;

// Path to the input file
string filePath = @"input.txt";

try
{
    // Read all lines from the input file
    string[] lines = File.ReadAllLines(filePath);

    // Get grid dimensions
    int rows = lines.Length;
    int columns = lines[0].Length;

    // Create two grids: current state and next state for simulation
    char[,] grid = new char[rows, columns];
    char[,] nextGrid = new char[rows, columns];

    // Flag to track if water is still dropping in the simulation
    bool isDropping = true;

    // Counter for split events (where water hits an obstacle and spreads)
    int splits = 0;

    // Initialize both grids with the input data
    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < columns; y++)
        {
            grid[x, y] = lines[x][y];
            nextGrid[x, y] = lines[x][y];
        }
    }

    //Console.Clear();
    //Console.CursorVisible = false;

    // Main simulation loop - continues while water is actively dropping
    while (isDropping)
    {
        isDropping = false;

        // Iterate through each cell in the grid
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                // Check if current cell is the water source
                if (grid[y, x] == 'S')
                {
                    // If cell below is empty, water flows down
                    if (y + 1 < rows && grid[y + 1, x] == '.')
                    {
                        nextGrid[y + 1, x] = '|';
                        isDropping = true;
                    }
                }

                // Check if current cell has falling water
                if (grid[y, x] == '|')
                {
                    // Check if there's a cell below
                    if (y + 1 < rows)
                    {
                        // If cell below is empty, water continues falling
                        if (grid[y + 1, x] == '.')
                        {
                            nextGrid[y + 1, x] = '|';
                            isDropping = true;
                        }
                        // If water hits an obstacle (^), it spreads left and right
                        else if (grid[y + 1, x] == '^')
                        {
                            // Spread water to the left if space is available
                            if (x - 1 >= 0 && nextGrid[y + 1, x - 1] == '.')
                            {
                                nextGrid[y + 1, x - 1] = '|';
                                isDropping = true;
                            }

                            // Spread water to the right if space is available
                            if (x + 1 < columns && nextGrid[y + 1, x + 1] == '.')
                            {
                                nextGrid[y + 1, x + 1] = '|';
                                isDropping = true;
                            }
                        }
                    }
                }
            }
        }

        // Copy nextGrid state to grid for the next iteration
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                grid[y, x] = nextGrid[y, x];
            }
        }
        //Gamefy(grid, rows, columns);
    }

    // Count the number of splits (water hitting obstacles and spreading)
    for (int y = 0; y < rows; y++)
    {
        for (int x = 0; x < columns; x++)
        {
            // Check if current cell is an obstacle with water above it
            if (grid[y, x] == '^')
            {
                if (grid[y - 1, x] == '|')
                {
                    splits++;
                }
            }
        }
    }

    // Output the result
    Console.WriteLine($"We got {splits} of splits");
}

// Handle file reading errors
catch (IOException e)
{
    Console.WriteLine($"An error occurred while reading the file: {e.Message}");
}


// Helper function to display the grid state (for debugging/visualization)
void Gamefy(char[,] grid, int rows, int columns)
{
    // Move cursor to top-left to overwrite previous display
    Console.SetCursorPosition(0, 0);

    // Print the entire grid
    for (int y = 0; y < rows; y++)
    {
        for (int x = 0; x < columns; x++)
        {
            Console.Write(grid[y, x]);
        }
        Console.WriteLine();
    }

    // Add delay to visualize simulation step-by-step
    Thread.Sleep(300);
}
