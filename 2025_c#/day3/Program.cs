using System;
using System.IO;

// Defines the full path to the input file.
string filePath = "input.txt";

// Accumulator variable to store the final sum of all generated numbers.
long totalVoltage = 0;

try
{
    // Reads all lines from the text file and stores them in a string array.
    string[] lines = File.ReadAllLines(filePath);

    // Iterates over each line individually.
    foreach (string line in lines)
    {
        // Gets the total length of the current line (total available characters).
        int bateriesCount = line.Length;

        // 'cursor' indicates the position from which we can start looking for the next digit.
        // Starts at 0 and advances as we select numbers.
        int cursor = 0;

        // Defines that we need to extract exactly 12 digits from each line.
        int batteriesNeeded = 12;

        // String that will build the final 12-digit number for the current line.
        string voltageOfTheLine = "";

        // Loop to find each of the 12 needed digits, one by one.
        for (int i = 0; i < batteriesNeeded; i++)
        {
            // Calculates how many digits we still need to select AFTER the current one.
            // Ex: If we are in the 1st loop (i=0), we need 11 more after this.
            int leftOver = (batteriesNeeded - 1) - i;

            // Calculates the maximum limit (index) up to which we can search in the string.
            // We cannot pick a digit too far at the end if we need to leave room
            // for the remaining digits ('leftOver').
            int bounds = bateriesCount - 1 - leftOver;

            // Variable to store the largest numeric character found in the search window.
            // Initializes with '/' (ASCII 47), which is smaller than '0' (ASCII 48).
            char biggerVoltage = '/';

            // Stores the index where we found this largest digit.
            int indexOfTheBattery = -1;

            // Scans the string from the current 'cursor' position up to the 'bounds' limit.
            // The goal is to find the largest possible digit in this valid window.
            for (int j = cursor; j <= bounds; j++)
            {
                // If the current character is greater than the largest one seen so far...
                if (line[j] > biggerVoltage)
                {
                    // ...we update the largest character and its position.
                    biggerVoltage = line[j];
                    indexOfTheBattery = j;
                }
            }
            // Appends the largest found digit to our final number.
            voltageOfTheLine += biggerVoltage;

            // Updates the cursor to the position immediately after the digit we just chose.
            // This ensures the next sought digit is to the right of the current one (maintaining order).
            cursor = indexOfTheBattery + 1;
        }

        // Converts the formed 12-digit string into a number (long) and adds it to the total.
        totalVoltage += long.Parse(voltageOfTheLine);
    }

    Console.WriteLine($"Total: {totalVoltage}");
}
catch (IOException e)
{
    // Error handling in case the file does not exist or cannot be read.
    Console.WriteLine($"Can't open file, Error: {e.Message}");
}
