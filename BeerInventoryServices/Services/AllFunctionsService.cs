
using BeerData.Interfaces;
using BeerInventoryServices.Services.Interfaces;

namespace BeerInventoryServices.Services
{
    public class AllFunctionsService : IBeerInventoryService
    {
        private readonly IEnumerable<IBeerInventoryService> beerInventoryServices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="beerInventoryServices"></param>
        public AllFunctionsService(IEnumerable<IBeerInventoryService> beerInventoryServices)
        {
            this.beerInventoryServices = beerInventoryServices;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns> List of beers with all functionalities</returns>
        public List<IResultBeer> GetResultBeer(string[] parameters)
        {
            try
            {
                List<IResultBeer> resultBeers = new List<IResultBeer>();

                if (parameters == null || parameters.Length <= 1)
                    return resultBeers;

                if (beerInventoryServices != null)
                {
                    //Go to all services and get results to display
                    foreach (IBeerInventoryService service in beerInventoryServices)
                    {
                        try
                        {
                            List<IResultBeer> resultItems = service.GetResultBeer(parameters);
                            resultBeers.AddRange(resultItems);
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
                return resultBeers;
            }
            catch
            {
                throw;
            }
        }
    }
}
