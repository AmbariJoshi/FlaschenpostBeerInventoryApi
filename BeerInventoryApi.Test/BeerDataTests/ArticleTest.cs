using BeerInventoryApi.Utility;
using BeerInventoryApi.Logger;
using Serilog;
using Serilog.Configuration;
using Serilog.Extensions;
using BeerData.DataObjects;

namespace TestProject1.BeerDataTests
{
    [TestClass]
    public class ArticleTest
    {
        [TestInitialize]
        public void ClassInitialize()
        {
            LogService.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

        }

        [TestMethod]
        [DataRow("20 x 0,5L (Glas)", 20)]
        [DataRow("3 x 0,5L (Glas)", 3)]
        public void TestQuantityPositive(string shortDescrption, int quantity)
        {
            var testClass = new Article();
            testClass.ShortDescription = shortDescrption;
            Assert.AreEqual(quantity, testClass.Quantity);

        }

        [TestMethod]
        [DataRow("(1,80 €/Liter)", 1.8)]
        [DataRow("(2,10 €/Liter)", 2.1)]
        public void TestPricePerUnitPositive(string pricePerUnitText, double price)
        {
            var testClass = new Article();
            testClass.PricePerUnitText = pricePerUnitText;
            Assert.AreEqual(price, testClass.PricePerUnit);

        }

        [TestMethod]
        public void TestQuantityNegative()
        {
            var testClass = new Article();
            testClass.ShortDescription = "abc";
            Assert.AreEqual(testClass.Quantity, 0);

        }

        [TestMethod]
        [DataRow("(d €/Liter)", 0)]
        [DataRow("df", 0)]
        public void TestPricePerUnitNegative(string pricePerUnitText, double price)
        {
            var testClass = new Article();
            testClass.PricePerUnitText = pricePerUnitText;
            Assert.AreEqual(price, testClass.PricePerUnit);

        }
    }
}
