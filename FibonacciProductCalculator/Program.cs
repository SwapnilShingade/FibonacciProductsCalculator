using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FibonacciProductCalculator
{
    class Program
    {
        public static ILogger<Program> _logger;
        static void Main(string[] args)
        {
            try
            {
                // Add Service Collection to configure Configuration Services
                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection);
                IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
                _logger = serviceProvider.GetService<ILogger<Program>>();                

                Console.WriteLine("***Welcome to Fibonacci Series Product Calculator!!***");
                int limitingNumber, productOfAll;
                string userInput = string.Empty;
                string finalSeriesOfProduct = string.Empty;
                var isContinued = false;                

                // Check if user enters valid value
                do
                {
                    userInput = Helper.PromptForValidInput();
                    limitingNumber = Convert.ToInt32(userInput);
                }
                while (limitingNumber <= 0);

                productOfAll = Helper.ProcessFibonacciSeries(limitingNumber, _logger);                
                finalSeriesOfProduct = "" + productOfAll;

                // Check if user wants to enter number again
                do
                {
                    if (isContinued = Helper.PromptForContinuing())
                    {
                        productOfAll = Helper.CheckForRetry(_logger);
                    }
                        
                   Helper.FinalResult(productOfAll, _logger, ref finalSeriesOfProduct);                     
                    
                }
                while (isContinued);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception Occured: " + ex.Message);
                Console.WriteLine("Exception Occured " +ex.Message);
            }
        }

        /// <summary>
        /// Configure Logger
        /// </summary>
        /// <param name="serviceCollection"></param>
        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging(configure => configure.AddConsole())
               .AddTransient<Program>();
        }
    }
}
