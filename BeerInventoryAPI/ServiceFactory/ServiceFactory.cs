//This class returns the exact service instance from dependency based on user action

using BeerInventoryServices.Services.Interfaces;
using BeerInventoryHelper;
using BeerInventoryApi.Utility;
using Unity;
using BeerInventoryServices.Services;

namespace BeerInventoryAPI.ServiceFactory
{
    public class ServicesFactory
    {
        public ServicesFactory()
        {

        }
        public IBeerInventoryService GetService(TypeOfResult typeOfResult)
        {
            switch (typeOfResult)
            {
                //return new service to determine most expensive and most cheap beers
                case TypeOfResult.ExpensiveCheap:
                    return DependencyHandler.UnityContainer.Resolve<IBeerInventoryService>(TypeOfResult.ExpensiveCheap.ToString("f"));

                case TypeOfResult.MostBottles:
                    //return new service to determine beer coming in most bottles
                    return DependencyHandler.UnityContainer.Resolve<IBeerInventoryService>(TypeOfResult.MostBottles.ToString("f"));


                case TypeOfResult.ExactPriced:
                    //return new service to determine beer with exact price inpiut by user
                    return DependencyHandler.UnityContainer.Resolve<IBeerInventoryService>(TypeOfResult.ExactPriced.ToString("f"));


                case TypeOfResult.All:
                    //return new service to compute all functionlitites. It will need list of all services 
                    return new AllFunctionsService(DependencyHandler.UnityContainer.Resolve<IEnumerable<IBeerInventoryService>>());


                default:
                    return new AllFunctionsService(DependencyHandler.UnityContainer.Resolve<IEnumerable<IBeerInventoryService>>());

            }
        }
    }
}


