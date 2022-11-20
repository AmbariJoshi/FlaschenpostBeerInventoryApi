using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerData.Interfaces;
using BeerInventoryServices.Services.Interfaces;
using BeerInventoryApi.Logger;


namespace BeerInventoryServices.Services
{
    public class ExpensiveCheapBeerService : IBeerInventoryService
    {
        //To retrieve data from input url
        public IGetInputDataService getInputDataServiceInstance;

       
        public ExpensiveCheapBeerService()
        {
            getInputDataServiceInstance = new GetInputDataService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>List of beers that are most cheap and most expensive</returns>
        public List<IResultBeer> GetResultBeer(string[] parameters)
        {
            if (parameters == null || parameters.Length <= 0)
                return null;

            try
            {
                //Get data from inputnurl in objects list format from json
                List<IBeerItem> inputBeerData = getInputDataServiceInstance.RetrieveDataFromInputUrl(parameters[0]).Result;

                if (inputBeerData == null)
                {
                    return null;
                }

                //Send input data to service and get most expensive and cheap beers
                return Helper.ResultBeerLogicCalculator.GetMostExpensiveCheapBeer(inputBeerData);
            }
            catch(Exception ex)
            {
                LogService.LogError("ExpensiveCheapBeerService", "GetResultBeer", "Exception thrown");
                throw ex;
            }
        }
    }
}
