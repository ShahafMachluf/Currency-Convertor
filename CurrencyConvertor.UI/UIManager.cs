using System;
using System.Collections.Generic;
using System.IO;
using CurrencyConvertor.Engine;

namespace CurrencyConvertor.UI
{
    public static class UIManager
    {
        public static void RunConvertor(string i_FileName)
        {
            string convertTo;
            Currency currency = new Currency();
            string[] lines = File.ReadAllLines(i_FileName);

            try
            {
                List<string> supportedCurrencies = currency.GetConvertibleTypes();
                if (!supportedCurrencies.Contains(lines[0]) || !supportedCurrencies.Contains(lines[1]))
                {
                    Console.WriteLine("ERROR. currency type: " + lines[0] + " or " + lines[1] + " is not supported by the API.");
                }
                else
                {
                    currency.SetType(lines[0]);
                    convertTo = lines[1];
                    for (int i = 2; i < lines.Length; i++)
                    {

                        currency.SetValue(float.Parse(lines[i]));
                        Console.WriteLine(currency.Convert(convertTo));
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("There was an error at the API call.");
                Console.WriteLine("Message: " + exp.Message);
            }
        }
    }
}
