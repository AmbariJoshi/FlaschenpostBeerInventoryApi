//This class has methods to clculate logic for getting beers based on user choice
using BeerData.Interfaces;
using BeerData.DataObjects;
using BeerInventoryHelper;
using BeerInventoryApi.Logger;


namespace BeerInventoryServices.Helper
{
    public static class ResultBeerLogicCalculator
    {

        private const string className = "ResultBeerLogicCalculator";
        /// <summary>
        /// This method returns most expensive beer
        /// </summary>
        public static List<IResultBeer>? GetMostExpensiveBeer(List<IBeerItem> inputBeers)
        {
            try
            {
                //Following 2 steps give maximum value of PricePerUnit 
                var beerItemMax = inputBeers.Aggregate((e1, e2) => (e1.Articles.Max(p1 => p1.PricePerUnit) > e2.Articles.Max(p2 => p2.PricePerUnit)) ? e1 : e2);
                var resultMaxArticle = beerItemMax.Articles.MaxBy(p => p.PricePerUnit);

                if (beerItemMax != null && resultMaxArticle != null)
                {
                    List<IResultBeer> costliestBeers = new List<IResultBeer>();

                    //Get all beers with PricePerUnit equal to maximun PricePerUnit calculated above
                    var exactPriced = (from beer in inputBeers
                                       from Article in beer.Articles
                                       where Article.PricePerUnit == resultMaxArticle.PricePerUnit
                                       select beer).GroupBy(x => x.Id).Select(z => z.First());

                    if (exactPriced != null && exactPriced.Count() > 0)
                    {

                        foreach (var beer in exactPriced)
                        {
                            /*
                             * Each beer item can have multiple articles
                             * We need only articles with PricePerUnit as exact value of max PricePerUnit
                             */
                            var article = from Article in beer.Articles
                                          where Article.PricePerUnit == resultMaxArticle.PricePerUnit
                                          select Article;

                            if (article != null)
                            {
                                foreach (var articleItem in article)
                                {
                                    //Combine beer and article data to prepare output object
                                    IResultBeer resultBeer = GetResultObject(TypeOfResult.MostExpensive.ToString(), beer, articleItem);
                                    costliestBeers.Add(resultBeer);
                                }
                            }
                        }
                    }
                    return costliestBeers;
                }
            }
            catch
            {
                LogService.LogError(className, "GetMostExpensiveBeer", "Error in gettng most expensive beer");
                throw;
            }

            return null;
        }

        /// <summary>
        /// This method returns most cheap beer
        /// </summary>
        public static List<IResultBeer>? GetMostCheapBeer(List<IBeerItem> inputBeers)
        {
            try
            {
                //Following 2 steps give min value of PricePerUnit 

                var beerItemMin = inputBeers.Aggregate((e1, e2) => (e1.Articles.Min(p1 => p1.PricePerUnit) < e2.Articles.Min(p2 => p2.PricePerUnit)) ? e1 : e2);
                var resultMinArticle = beerItemMin.Articles.MinBy(p => p.PricePerUnit);

                if (beerItemMin != null && beerItemMin.Articles != null && beerItemMin.Articles.Count > 0)
                {

                    if (beerItemMin != null && resultMinArticle != null)
                    {
                        List<IResultBeer> cheapestBeers = new List<IResultBeer>();

                        var exactPriced = (from beer in inputBeers
                                           from Article in beer.Articles
                                           where Article.PricePerUnit == resultMinArticle.PricePerUnit
                                           select beer).GroupBy(x => x.Id).Select(z => z.First());

                        if (exactPriced != null && exactPriced.Count() > 0)
                        {

                            /*
                             * Each beer item can have multiple articles
                             * We need only articles with PricePerUnit as exact value of min PricePerUnit
                             */
                            foreach (var beer in exactPriced)
                            {
                                var article = from Article in beer.Articles
                                              where Article.PricePerUnit == resultMinArticle.PricePerUnit
                                              select Article;

                                if (article != null)
                                {
                                    foreach (var articleItem in article)
                                    {
                                        //Combine beer and article data to prepare output object
                                        IResultBeer resultBeer = GetResultObject(TypeOfResult.MostCheap.ToString(), beer, articleItem);
                                        cheapestBeers.Add(resultBeer);
                                    }
                                }
                            }
                        }
                        return cheapestBeers;
                    }
                }
            }
            catch
            {
                LogService.LogError(className, "GetMostCheapBeer", "Exception in gettng most cheap beer");

                throw;
            }
            return null;
        }


        /// <summary>
        /// This method prepares json output format result
        /// </summary>
        private static IResultBeer GetResultObject(string caption, IBeerItem beerItem, IArticle article)
        {
            return new ResultBeer
            {
                Caption = caption,
                Id = beerItem.Id,
                Name = beerItem.Name,
                BrandName = beerItem.BrandName,
                ResultId = article.Id,
                Price = article.Price,
                Quantity = article.Quantity,
                PricePerUnit = article.PricePerUnit
            };
        }

