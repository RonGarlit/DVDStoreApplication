// A new namespace `DVDStore.Web.MVC.Common.InduceProblems` has been added. This includes a
// class `ProblematicCode` with several methods demonstrating common coding issues such as
// usage of magic numbers, synchronous file reading, redundant calculations, inefficient
// collection usage, dead code, predictable random number generation, exposure of exception
// details, and unnecessary async keyword usage. Also added an unused private string variable.


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
