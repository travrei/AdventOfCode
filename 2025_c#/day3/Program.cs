using System;
using System.IO;

string filePath = "/home/andreie/dev/adventofcode/2025_c#/day3/input.txt";

int totalVoltage = 0;

try
{
    string[] lines = File.ReadAllLines(filePath);
    foreach (string line in lines)
    {
        int bateriesCount = line.Length;
        int biggerVoltageOfTheLine = 0;
        for (int i = 0; i < bateriesCount - 1; i++)
        {
            char cDozen = line[i];
            
            for (int j = i + 1; j < bateriesCount; j++)
            {
                char cUnit = line[j];
                string voltageStr = cDozen.ToString() + cUnit.ToString();
                int voltage =  int.Parse(voltageStr);
                if (voltage > biggerVoltageOfTheLine)
                {
                    biggerVoltageOfTheLine = voltage;
                }
            }
        }

        totalVoltage += biggerVoltageOfTheLine;
    }
    Console.WriteLine($"Total: {totalVoltage}");
}
catch (IOException e)
{
    Console.WriteLine($"Can't open file, Error: {e.Message}");
}