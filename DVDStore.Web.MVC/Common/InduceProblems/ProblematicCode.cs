/**********************************************************************************
**
**  DVDStore Application v1.0
**
**  Copyright 2024
**  Developed by:
**     Ronald Garlit.
**
**  This software was created for educational purposes.
**
**  Use is subject to license terms.
***********************************************************************************
**
**  FileName: ProblematicCode.cs (DVDStore Application)
**  Version: 1.0
**  Author: Ronald Garlit
**  
**  Description: 
**  This class is designed to demonstrate various common coding issues that are typically flagged 
**  by static code analysis tools like SonarLint. The issues highlighted include the use of magic numbers, 
**  synchronous file reading, redundant calculations, inefficient collection usage, dead code, 
**  predictable random number generation, exposure of exception details, and unnecessary use of the async keyword.
**  It also includes an unused private string variable to demonstrate code that is never utilized.
**
**  Specific issues include:
**  - Magic Numbers: Direct usage of constant values in methods instead of using named constants or variables.
**  - Synchronous File Reading: Blocking the main thread with synchronous file operations.
**  - Redundant Calculations: Performing calculations that yield the same result each time.
**  - Inefficient Collection Usage: Inefficiently handling collections, including unnecessary checks.
**  - Dead Code: Code that is never executed, making the codebase harder to maintain.
**  - Predictable Random Number Generation: Using the default Random class which may lead to predictable random values.
**  - Exception Detail Exposure: Exposing internal exception messages which can be a security risk.
**  - Unnecessary Async Keyword: Using the async keyword without performing any asynchronous operations.
**  - Unused Variables: Declaring variables that are never used.
**
**  This class serves as an educational tool to help developers identify and understand these common issues.
**
**  Change History
**
**  WHEN            WHO          WHAT
**---------------------------------------------------------------------------------
**  2024-06-05      RGARLIT      CREATED CLASS TO DEMONSTRATE COMMON CODING ISSUES
***********************************************************************************/
namespace DVDStore.Web.MVC.Common.InduceProblems
{
    public class ProblematicCode
    {
        private string unusedVariable = "Never used";

        // Using a magic number directly in methods
        public void DisplayNumber()
        {
            Console.WriteLine("Magic Number: " + 42);
        }

        // Synchronous file reading might block the main thread
        public void ReadFileContent()
        {
            // CWE-459: Incomplete Cleanup
            StreamReader reader = new StreamReader("example.txt");
            string content = reader.ReadToEnd(); // Potential for blocking the main thread
            Console.WriteLine(content);
        }

        // Method that always returns the same value, which could be simplified
        public int AddNumbers()
        {
            int a = 5;
            int b = 10;
            int result = a + b; // Redundant calculation
            return result;
        }

        // Inefficient use of collections
        public void ProcessList()
        {
            List<int> numbers = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                numbers.Add(i);
            }

            // Unnecessary check that could be simplified or removed
            if (numbers.Count > 0)   // CWE-571: Expression is Always True
            {
                foreach (var number in numbers)
                {
                    Console.WriteLine(number);
                }
            }
        }

        // Example of dead code
        public void UnusedMethod()
        {
            Console.WriteLine("This method is never called.");
        }

        // Unsecured random number
        public void DisplayRandomNumber()
        {
            Random random = new Random();
            Console.WriteLine("Random number: " + random.Next(1, 100)); // Predictable random number
        }

        // Potential security vulnerability due to exception details being exposed
        public void CheckFileExists(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    Console.WriteLine("File exists.");
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error checking file: " + ex.Message); // Exposing exception details
            }
        }

        // Async method without async operations
        public async Task<int> GetValueAsync()
        {
            return 123; // Should not be async
        }
    }
}
