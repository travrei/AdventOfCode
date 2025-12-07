using System;
using System.IO;

// Path to the input file containing the grid data
string filePath = @"input.txt";

try
{
    // Read all lines from the input file
    string[] lines = File.ReadAllLines(filePath);

    // Find the maximum line length to normalize grid dimensions
    int maxSize = lines.Max(x => x.Length);

    // Pad all lines to the same length to create a rectangular grid
    for (int i = 0; i < lines.Length; i++)
    {
        lines[i] = lines[i].PadRight(maxSize);
    }

    // Accumulator for the sum of all block results
    long grandTotal = 0;

    // Starting column index of the current block
    int startCol = 0;

    // Iterate through each column to find block boundaries (empty columns separate blocks)
    for (int col = 0; col <= maxSize; col++)
    {
        // Flag to track if current column is empty (contains only spaces)
        bool isEmptyCol = true;

        if (col < maxSize)
        {
            // Check all rows in the current column
            for (int row = 0; row < lines.Length; row++)
            {
                // If any non-space character is found, column is not empty
                if (lines[row][col] != ' ')
                {
                    isEmptyCol = false;
                    break;
                }
            }
        }

        // When an empty column is found, it marks the end of a block
        if (isEmptyCol)
        {
            // Process the block if it contains data (col > startCol)
            if (col > startCol)
            {
                // Solve the block and add the result to the grand total
                grandTotal += SolveBlock(lines, startCol, col);
            }
            // Move the starting position to the next column after the empty column
            startCol = col + 1;
        }
    }

    // Output the final sum of all block calculations
    Console.WriteLine(grandTotal);
}
catch (IOException e)
{
    // Handle file I/O errors
    Console.WriteLine($"A Error Has Occured: {e.Message}");
}


// Solves a mathematical expression within a block of the grid
// Parameters: grid (the input grid), startCol (inclusive), endCol (exclusive)
// Returns the result of applying the operation to all numbers found
long SolveBlock(string[] grid, int startCol, int endCol)
{
    // List to store all numeric values found in this block
    List<long> numbers = new List<long>();

    // The operation to apply between numbers ('?' = no operation found)
    char operation = '?';

    // Process each row in the grid
    for (int row = 0; row < grid.Length; row++)
    {
        // Extract the substring for this block from the current row
        int length = endCol - startCol;
        string element = grid[row].Substring(startCol, length).Trim();

        // Skip empty elements
        if (string.IsNullOrEmpty(element)) continue;

        // Try to parse the element as a number
        if (long.TryParse(element, out long number))
        {
            numbers.Add(number);
        }
        // Check if the element is a valid operator (+ or *)
        else if (element == "+" || element == "*")
        {
            operation = element[0];
        }
    }

    // Return 0 if no valid operator or numbers were found
    if (operation == '?' || numbers.Count == 0) return 0;

    // Start with the first number as the initial result
    long result = numbers[0];

    // Apply the operation to each subsequent number
    for (int i = 1; i < numbers.Count; i++)
    {
        if (operation == '+') result += numbers[i];
        if (operation == '*') result *= numbers[i];
    }

    // Return the final calculated result for this block
    return result;
}
