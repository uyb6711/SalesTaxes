using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SalesTaxes.Model.Utils
{
    /*
     * StringUtils organies a list of method to check condition of given string
     */ 
    class StringUtils
    {
        /*
         * Capitalize only first letter of given string 
         */
        public static string CapitalizeFirstLetter(string str)
        {
            char[] word = str.ToCharArray();
            word[0] = char.ToUpper(word[0]);

            return new string(word);
        }

        /*
         * Check if given string is a format of dollar currency.
         */ 
        public static decimal ValidateCurrency(string str)
        {
            while (!IsCurrency(str))
            {
                Console.Error.WriteLine("Invalid Input. Please enter a valid input ($ currency)");
                str = Console.ReadLine();
            }

            return decimal.Parse(str);
        }

        /*
         * Check if given string is a valid answer for yes or no question
         */
        public static string ValidateYesOrNo(string str)
        {
            while (!IsYesOrNo(str))
            {
                Console.Error.WriteLine("Invalid Input. Please enter a valid input (y/n)");
                str = Console.ReadLine();
            }

            return str;
        }

        /*
         * Check if given string is a valid number
         */
        public static int ValidateNumber(string str)
        {
            while (!IsNumber(str))
            {
                Console.Error.WriteLine("Invalid Input. Please enter a valid number (number has to be greater than 0)");
                str = Console.ReadLine();
            }
            return int.Parse(str);
        }

        public static bool IsImported(string str)
        {
            return str.Equals("y") ? true : false;
        }

        private static bool IsYesOrNo(string str)
        {
            str = str.ToLower();
            return str.Equals("y") || str.Equals("n") ? true : false;
        }

        private static bool IsCurrency(string str)
        {
            return decimal.TryParse(str, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"), out var currency);
        }

        private static bool IsNumber(string str)
        {
            int.TryParse(str, out var number);
            return number > 0 ? true : false;
        }


        

    }
}
