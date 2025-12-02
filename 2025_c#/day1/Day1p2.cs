//This is the code that I used to find the solution for the first part!
using System;
using System.IO;
using System.Linq;

//Reading the input file
string filePath = @"/home/andreie/dev/adventofcode/2025_c#/day1/input.txt";
int dial = 50; //Setting the dial to start at the position 50
int passwd = 0; //The password count
int distanceToZero = 0; //Here is for count how many times I got a click

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
                if (dial == 0) //If dial is 0, distance is 100
                {
                    distanceToZero = 100;
                }
                else //Else, distance is the dial
                {
                    distanceToZero = dial;
                }
                dial -= number;
                break;
            case "R":
                distanceToZero = 100 - dial;
                dial += number;
                break;
            default:
                break;
        }
        dial = ((dial % 100) + 100) % 100; //The % op. Is making sure that I never goes up 99 or bellow 0
        line_number++; //Again, just to follow in witch line I'm at
        
        Console.WriteLine($"{line_number}: {letter} {number} --> {dial}");
        //Check if i got to the first Zero
        if (number >= distanceToZero)
        {
            passwd++;
            //After the first moviment, how many moviments is left?
            int leftOver = number - distanceToZero;
            passwd += leftOver / 100; //Here is how many loops "fit" in 100
        }
    }
    
    Console.WriteLine($"Passwd: {passwd}"); //The result for the site
}
//If I can't open the file...
catch (IOException e)
{
    Console.WriteLine($"A Error has occured: {e.Message}");
}