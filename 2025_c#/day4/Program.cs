using System;
using System.IO;

//Grabbing the input (Again Fullpath just because I'm on vacation)
string filePath = @"/workspaces/AdventOfCode/2025_c#/day4/test_input.txt";

try
{
    string[] lines = File.ReadAllLines(filePath);

    int bounds = lines.Length;
    for (int x = 0; x < bounds; x++)
    {
        for(int y = 0; y < lines[x].Length; y++)
        {
            char currentChar = lines[x][y];
            if(currentChar == '.')
            {
                continue;
            }
            if(IsValid(x, y))
            {
                Console.Write('#');
            }
        }
        Console.WriteLine();
    }
}
catch(IOException e)
{
    Console.WriteLine($"Can not open file -- ERR: {e.Message}");
}


bool IsValid(int x, int y)
{
    int[] dx = {-1, 0, 1, -1, 1, -1, 0, 1};
    int[] dy = {1, 1, 1, 0, 0, -1, -1, -1};
    
    for(int k = 0; k < 8; k++)
    {
        int neighX = x + dx[k];
        int neighY = y + dy[k];

        int amountOfPapers = 0;

        if (neighX <=0 || neighY <= 0 || neighX >= bounds || neighY >= bounds)
        {
            continue;
        }

        if(lines[neighX][neighY] == '@')
        {
            amountOfPapers++;
        }

        return amountOfPapers >= 4 ? false : true;
    }
}