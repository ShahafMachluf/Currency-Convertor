using System;

namespace CurrencyConvertor.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("ERROR. file name not received");
            }
            else
            {
                UIManager.RunConvertor(args[0]);
            }
        }
    }
}
