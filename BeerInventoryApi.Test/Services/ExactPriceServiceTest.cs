using BeerData.DataObjects;
using BeerData.Interfaces;
using BeerInventoryServices.Services;
using BeerInventoryServices.Services.Interfaces;
using Moq;
using BeerInventoryApi.Logger;
using Serilog;
using Serilog.Configuration;
using Serilog.Extensions;

namespace BeerInventoryApi.Test.Services
{
    [TestClass]
    public class ExactPriceServiceTest
    {

        private List<IBeerItem> goodItems = new List<IBeerItem>();
        private List<Article> articles;
        private BeerItem beer;
        private List<Article> articles1;
        private BeerItem beer1;
        private List<Article> articles2;
        private BeerItem beer2;
        private List<Article> articles3;
        private BeerItem beer3;
        private List<Article> articles4;
        private BeerItem beer4;
        private string[] parameters = new string[2]; 

        [TestInitialize]
        public void ClassInitialize()
        {
            //Inititializing logger for tests
            LogService.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
            articles = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 17.99, PricePerUnitText = "(1,80 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" },
                                                        new Article { Id = 2, Image = "SomeString", Price = 17.99, PricePerUnitText = "(1,40 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" } };


            beer = new BeerItem { Id = 1, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles, Name = "SomeName" };

            articles1 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 17.99, PricePerUnitText = "(5,80 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" },
                                            new Article { Id = 2, Image = "CheapArticle", Price = 16.99, PricePerUnitText = "(0,80 €/Liter)", ShortDescription = "13 x 0,5L (Glas)" }};
            beer1 = new BeerItem { Id = 2, BrandName = "SomeBrand", DescriptionText = "CheapBeer", Articles = articles1, Name = "SomeName" };


            articles2 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 10, PricePerUnitText = "(1,80 €/Liter)", ShortDescription = "20 x 0,5L (Glas)" },
                                                          new Article{ Id = 2, Image = "SomeString", Price = 7.99, PricePerUnitText = "(2,80 €/Liter)", ShortDescription = "9 x 0,5L (Glas)" }};
            beer2 = new BeerItem { Id = 3, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles2, Name = "SomeName" };


            articles3 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 11.99, PricePerUnitText = "(1,80 €/Liter)", ShortDescription = "30 x 0,5L (Glas)" } };
            beer3 = new BeerItem { Id = 4, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles3, Name = "SomeName" };


            articles4 = new List<Article> { new Article { Id = 1, Image = "SomeString", Price = 33, PricePerUnitText = "(5,8 €/Liter)", ShortDescription = "30 x 0,5L (Glas)" } };
            beer4 = new BeerItem { Id = 5, BrandName = "SomeBrand", DescriptionText = "SomeString", Articles = articles4, Name = "SomeName" };

            goodItems.Add(beer1);
            goodItems.Add(beer2);
            goodItems.Add(beer3);
            goodItems.Add(beer4);
            goodItems.Add(beer);
        }
        [TestMethod]
        [DataRow(17.99, 3)]
        [DataRow(33, 1)]

        public void TestGetResultPositive(double price, int count)
        {
            parameters[0] = "https://flapotest.blob.core.windows.net/test/ProductData.json";
            parameters[1] = price.ToString();
            var getInputDataService = new Mock<IGetInputDataService>();
            getInputDataService.Setup(x => x.RetrieveDataFromInputUrl(parameters[0])).ReturnsAsync(goodItems);

            GetExactPricedBeerService service = new GetExactPricedBeerService();
            service.getInputDataServiceInstance = getInputDataService.Object;
            var resultBeers = service.GetResultBeer(parameters);

            Assert.IsTrue(resultBeers.Any(x => x.Caption == "ExactPriced_" + price.ToString() && x.Price == price) && resultBeers.Count == count);


        }

    }
}
