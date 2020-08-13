using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FibonacciProductCalculator
{
    public static class Helper
    {
        public  static readonly string ExceptionMessage = "Exception Occured in method: {0} exception message: {1}";
        /// <summary>
        /// return product of numbers in fibonacci series
        /// upto entered limitingNumber
        /// </summary>
        /// <param name="limitingNumber"></param>
        /// <returns>int</returns>
        public static int ProcessFibonacciSeries(int limitingNumber, ILogger _logger)
        {
            int fibonacciProductSeries = 1;
            try
            {
                int firstNumber = 1, secondNumber = 1, calculatedValue;                
                string fibonacciSeries = firstNumber + "," + secondNumber;

                for (int i = 2; i < limitingNumber; ++i)
                {
                    calculatedValue = firstNumber + secondNumber;
                    fibonacciSeries = fibonacciSeries + "," + calculatedValue;
                    fibonacciProductSeries *= calculatedValue;
                    firstNumber = secondNumber;
                    secondNumber = calculatedValue;
                }

                Console.WriteLine("\n" + $"Fibonacci Number Series upto {limitingNumber}: {fibonacciSeries}");                
                Console.WriteLine("\n" + $"Product of Fibonacci Numbers upto {limitingNumber}: {fibonacciProductSeries}");                
            }
            catch(Exception ex)
            {
                _logger.LogError(string.Format(ExceptionMessage, "ProcessFibonacciSeries", ex.Message));
            }
            return fibonacciProductSeries;
        }

        /// <summary>
        /// Check if user wants to conitnue
        /// </summary>
        /// <returns>bool</returns>
        public static bool PromptForContinuing()
        {
            Console.WriteLine("\n" + "Do you wish to continue? Please enter " + "Y" + " for Yes " + "N" + " for No");
            var isContinuedByUser = Console.ReadKey();
            if ("y" == isContinuedByUser.KeyChar.ToString().ToLower())
                return true;
            else return false;
        }

        /// <summary>
        /// Ask user for limitingNumber for Fibonacci Series
        /// </summary>
        /// <returns>string</returns>
        public static string PromptForValidInput()
        {
            Console.WriteLine("Please enter a limiting number for Fibonacci Series, number must be greater than zero.");
            return Console.ReadLine();
        }


        /// <summary>
        /// Ask user for limitingNumber for Fibonacci Series
        /// </summary>
        /// <returns>int</returns>
        public static int CheckForRetry(ILogger _logger)
        {
            int result =  0;
            try
            {
                Console.WriteLine("\n" + "Please enter a limiting number for Fibonacci Series, number must be greater than zero.");
                string userInput = Console.ReadLine();
                int limitingNumber = Convert.ToInt32(userInput);
                return ProcessFibonacciSeries(limitingNumber, _logger);              

            }
            catch(Exception ex)
            {
                _logger.LogError(string.Format(ExceptionMessage, "ProcessFibonacciSeries", ex.Message));
                Console.WriteLine("Please Enter Valid Input!!");
            }
            return result;
        }


        /// <summary>
        /// Writes final series of Product of Numbers from Fibonacci Series
        /// </summary>
        /// <param name="product"></param>
        /// <param name="finalSeriesOfProduct"></param>
        public static void FinalResult(int product, ILogger _logger, ref string finalSeriesOfProduct)
        {
            try
            {
                if (string.IsNullOrEmpty(finalSeriesOfProduct))
                {
                    finalSeriesOfProduct = finalSeriesOfProduct + product;
                }
                else
                {
                    finalSeriesOfProduct = finalSeriesOfProduct + "," + product;
                }
            }

            catch(Exception ex)
            {
                _logger.LogError(string.Format(ExceptionMessage, "FinalResult", ex.Message));
            }
            Console.WriteLine("\n" + "Series of Product of Fibonacci Numbers: " +finalSeriesOfProduct);
        }
    }
}
