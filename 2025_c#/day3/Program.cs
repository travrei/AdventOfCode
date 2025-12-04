using System;
using System.IO;

//Getting the input file (I'm using the full path because of... convenience.)
string filePath = "/home/andreie/dev/adventofcode/2025_c#/day3/input.txt";

int totalVoltage = 0;

try
{
    //Getting all the lines of the file
    string[] lines = File.ReadAllLines(filePath); 
    foreach (string line in lines) //Reading line by line
    {
        int bateriesCount = line.Length; //Getting the total of batteries in my line
        int biggerVoltageOfTheLine = 0;
        for (int i = 0; i < bateriesCount - 1; i++)
        {
            char cDozen = line[i]; //Grabbing the higher number to be the dozen
            for (int j = i + 1; j < bateriesCount; j++)
            {
                char cUnit = line[j]; //Grabbing the next number to be the unit
                string voltageStr = cDozen.ToString() + cUnit.ToString(); //Making the number
                int voltage =  int.Parse(voltageStr);
                if (voltage > biggerVoltageOfTheLine) //Checking if the number is bigger than the last
                {
                    biggerVoltageOfTheLine = voltage;
                }
            }
        }

        totalVoltage += biggerVoltageOfTheLine; //Sum the bigger number of the line in the total
    }
    Console.WriteLine($"Total: {totalVoltage}");
}
catch (IOException e)
{
    Console.WriteLine($"Can't open file, Error: {e.Message}");
}