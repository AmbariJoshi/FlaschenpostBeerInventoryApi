/// <summary>
/// Service class to get exact priced beers
/// </summary>

using BeerData.Interfaces;
using BeerInventoryServices.Services.Interfaces;
using BeerInventoryApi.Logger;

namespace BeerInventoryServices.Services
{
    public class GetExactPricedBeerService : IBeerInventoryService
    {
        public IGetInputDataService getInputDataServiceInstance;
  
        public GetExactPricedBeerService()
        {
            getInputDataServiceInstance = new GetInputDataService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters" price and url></param>
        /// <returns>List of beers with exact price entred by user</returns>
        public List<IResultBeer> GetResultBeer(string[] parameters)
        {
            if (parameters == null || parameters.Length <= 1)
                return null;
            try
            {
                //Get data from service from inpt url converted into list of BeerItems from json format
                List<IBeerItem> inputBeerData = getInputDataServiceInstance.RetrieveDataFromInputUrl(parameters[0]).Result;

                if (inputBeerData == null)
                {
                    return null;
                }
                try
                {
                    var price = Convert.ToDouble(parameters[1]);
                    //Extract price from user input and send to service to calculate beers with that exact price
                    return Helper.ResultBeerLogicCalculator.GetBeersWithExactPrice(inputBeerData, price);
                }
                catch 
                {
                    LogService.LogError("GetExactPricedBeerService", "GetResultBeer", "Exception thrown");
                    throw;
                }
             }
            catch
            {
                LogService.LogError("GetExactPricedBeerService", "GetResultBeer", "Exception thrown");

                throw;
            }
        }
    }
}
