using System;
using System.Collections.Generic;

namespace BlackJackKursinis
{
    public class InputOutput
    {
        public string PromptMessage(string message)
        {
            Console.Write(message + ": "); 
            return Console.ReadLine();
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(); 
        }

        public double ReadDouble(string prompt, double minValue = double.MinValue, double maxValue = double.MaxValue)
        {
            double value;
            while (true)
            {
                string input = PromptMessage(prompt);
                if (double.TryParse(input, out value) && value >= minValue && value <= maxValue)
                {
                    break;
                }

                DisplayMessage($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
            }

            return value;
        }

        public bool ReadYesNo(string prompt)
        {
            while (true)
            {
                string input = PromptMessage(prompt + " (Y/N)").Trim().ToUpper();
                if (input == "Y") return true;
                if (input == "N") return false;

                DisplayMessage("Invalid input. Please enter 'Y' for yes or 'N' for no.");
            }
        }

        public void DisplayCollection<T>(string header, IEnumerable<T> collection)
        {
            DisplayMessage(header);

            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }
    }
}
