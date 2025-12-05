using System;
using System.IO;

// Define the path to the input file.
string filePath = @"input.txt";

try
{
    // Read all lines from the input file.
    string[] fileContentLines = File.ReadAllLines(filePath);

    // A flag to determine if the current line being processed is an ID or a range.
    bool isID = false;
    
    // A list to store the ranges. Each range is a tuple of (min, max).
    List<(long Min, long Max)> ranges = new List<(long Min, long Max)>();
    // A list to store the IDs.
    List<long> ids = new List<long>();

    // A counter for the number of IDs that fall within the ranges.
    int totalFresh = 0;
    
    // Iterate over each line in the file.
    foreach (string linestr in fileContentLines)
    {
        // An empty line separates the ranges from the IDs.
        if (linestr == "")
        {
            isID = true;
            continue;
        }
        // If we are not yet at the IDs section, process the line as a range.
        if (!isID)
        {
            // Split the line by the '-' character to get the min and max of the range.
            string[] rangesLine = linestr.Split('-');
            long min = long.Parse(rangesLine[0]);
            long max = long.Parse(rangesLine[1]);
            // Add the range to the list of ranges.
            ranges.Add((min, max));
        }
        // Otherwise, process the line as an ID.
        //else ids.Add(long.Parse(linestr));
    }
    
    //Sorted the List of tuples
    ranges.Sort();
    long totalCovered = 0;
    long currentMin = ranges[0].Min; // Initialize with the first range's min
    long currentMax = ranges[0].Max; // Initialize with the first range's max
    
    // Iterate through the sorted ranges to merge overlapping ones
    for (int i = 1; i < ranges.Count; i++) 
    {
        long nextMin = ranges[i].Min; // Get the min of the next range
        long nextMax = ranges[i].Max; // Get the max of the next range

        if (currentMax >= nextMin) // Check for overlap
        {
            currentMax = Math.Max(currentMax, nextMax); // Merge by extending currentMax
        }
        else // No overlap, so the current merged range is complete
        {
            totalCovered += currentMax - currentMin + 1; // Add the length of the merged range
            currentMin = nextMin; // Start a new merged range with the next range's min
            currentMax = nextMax; // Start a new merged range with the next range's max
        }
    }
    
    totalCovered += (currentMax - currentMin + 1); // Add the last merged range
    Console.WriteLine($"{totalCovered}");
    /*
    // Iterate over each ID.
    foreach (long id in ids)
    {
        // For each ID, iterate over each range.
        foreach (var ran in ranges)
        {
            // Check if the ID falls within the current range.
            if (id >= ran.Min && id <= ran.Max)
            {
                // If it does, increment the counter and break out of the inner loop
                // as we only need to find one matching range.
                totalFresh++;
                break;
            }
        }
    }
    */

    // Print the total count of fresh IDs.
    //Console.WriteLine(totalFresh);
}
catch (IOException e)
{
    // Handle potential file I/O errors.
    Console.WriteLine($"A Error has ocurred: {e.Message}");
}