        /// <summary>
        /// This method returns most cheap and most expensive beer
        /// </summary>
        public static List<IResultBeer>? GetMostExpensiveCheapBeer(List<IBeerItem> inputBeers)
        {
            List<IResultBeer> expensiveCheapBeerList = new List<IResultBeer>();

            try
            {
                List<IResultBeer> mostExpensiveBeer = GetMostExpensiveBeer(inputBeers);
                if (mostExpensiveBeer != null)
                {
                    expensiveCheapBeerList.AddRange(mostExpensiveBeer);
                }
            }
            catch(Exception ex1)
            {
                LogService.LogError(className, "GetMostExpensiveCheapBeer", $"Exception caught while calculating most expensive beer:{ex1.Message}");
            }

            try
            {
                List<IResultBeer> mostCheapBeer = GetMostCheapBeer(inputBeers);


                if (mostCheapBeer != null)
                {
                    expensiveCheapBeerList.AddRange(mostCheapBeer);

                }
            }
            catch (Exception ex2)
            {
                LogService.LogError(className, "GetMostExpensiveCheapBeer", $"Exception caught while calculating most cheap beer:{ex2.Message}");

            }
            if (expensiveCheapBeerList.Count > 0)
                return expensiveCheapBeerList;

            return null;
        }


        /// <summary>
        /// This method returns most bottles beer
        /// </summary>
        public static List<IResultBeer>? GetMostBottlesBeer(List<IBeerItem> inputBeers)
        {
            try
            {
                //Following 2 steps give max value of bottles 

                var beerMostBottles = inputBeers.Aggregate((e1, e2) => (e1.Articles.Max(p1 => p1.Quantity) > e2.Articles.Max(p2 => p2.Quantity)) ? e1 : e2);
                var articleMostBottles = beerMostBottles.Articles.MaxBy(p => p.Quantity);

                if (beerMostBottles != null && articleMostBottles != null)
                {
                    List<IResultBeer> resultBeers = new List<IResultBeer>();

                    var mostBottles = (from beer in inputBeers
                                       from Article in beer.Articles
                                       where Article.Quantity == articleMostBottles.Quantity
                                       select beer).GroupBy(x => x.Id).Select(z => z.First());

                    if (mostBottles != null && mostBottles.Count() > 0)
                    {
                        /*
                            * Each beer item can have multiple articles
                            * We need only articles with Quantity as exact value of most bottles quantity
                            */
                        foreach (var beer in mostBottles)
                        {
                            var article = from Article in beer.Articles
                                          where Article.Quantity == articleMostBottles.Quantity
                                          select Article;

                            if (article != null)
                            {
                                foreach (var articleItem in article)
                                {
                                    IResultBeer resultBeer = GetResultObject(TypeOfResult.MostBottles.ToString(), beer, articleItem);
                                    resultBeers.Add(resultBeer);
                                }
                            }
                        }
                    }
                    if (resultBeers.Count > 0)
                        return resultBeers.OrderBy(x => x.PricePerUnit).ToList();
                    else
                    {
                        LogService.LogError(className, "GetMostBottlesBeer", "No beer found most bottles");
                    }
                }
            }
            catch
            {
                LogService.LogError(className, "GetMostBottlesBeer", "Exception in GetMostBottlesBeers");
                throw;
            }
            return null;

        }


        /// <summary>
        /// This method returns beers with exact price
        /// </summary>

        public static List<IResultBeer>? GetBeersWithExactPrice(List<IBeerItem> inputBeers, double price)
        {

            try
            {
                List<IResultBeer> exactPricedBeers = new List<IResultBeer>();

                //Get beers with articles having exact price as Price
                var exactPriced = (from beer in inputBeers
                                   from Article in beer.Articles
                                   where Article.Price == price
                                   select beer).GroupBy(x => x.Id).Select(z => z.First());

                if (exactPriced != null && exactPriced.Count() > 0)
                {
                    foreach (var beer in exactPriced)
                    {
                        /*
                            * Each beer item can have multiple articles
                            * We need only articles with Price as exact value of price entered by user
                            */
                        var article = from Article in beer.Articles
                                      where Article.Price == price
                                      select Article;

                        if (article != null)
                        {
                            foreach (var articleItem in article)
                            {
                                IResultBeer resultBeer = GetResultObject(TypeOfResult.ExactPriced + "_" + price.ToString(), beer, articleItem);
                                exactPricedBeers.Add(resultBeer);
                            }
                        }
                    }
                }
                if (exactPricedBeers.Count > 0)
                    return exactPricedBeers.OrderBy(x => x.PricePerUnit).ToList();
                else
                    LogService.LogError(className, "GetBeersWithExactPrice", $"No beer found with price {price}");
            }
            catch (Exception ex)
            {
                LogService.LogError(className, "GetBeersWithExactPrice", $"Exception finding beer with price {price}");
                throw ex;
            }

             return null;
            }
        }
    }

