/// <summary>
/// service to get data from input json to be consumed by other services
/// </summary>


using BeerData.Interfaces;
using BeerData.DataObjects;
using BeerInventoryServices.Services.Interfaces;
using BeerInventoryApi.Logger;
using Newtonsoft.Json;

namespace BeerInventoryServices.Services
{
    public class GetInputDataService : IGetInputDataService
    {
        public GetInputDataService()
        {

        }

        /// <summary>
        /// Retrieves json data from given url, converts it into BeerItems and retuen to calling method to act on
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<List<IBeerItem>> RetrieveDataFromInputUrl(string url)
        {
            LogService.LogInfo("GetInputDataService", "RetrieveDataFromInputUrl",$"Trying to get data from url: {url}");

            List<IBeerItem> inputBeerData = new List<IBeerItem>();
            List<BeerItem> inputBeer = new List<BeerItem>();
            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;

                    //Proceed only if success
                    if (response.IsSuccessStatusCode)
                    {
                        //Wait till all the data is read
                        string result = await response.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(result))
                        {
                            try
                            {
                                inputBeer = JsonConvert.DeserializeObject<List<BeerItem>>(result);
                                if(inputBeer != null && inputBeer.Count > 0)
                                    LogService.LogInfo("GetInputDataService", "RetrieveDataFromInputUrl", $"Seccessfully retrieved {inputBeer.Count} records from url");
                                else
                                    LogService.LogWarning("GetInputDataService", "RetrieveDataFromInputUrl", $"retrieved NO records from url");

                            }
                            catch (Exception ex)
                            {
                                LogService.LogError("GetInputDataService", "RetrieveDataFromInputUrl", "Exception in parsing json data");

                                throw ex;
                            }

                        }
                        else
                        {
                            LogService.LogInfo("GetInputDataService", "RetrieveDataFromInputUrl", "Empty result from given url");

                        }
                    }
                    else
                    {
                        LogService.LogError("GetInputDataService", "RetrieveDataFromInputUrl", "Exception in data from given url");

                        throw new HttpRequestException();
                    }
                }

                if(inputBeer != null)
                inputBeerData.AddRange(inputBeer);
            }
            catch(AggregateException ex)
            {
                LogService.LogError("GetInputDataService", "RetrieveDataFromInputUrl", $"Bad input url: {url}");
                throw ex ;
            }
            catch(Exception e)
            {
                LogService.LogError("GetInputDataService", "RetrieveDataFromInputUrl", "Exception in data from given url");
                throw e;
            }
            return inputBeerData;
        }
    }
}
