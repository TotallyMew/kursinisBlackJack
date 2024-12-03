using System;
using System.Collections.Generic;

namespace BlackJackKursinis
{
    internal class InputOutput
    {
        // Displays a message with a colon and reads input on the same line
        public string PromptMessage(string message)
        {
            Console.Write(message + ": "); // Add a colon before taking input
            return Console.ReadLine();
        }

        // Displays a simple message with a blank line after for clarity
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(); // Add a blank line after the message
        }

        // Reads and parses a double value from the user
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

        // Reads a yes/no input from the user
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

        // Displays a collection of items with a header
        public void DisplayCollection<T>(string header, IEnumerable<T> collection)
        {
            DisplayMessage(header);

            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(); // Add a blank line after the collection
        }
    }
}
