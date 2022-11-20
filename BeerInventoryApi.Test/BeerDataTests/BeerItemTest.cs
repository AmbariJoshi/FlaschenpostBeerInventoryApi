using BeerData.DataObjects;

namespace TestProject1.BeerDataTests
{
    [TestClass]
    public class BeerItemTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var testClass = new BeerItem();
            Assert.IsNotNull(testClass);

        }
    }
}
