using System.Globalization;
using System.Text.RegularExpressions;
using BeerInventoryApi.Logger;

namespace BeerInventoryHelper
{
    //Reference for the type of function user wants
    public enum TypeOfResult
    {
        MostExpensive,
        MostCheap,
        ExpensiveCheap,
        ExactPriced,
        MostBottles,
        All
    }


    public static class Helper
    {

        private static bool IsPriceInEuroPerLiterFormat(string text)
        {
            try
            {
                string pattern = @"€\/Liter";
                Match m = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                LogService.LogError("Helper", "IsPriceInEuroPerLiterFormat", "Exception thrown");
                throw;
            }

            return false;
        }

        private static bool IsShortDescriptionValid(string text)
        {
            try
            {
                string pattern = @"^\d*\s*x\s*\d*,*\d*\s*L\s*\(Glas\)\s*$";
                Match m = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
                return m.Success;
            }
            catch (Exception)
            {
                LogService.LogError("Helper", "IsShortDescriptionValid", "Exception thrown");
                return false;
            }
        }


        /// <summary>
        /// Expected format : 20 x 0,5L (Glas) 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static int? ExtractBottleCount(string text)
        {
            try
            {
                string pattern = @"\d*\s*x";
                Match m = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    return Int32.Parse(m.Value.Replace("x", string.Empty).Trim());
                }
            }
            catch (Exception)
            {
                LogService.LogError("Helper", "ExtractBottleCount", "Exception thrown");
                throw;
            }

            return null;
        }

        /// <summary>
        /// Expected format is (1,80 €/Liter)
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static double ExtractPrice(string text)
        {
            try
            {
                string pattern = @"\d,?\d?";
                Match m = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
                if (m.Success)
                {
                    return double.Parse(m.Value, CultureInfo.GetCultureInfo("de-DE"));
                }
            }
            catch (Exception)
            {
                LogService.LogError("Helper", "ExtractPrice", "Exception thrown");
                throw;
            }

            return 0;
        }


        /// <summary>
        /// Parses shortDescription from input json of article in the format 'quantity int x n int glas' to quantity
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static int GetBottleCount(string description)
        {
            try
            {
                if (string.IsNullOrEmpty(description) == false)
                {
                    if (IsShortDescriptionValid(description) == true)
                    {
                        return ExtractBottleCount(description) ?? 0;
                    }
                    LogService.LogError("Helper", "GetBottleCount", $"ShortDescription {description} not in expected int x int n glas format");

                }
            }
            catch
            {
                LogService.LogError("Helper", "GetBottleCount", $"ShortDescription {description} not in expected int x int n glas format");
                throw;
            }
            return 0;

        }

        /// <summary>
        /// Parses pricePerUnitText from input json of article in the format 'double price in euro/Litre' to price
        /// </summary>
        /// <param name="pricePerUnitText"> </param>
        /// <returns></returns>
        public static double GetUnitPrice(string pricePerUnitText)
        {
            try
            {
                if (string.IsNullOrEmpty(pricePerUnitText) == false)
                {
                    if (IsPriceInEuroPerLiterFormat(pricePerUnitText))
                    {
                        return ExtractPrice(pricePerUnitText);
                    }
                    LogService.LogError("Helper", "GetUnitPrice", $"PricePerUnitText {pricePerUnitText} not in expected euro/Lite format");
                }
                else
                {
                    LogService.LogError("Helper", "GetUnitPrice", "PricePerUnitText is null or empty");
                    return 0;
                }
            }
            catch(Exception)
            {
                LogService.LogError("Helper", "ExtractPrice", "Exception thrown");
                throw;
            }
            return 0;
        }


    }
}