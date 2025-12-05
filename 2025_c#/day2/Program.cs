using System;
using System.IO;

//Getting the input file
string filePath = @"input.txt";

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
            bool isInvalid = false;
            //We are going to test the patterns to se if fits
            for (int len = 1; len <= numberToVerify.Length / 2; len++)
            {
                if (numberToVerify.Length % len != 0) continue; //If total isn't divisible by the size of a pattern, we skip

                //We get the "candidate" a pattern
                string pattern = numberToVerify.Substring(0, len);

                //Let's rebuild the string using this pattern to se if fits
                //I know this can be optimized, but I'm on vacation of my job so... Quick & Dirt it's!
                string rebuild = "";
                int repetitions = numberToVerify.Length / len;
                for (int k = 0; k < repetitions; k++)
                {
                    rebuild += pattern;
                }

                if (rebuild == numberToVerify)
                {
                    isInvalid = true; //If I rebuild the pattern and we ended up with the same string as we started, we find a invalid ID!
                    break;
                }
            }

            if (isInvalid)
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
