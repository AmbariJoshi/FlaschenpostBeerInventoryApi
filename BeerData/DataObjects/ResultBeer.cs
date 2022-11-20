//This class represnts output json 

using BeerData.Interfaces;

namespace BeerData.DataObjects
{
    public class ResultBeer : IResultBeer
    {
        //This field shows what kind of result it is - e.g. MostExpensive and cheap/Most bottles/exact priced etc
        public string? Caption { get; set; }

        public int Id { get; set; }
        public string? Name { get; set; }

        public string? BrandName { get; set; }

        //Article id
        public int ResultId { get; set; }

        //Total price
        public double Price { get; set; }
        
        //Total quantity per article
        public Double Quantity { get; set; }
        
        //Perice per unit usually per litre
        public double PricePerUnit { get; set; }

    }
}