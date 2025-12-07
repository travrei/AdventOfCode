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
    // Handle file I/O errors and display the error message
    Console.WriteLine($"A Error Has Occured: {e.Message}");
}


// Solves a mathematical expression within a block of the grid
// startCol: the starting column index (inclusive) of the block to process
// endCol: the ending column index (exclusive) of the block to process
// Returns: the calculated result after applying the operation to all numbers
long SolveBlock(string[] grid, int startCol, int endCol)
{
    // List to store all numeric values found in this block
    List<long> numbers = new List<long>();

    // The operation to apply between numbers ('?' means no operation was found)
    char operation = '?';

    // Get the index of the last row (bottom row containing the operation)
    int lastRowIndex = grid.Length - 1;

    // Scan the last row within the block to find the operation symbol
    for (int c = startCol; c < endCol; c++)
    {
        char symbol = grid[lastRowIndex][c];

        // Identify the operation type (+, *, or unknown)
        switch (symbol)
        {
            case '+':
                operation = '+';
                break;
            case '*':
                operation = '*';
                break;
            default:
                break;
        }
    }

    // If no valid operation was found, return 0 as the result
    if (operation == '?') return 0;

    // Iterate through columns from right to left to extract numbers
    for (int col = endCol - 1; col >= startCol; col--)
    {
        // String to accumulate digits from current column
        string numStr = "";

        // Scan all rows (except the last one) in this column for digits
        for (int row = 0; row < lastRowIndex; row++)
        {
            char c = grid[row][col];

            // Append digit characters to build the number string
            if (char.IsDigit(c))
            {
                numStr += c;
            }
        }

        // If a number was found, convert it to long and add to list
        if (!string.IsNullOrEmpty(numStr))
        {
            numbers.Add(long.Parse(numStr));
        }
    }

    // If no numbers were found, return 0 as the result
    if (numbers.Count == 0) return 0;

    // Initialize result with the first number in the list
    long result = numbers[0];

    // Apply the operation to each subsequent number
    for (int i = 1; i < numbers.Count; i++)
    {
        switch (operation)
        {
            case '+':
                // Add the current number to the result
                result += numbers[i];
                break;
            case '*':
                // Multiply the result by the current number
                result *= numbers[i];
                break;
        }
    }

    // Return the final calculated result
    return result;
}
