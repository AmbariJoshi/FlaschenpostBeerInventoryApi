

namespace BeerInventoryServices.Services.Interfaces
{
    public interface IGetInputDataService
    {
        Task<List<BeerData.Interfaces.IBeerItem>> RetrieveDataFromInputUrl(string url);
    }
}
