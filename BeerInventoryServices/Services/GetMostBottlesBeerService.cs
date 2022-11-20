/// <summary>
/// Service to get most bottled beer
/// </summary>

using BeerData.Interfaces;
using BeerInventoryServices.Services.Interfaces;
using BeerInventoryApi.Logger;

namespace BeerInventoryServices.Services
{
    public class GetMostBottlesBeerService : IBeerInventoryService
    {
        public IGetInputDataService getInputDataServiceInstance;
        public GetMostBottlesBeerService()
        {
           getInputDataServiceInstance = new GetInputDataService();
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="parameters"  url></param>
       /// <returns>List of most bottles beer</returns>
        public List<IResultBeer> GetResultBeer(string[] parameters)
        {
            if (parameters == null || parameters.Length <= 0)
                return null;

            try
            {
                List<IBeerItem> inputBeerData = getInputDataServiceInstance.RetrieveDataFromInputUrl(parameters[0]).Result;

                if (inputBeerData == null)
                {
                    return null;
                }

                return Helper.ResultBeerLogicCalculator.GetMostBottlesBeer(inputBeerData);
            }
            catch
            {
                LogService.LogError("GetMostBottlesBeerService", "GetResultBeer", "Exception thrown");

                throw;
            }
        }
    }
}
