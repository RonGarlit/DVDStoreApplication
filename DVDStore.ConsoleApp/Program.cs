// /**********************************************************************************
// **
// **  ConsoleAppPrototype v1.1
// **
// **  Copyright 2024
// **  Developed by: Ronald A. Garlit
// **
// ***********************************************************************************
// **
// **  FileName: Program.cs (ConsoleAppPrototype)
// **  Version: 0.1
// **  Author: Ronald A. Garlit
// **
// **  Description: This is my prototype console application for creating and 
// **  executing .NET console application. Based on my Visual Studio 2019 version.
// **
// **  Prototype console application for executing your own .NET console
// **  application.
// **
// **  Change History
// **
// **  WHEN         WHO         WHAT
// **---------------------------------------------------------------------------------
// **  2024-02-14   RGARLIT     STARTED DEVELOPMENT
// ***********************************************************************************/
using System;
using System.Threading;

namespace ConsoleAppPrototype
{
    internal class Program
    {
        /// <summary>
        ///     Main is changed from a void (without returnvalue) to a method returning an int
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static int Main(string[] args)
        {

            //*****************************************************************
            //  This is commented out and is used to demonstrate calling with
            //  NO parameters
            //  NOTE: You have to comment out the section below that does the
            //  CheckArgs stuff
            //*****************************************************************
            //Console.WriteLine($"Running in console...");
            //Console.WriteLine($"Sleeping for thirty seconds");
            //Thread.Sleep(30000);
            //Console.WriteLine($"Bye!");
            // If the application has finished successfully, return 0
            // return 0;
            //*****************************************************************


            //*****************************************************************
            //  This is the main section that assumes the setup and use of
            //  parameters. When you are using parameters you would edit this
            //  section accordingly for the number of parameters used/needed.
            //  NOTE:  This is that the structure of the try/catch has all the
            //  base return codes and setup for logging back to the SSIS
            //  package calling it.
            //*****************************************************************
            if (!CheckArgs(args))
            {
                return -2;
            }

            try
            {
                var param1 = string.Empty;
                var param2 = string.Empty;
                var param3 = string.Empty;
                var param4 = true;

                ParseArgs(args, out param1, out param2, out param3, out param4);

                // TODO Add your own application logic here.

                // For the demo only. You can remove it:
                Console.WriteLine("(.. For debug only .. let's see what we've got ..)");
                Console.WriteLine("===============================================");
                Console.WriteLine("The following parameter values have been provided:");
                Console.WriteLine("===============================================");
                Console.WriteLine("param1: {0}", param1);
                Console.WriteLine("param2: {0}", param2);
                Console.WriteLine("param3: {0}", param3);
                Console.WriteLine("param4: {0}", param4.ToString().ToLower());

                Thread.Sleep(10000); // 10 seconds to read the output.

                // Force an error when param1 has a certain value.
                // This is for the demo only. You can remove it:
                if (param1 == "testerror")
                {
                    var zero = 0;
                    var i = 1 / zero;
                }

                // If the application has finished successfully, return 0
                return 0;
            }

            catch (Exception ex)
            {
                // Write the error to the StandardError output.
                Console.Error.WriteLine(ex.ToString());
                // Optionally write the error also to the StandardOutput output.
                Console.WriteLine(ex.ToString());

                // Get the errorcode of the exception.
                // If it would be 0 (the code for success), just return -1. 
                // Otherwise return the real error code.
                var errorCode = ex.HResult;
                return errorCode == 0 ? -1 : errorCode;
            }
        }

