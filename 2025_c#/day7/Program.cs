using System;
using System.IO;

string filePath = @"test_input.txt";

try
{
    string[] lines = File.ReadAllLines(filePath);

    int rows = lines.Length;
    int columns = lines[0].Length;

    char[,] grid = new char[rows, columns];
    char[,] nextGrid = new char[rows, columns];

    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < columns; y++)
        {
            grid[x, y] = lines[x][y];
            nextGrid[x, y] = lines[x][y];
        }
    }

    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < columns; y++)
        {
            if (grid[x, y] == 'S')
            {
                if (grid[x, y + 1] == '.')
                {
                    nextGrid[x, y + 1] = '|';
                }
            }
            if (grid[x, y] == '|')
            {
                if (grid[x, y + 1] == '.')
                {
                    nextGrid[x, y + 1] = '|';
                }
                else if (grid[x, y + 1] == '^')
                {
                    nextGrid[x - 1, y + 1] = '|';
                    nextGrid[x + 1, y + 1] = '|';
                }
            }
        }
    }
    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < columns; y++)
        {
            grid[x, y] = nextGrid[x, y];
        }
    }

    for (int x = 0; x < rows; x++)
    {
        for (int y = 0; y < columns; y++)
        {
            Console.Write(grid[x, y]);
        }
        Console.WriteLine();
    }
}
catch (IOException e)
{
    Console.WriteLine($"An error occurred while reading the file: {e.Message}");
}
