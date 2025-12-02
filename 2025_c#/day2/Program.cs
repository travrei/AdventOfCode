using System;
using System.IO;

//Getting the input file
string filePath = @"/home/andreie/dev/adventofcode/2025_c#/day2/input.txt";

try
{
    string fileContent = File.ReadAllText(filePath); //Load all the file into a string

    string[] rangeStrings = fileContent.Trim().Split(','); //Break on the char ',' and remove any whitespaces

    long totalSum = 0;

    foreach (string rangeString in rangeStrings)
    {
        string[] parts = rangeString.Split('-'); //Break on the char '-'

        long start = long.Parse(parts[0]); //Getting the first number
        long end = long.Parse(parts[1]); //Getting the second number

        for (long i = start; i <= end; i++) //Making a loop with the id's numbers
        {
            string numberToVerify = i.ToString(); //Converting the number to string!
            //Dividing the numbers in half
            //If it's odd, the check will always be false
            //If is pair we check
            string half1 = numberToVerify.Substring(0, numberToVerify.Length / 2);
            string half2 = numberToVerify.Substring(numberToVerify.Length / 2);
            
            //If the first half is equal to the second, we got a invalid ID!
            if (half1 == half2)
            {
                totalSum += i;
            }
        }
    }
    Console.WriteLine($"The total sum is: {totalSum}");
}
//If I got any errors, I'll know!
catch (IOException e)
{
    Console.WriteLine($"A Error has ocurred: {e.Message}");
}
