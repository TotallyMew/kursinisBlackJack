using System;
using System.Collections.Generic;

namespace BlackJackKursinis
{
    public class InputOutput
    {
        public string getInput(string message)
        {
            Console.Write(message + ": "); 
            return Console.ReadLine();
        }

        public void displayMessage(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine(); 
        }

        public double readDouble(string prompt, double minValue = 0.0, double maxValue = 10000.0)
        {
            double value;
            while (true)
            {
                string input = getInput(prompt);
                if (double.TryParse(input, out value) && value >= minValue && value <= maxValue)
                {
                    break;
                }

                displayMessage($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
            }

            return value;
        }

        public bool readYesNo(string prompt)
        {
            while (true)
            {
                string input = getInput(prompt + " (Y/N)").Trim().ToUpper();
                if (input == "Y") return true;
                if (input == "N") return false;

                displayMessage("Invalid input. Please enter 'Y' for yes or 'N' for no.");
            }
        }

        public void displayCollection<T>(string header, IEnumerable<T> collection)
        {
            displayMessage(header);

            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }
    }
}
