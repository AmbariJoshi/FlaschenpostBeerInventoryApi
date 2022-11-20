//This class resolves dependencies


using Unity;
using BeerInventoryServices.Services;
using BeerInventoryServices.Services.Interfaces;
using BeerInventoryHelper;

using BeerInventoryApi.Logger;

namespace BeerInventoryApi.Utility
{

    public static class DependencyHandler
    {

        static DependencyHandler()
        {
            UnityContainer = new UnityContainer();
        }

       

        //Unity container for DI
        public static IUnityContainer UnityContainer { get; private set; }


        //This method is called at start of service in Program.cs
        public static void Init()
        {
            //Register instances of all services so that it is created only once and used throughout 
            UnityContainer.RegisterType<IGetInputDataService, GetInputDataService>();
            UnityContainer.RegisterType<IBeerInventoryService, ExpensiveCheapBeerService>(TypeOfResult.ExpensiveCheap.ToString("f"));
            UnityContainer.RegisterType<IBeerInventoryService, GetMostBottlesBeerService>(TypeOfResult.MostBottles.ToString("f"));
            UnityContainer.RegisterType<IBeerInventoryService, GetExactPricedBeerService>(TypeOfResult.ExactPriced.ToString("f"));

            LogService.LogError("DependencyResolver", "Init", "Dependencies registered successfully");

        }



    }

}