        /// <summary>
        ///     Checks if the application is called with command line parameters.
        ///     If not a message is shown on the command line for 30 seconds.
        ///     A more detailed check of the command line arguments is done in private static void ParseArgs.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static bool CheckArgs(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("(.. You have 30 seconds to read this help text ..)");
                Console.Error.WriteLine("===============================================");
                Console.Error.WriteLine("This application needs command line parameters.");
                Console.Error.WriteLine("===============================================");
                Console.Error.WriteLine("Required parameters:");
                Console.Error.WriteLine("-param1, followed by the value of parameter 1");
                Console.Error.WriteLine("-param2, followed by the value of parameter 2");
                Console.Error.WriteLine("-param3, followed by the value of parameter 3");
                Console.Error.WriteLine("Optional parameters:");
                Console.Error.WriteLine(
                    "-param4, followed by the value of parameter 4. If not value provided, the default value 'true' will be used.");
                Console.Error.WriteLine("===============================================");
                Console.Error.WriteLine("Example of use:");
                Console.Error.WriteLine(
                    "SSISConsoleAppPrototype.exe -param1 Value 1 with space -param2 Value2WithoutSpace -param3 Value 3 with space again -param4 false");
                // Use Sleep, so that:
                // - if ran interactively, give time to read the message.
                // - if ran from SSIS Package, prevent that console application stays open and waits for user input (would be the case when using Console.ReadKey();)
                Thread.Sleep(30000);
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Parses the command line parameters.
        ///     In a real console application you would give your parameters more
        ///     meaningful names instead of numbering them as param1, param2, etcetera.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="param1">Output parameter with value for param1.</param>
        /// <param name="param2">Output parameter with value for param2.</param>
        /// <param name="param3">Output parameter with value for param3.</param>
        /// <param name="param4">Output parameter with value for param4.</param>
        private static void ParseArgs(string[] args, out string param1, out string param2, out string param3,
            out bool param4)
        {
            // Set the parameter values to default values first.
            param1 = string.Empty;
            param2 = string.Empty;
            param3 = string.Empty;
            param4 = true;

            // In case a parameter value contains spaces, it is spread over multiple 
            // elements in the args[] array. In this case we use lastArg to concatenate
            // these different parts of the value to a single value.
            var lastArg = string.Empty;
            // If the next parameter is not found, the value must be of lastArg.
            var foundNext = false;

            // paramCount is used to check that all required parameter values are provided.
            var paramCount = 0;

            // Loop through the args[] array. 
            for (var i = 0; i <= args.GetUpperBound(0); i++)
            {
                foundNext = false;

                // Create an if statement for each parameter that is provided on the command line.
                if (args[i].ToLower() == "-param1")
                {
                    i++;
                    paramCount++;
                    foundNext = true;
                    lastArg = "-param1";
                    // Check if there is a value, otherwise keep the default.
                    if (i > args.GetUpperBound(0))
                    {
                        break;
                    }

                    param1 = args[i];
                }

                if (args[i].ToLower() == "-param2")
                {
                    i++;
                    paramCount++;
                    foundNext = true;
                    lastArg = "-param2";
                    // Check if there is a value, otherwise keep the default.
                    if (i > args.GetUpperBound(0))
                    {
                        break;
                    }

                    param2 = args[i];
                }

                if (args[i].ToLower() == "-param3")
                {
                    i++;
                    paramCount++;
                    foundNext = true;
                    lastArg = "-param3";
                    if (i > args.GetUpperBound(0))
                    {
                        break;
                    }

                    param3 = args[i];
                }

                if (args[i].ToLower() == "-param4")
                {
                    i++;
                    // Optional parameter, so do not count it! paramCount++;
                    foundNext = true;
                    lastArg = "-param4";
                    // Check if there is a value, otherwise keep the default.
                    if (i > args.GetUpperBound(0))
                    {
                        break;
                    }

                    param4 = args[i].ToLower() == "true" ? true : false;
                }

                if (!foundNext)
                {
                    // In case a parameter value contains spaces, it is spread over multiple elements in the args[] array.
                    // In this case we use lastArg to concatenate these different parts of the value to a single value.
                    switch (lastArg)
                    {
                        case "-param1":
                            param1 = string.Format("{0} {1}", param1, args[i]);
                            break;
                        case "-param2":
                            param2 = string.Format("{0} {1}", param2, args[i]);
                            break;
                        case "-param3":
                            param3 = string.Format("{0} {1}", param3, args[i]);
                            break;
                            // -param4 is not listed here because it is a boolean
                            // so spaces in the value should not occur.
                    }
                }
            }

            if (paramCount < 3)
            {
                var message = string.Format("Invalid arguments provided: {0}", string.Join(" ", args));
                throw new ArgumentException(message);
            }
        }
    }
}