
using BeerInventoryHelper;
using BeerInventoryApi.Logger;
using Serilog;
using Serilog.Configuration;
using Serilog.Extensions;

namespace BeerInventoryApi.Test.HelperMethodTesting
{
    [TestClass]
    public class HelperTest
    {
        [TestInitialize]
        public void ClassInitialize()
        {
            //Inititializing logger for tests
            LogService.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        }

        [TestMethod]
       // [DataRow("20 x 0,5L (Glas)",20)]
        [DataRow("dfsggssg", 0)]

        public void TestGetBottlesPositive(string input, int output)
        {
            Assert.AreEqual(Helper.GetBottleCount(input), output);
        }

        [TestMethod]
        [DataRow("fhdh x 0,5L (Glas)", 20)]

        public void TestGetBottlesNegative(string input, int output)
        {
            Assert.AreNotEqual(Helper.GetBottleCount(input), output);
        }


        [TestMethod]
        [DataRow("(1,80 €/Liter)", 1.8)]
        [DataRow("dfsggssg", 0)]

        public void TestGetUnitPricePositive(string input, double output)
        {
            Assert.AreEqual(Helper.GetUnitPrice(input), output);
        }

        [TestMethod]
        [DataRow("fhdh x 0,5L (Glas)", 20)]

        public void TestGetUnitPriceNegative(string input, double output)
        {
            Assert.AreNotEqual(Helper.GetUnitPrice(input), output);
        }



    }
}
