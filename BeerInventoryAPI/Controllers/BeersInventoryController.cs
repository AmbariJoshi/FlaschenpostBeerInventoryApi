//Controller class for this API

using BeerInventoryHelper;
using Microsoft.AspNetCore.Mvc;
using BeerData.Interfaces;
using BeerInventoryAPI.ServiceFactory;
using BeerInventoryApi.Logger;

//using Serilog.AspNetCore;

namespace BeerInventoryAPI.Controllers
{
    [Route("api/beerInventory")]
    [ApiController]
    public class BeersInventoryController : ControllerBase
    {
        private readonly ServicesFactory _servicesFactory;
        private const string _defaultUrl = "https://flapotest.blob.core.windows.net/test/ProductData.json";
        private readonly string className = "BeersInventoryController";
     
        public BeersInventoryController()
        {
            _servicesFactory = new ServicesFactory();
        }



        /// <summary>
        /// Returns the most expensive and most cheap beer
        /// </summary>
        [HttpGet]
        [Route("minMax")]

        public JsonResult ReturnMaxMinPricedBeer(string url =_defaultUrl )
        {

            try
            {
                List<IResultBeer> resultBeer = _servicesFactory.GetService(TypeOfResult.ExpensiveCheap).GetResultBeer(new string[] { url });
                if (resultBeer == null || resultBeer.Count == 0)
                    return new JsonResult(NotFound().ToString() + "  :No beers found with maximum and minimum price");

                LogService.LogInfo(className, "ReturnMaxMinPricedBeer", "Successfully calculated Most expensive and cheap beer");

                return new JsonResult(Ok(resultBeer));

            }
            catch (Exception ex)
            {
                return new JsonResult("error!!! Please check response body and Log for more details") { ContentType = ex.Message };
            }
        }

        /// <summary>
        /// Returns the beer that comes in most bottles
        /// </summary>
        [HttpGet]
        [Route("mostBottles")]
        public JsonResult ReturnMostBottlesBeer(string url = _defaultUrl)
        {
            try
            {
                List<IResultBeer> resultBeer = _servicesFactory.GetService(TypeOfResult.MostBottles).GetResultBeer(new string[] { url });
                if (resultBeer == null || resultBeer.Count == 0)
                    return new JsonResult(NotFound().ToString() + "  :No beers found with price most bottles");

                LogService.LogInfo(className, "ReturnMostBottlesBeer", "Successfully calculated Most bottles beer");

                return new JsonResult(Ok(resultBeer));
            }
            catch (Exception ex)
            {
                return new JsonResult("error!!! Please check response body and Log for more details") { ContentType = ex.Message };
            }
        }


        /// <summary>
        /// Returns the beer with exact price. Default price is 17.99. User can give another input price
        /// </summary>
        [HttpGet]
        [Route("exactPriced")]
        public JsonResult ReturnExactPricedBeer(double price = 17.99, string url = _defaultUrl)
        {
            try
            {
                List<IResultBeer> resultBeer = _servicesFactory.GetService(TypeOfResult.ExactPriced).GetResultBeer(new string[] { url, price.ToString() });
                if (resultBeer == null || resultBeer.Count == 0)
                   return new JsonResult(NotFound().ToString() + $"  :Not beers found with price {price}");

                LogService.LogInfo(className, "ReturnExactPricedBeer", $"Successfully calculated beer with price {price}");

                return new JsonResult(Ok(resultBeer));

            }
            catch (Exception ex)
            {
                return new JsonResult("error!!! Please check response body and Log for more details") { ContentType= ex.Message };
            }
        }


        /// <summary>
        /// Returns the list of beers with all existing functionalities
        /// </summary>
        [HttpGet]
        [Route("allFunctions")]
        public JsonResult ReturnAllFunctions(double price = 17.99, string url = _defaultUrl)
        {
            try
            {
                List<IResultBeer> resultBeer = _servicesFactory.GetService(TypeOfResult.All).GetResultBeer(new string[] { url, price.ToString() });
                if (resultBeer == null || resultBeer.Count == 0)
                    return new JsonResult(NotFound().ToString() + "  :No beers found for all functions");
                LogService.LogInfo(className, "ReturnAllFunctions", $"Successfully calculated all functionalitites");

                return new JsonResult(Ok(resultBeer));
            }
            catch (Exception ex)
            {
                return new JsonResult("error!!! Please check response body and Log for more details") { ContentType = ex.Message };
            }
        }

    }

}

