
using BeerInventoryServices.Services;
using Serilog;
using BeerInventoryApi.Logger;


namespace BeerInventoryApi.Test.Services
{
    [TestClass]
    public class InputDataServiceTest
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
        [DataRow("https://flapotest.blob.core.windows.net/test/ProductData.json")]
        public void TestInputdataPositive(string url)
        {
            var resultBeers = new GetInputDataService().RetrieveDataFromInputUrl(url);
            Assert.IsNotNull(resultBeers.Result);


        }

        [TestMethod]
        [DataRow("https://flapotblob.core.windows.net/test/ProductData.json")]
        public void TestInputdataNegative(string url)
        {
            var resultBeers = new GetInputDataService().RetrieveDataFromInputUrl(url);
            Assert.IsTrue(resultBeers.IsFaulted);
        }

    }
    }
