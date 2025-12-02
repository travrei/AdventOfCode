//This is the code that I used to find the solution for the first part!
using System;
using System.IO;
using System.Linq;

//Reading the input file
string filePath = @"/home/andreie/dev/adventofcode/2025_c#/day1/input.txt";
int dial = 50; //Setting the dial to start at the position 50
int passwd = 0; //The password count

try
{
    string[] lines = File.ReadAllLines(filePath); //Read all the lines of a file
    int line_number = 0; // Just to follow in witch line the reader is.
    foreach (string linestr in lines)
    {
        string letter = string.Join("", linestr.Where(char.IsLetter)); //Getting what is the letter
        string numberstr = string.Join("", linestr.Where(char.IsDigit)); //Getting the numbers
        int number = int.Parse(numberstr); //Parsing the numbers from string to int
        switch (letter) //Here I choose what to do
        //If 'L' I'll subtract the dial
        //If 'R' I'll sum the dial
        {
            case "L":
                dial -= number;
                break;
            case "R":
                dial += number;
                break;
            default:
                break;
        }
        dial = ((dial % 100) + 100) % 100; //The % op. Is making sure that I never goes up 99 or bellow 0
        line_number++; //Again, just to follow in witch line I'm at
        
        Console.WriteLine($"{line_number}: {letter} {number} --> {dial}");
        //If The Dial stops on 0... I add one to the password.
        if (dial == 0)
        {
            passwd++;
        }
    }
    
    Console.WriteLine($"Passwd: {passwd}"); //The result for the site
}
//If I can't open the file...
catch (IOException e)
{
    Console.WriteLine($"A Error has occured: {e.Message}");
}