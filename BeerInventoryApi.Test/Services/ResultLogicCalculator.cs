
using BeerInventoryServices.Helper;
using BeerData.DataObjects;
using BeerData.Interfaces;
using Serilog;
using Serilog.Configuration;
using Serilog.Extensions;
using BeerInventoryApi.Logger;

namespace BeerInventoryApi.Test.Services
{
    [TestClass]
    public class ResultLogicCalculatorTest
    {
        private  List<IBeerItem> _goodItems = new List<IBeerItem> ();
        private  List<IBeerItem> _badItems = new List<IBeerItem>();

       
        public void ClassInitialize()
        {
            //Inititializing logger for tests
            LogService.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

            List<Article> articles = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 17.99, PricePerUnitText = "(1,80 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" },
                                                        new Article { Id = 2, Image = "SomeString", Price = 17.99, PricePerUnitText = "(1,40 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" } };


            BeerItem beer = new BeerItem { Id = 1 ,BrandName = "SomeBrand", DescriptionText = "SomeString" , Articles = articles, Name = "SomeName"};

            List<Article> articles1 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 17.99, PricePerUnitText = "(5,80 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" },
                                                          new Article { Id = 2, Image = "SomeString", Price = 16.99, PricePerUnitText = "(0,80 €/Liter)", ShortDescription = "13 x 0,5L (Glas)" }};
            BeerItem beer1 = new BeerItem { Id = 2, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles1, Name = "SomeName" };


            List<Article> articles2 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 10, PricePerUnitText = "(1,80 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" },
                                                          new Article{ Id = 2, Image = "SomeString", Price = 7.99, PricePerUnitText = "(2,80 €/Liter)", ShortDescription = "9 x 0,5L (Glas)" }};
            BeerItem beer2 = new BeerItem { Id = 3, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles2, Name = "SomeName" };


            List<Article> articles3 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 11.99, PricePerUnitText = "(1,80 €/Liter)", ShortDescription = "30 x 0,5L (Glas)" } };
            BeerItem beer3 = new BeerItem { Id = 4, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles3, Name = "SomeName" };


            List<Article> articles4 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 33, PricePerUnitText = "(5,8 €/Liter)", ShortDescription = "30 x 0,5L (Glas)" } };
            BeerItem beer4 = new BeerItem { Id = 5, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles4, Name = "SomeName" };

            _goodItems.Add(beer1);
            _goodItems.Add(beer2);
            _goodItems.Add(beer3);
            _goodItems.Add(beer4);
            _goodItems.Add(beer);
        }

        [TestMethod]
        public void TestGetBottlesPositive()
        {
            ClassInitialize();

            var resultBeers = ResultBeerLogicCalculator.GetMostBottlesBeer(_goodItems);
            var expectedResult = new List<IResultBeer> { new ResultBeer { BrandName = "SomeBrand", Id = 4, Caption = "MostBottles", Name = "SomeName", Price = 11.99, PricePerUnit = 1.8, Quantity = 30, ResultId = 1 },
                                                         new ResultBeer { BrandName = "SomeBrand", Id = 5, Caption = "MostBottles", Name = "SomeName", Price = 33, PricePerUnit = 5.8, Quantity = 30, ResultId = 1 } };

            Assert.AreEqual(resultBeers[0].Quantity, expectedResult[0].Quantity);

        }

        [TestMethod]
        public void TestGetMostExpensive()
        {
            ClassInitialize();

            var resultBeers = ResultBeerLogicCalculator.GetMostExpensiveBeer(_goodItems);
            var expectedResult = new List<IResultBeer> { new ResultBeer { BrandName = "SomeBrand", Id = 2, Caption = "MostBottles", Name = "SomeName", Price = 17.99, PricePerUnit = 5.8, Quantity = 20, ResultId = 1 },
                                                         new ResultBeer { BrandName = "SomeBrand", Id = 5, Caption = "MostBottles", Name = "SomeName", Price = 33, PricePerUnit = 5.8, Quantity = 30, ResultId = 1 } };

            Assert.AreEqual(resultBeers[0].PricePerUnit, expectedResult[0].PricePerUnit);

        }

        [TestMethod]
        public void TestGetMostCheap()
        {
            ClassInitialize();

            var resultBeers = ResultBeerLogicCalculator.GetMostCheapBeer(_goodItems);
            var expectedResult = new List<IResultBeer> { new ResultBeer { BrandName = "SomeBrand", Id = 2, Caption = "MostBottles", Name = "SomeName", Price = 16.99, PricePerUnit = 0.8, Quantity = 13, ResultId = 2 } };

            Assert.AreEqual(resultBeers[0].PricePerUnit, expectedResult[0].PricePerUnit);

        }

        [TestMethod]
        [DataRow(17.99, 3)]
        [DataRow(16.99, 1)]
        [DataRow(33, 1)]

        public void TestGetexactPricePositive(double price, int count)
        {
            ClassInitialize();

            var resultBeers = ResultBeerLogicCalculator.GetBeersWithExactPrice(_goodItems, price);
            Assert.AreEqual(resultBeers.Count, count);
            Assert.AreEqual(resultBeers[0].Price, price);

        }

        [TestMethod]
        [DataRow(1, 0)]


        public void TestGetexactPriceNegative(double price, int count)
        {
            ClassInitialize();

            var resultBeers = ResultBeerLogicCalculator.GetBeersWithExactPrice(_goodItems, price);
            Assert.IsNull(resultBeers);

        }


        private bool AreListsEqual(List<IResultBeer> input, List<IResultBeer> output)
        {
            return input.SequenceEqual(output);
        }

        
    }

}
