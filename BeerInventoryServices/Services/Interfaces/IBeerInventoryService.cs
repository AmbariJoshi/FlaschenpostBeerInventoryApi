using BeerData.Interfaces;

namespace BeerInventoryServices.Services.Interfaces
{
    public interface IBeerInventoryService
    {
        List<IResultBeer> GetResultBeer(string[] parameters);
    }
}
