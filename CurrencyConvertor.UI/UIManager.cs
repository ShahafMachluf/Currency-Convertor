using System;
using System.Collections.Generic;
using CurrencyConvertor.Engine;

namespace CurrencyConvertor.UI
{
    public static class UIManager
    {
        public static void RunConvertor(string i_FileName)
        {
            string convertTo;
            CurrencyConvertor.Engine.IConvertible currency = new Currency();
            string[] lines = System.IO.File.ReadAllLines(i_FileName);
            try
            {
                List<string> supportedCurrencies = currency.GetConvertibleTypes();
                if (!Currency.IsCurrenciesTypesSupportedByAPI(supportedCurrencies, lines[0], lines[1]))
                {
                    Console.WriteLine("ERROR.currency type: " + lines[0] + " or " + lines[1] + " is not supported by the API.");
                }
                else
                {
                    currency.SetType(lines[0]);
                    convertTo = lines[1];
                    for (int i = 2; i < lines.Length; i++)
                    {

                        currency.SetValue(lines[i]);
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
