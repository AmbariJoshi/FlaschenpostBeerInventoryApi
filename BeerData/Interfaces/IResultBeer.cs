//Contract for Result for output json

namespace BeerData.Interfaces
{
    public interface IResultBeer
    {
        string? Caption { get; set; }

        int Id { get; set; }
        string? Name { get; set; }

        string? BrandName { get; set; }

        int ResultId { get; set; }

        double Price { get; set; }
        Double Quantity { get; set; }
        double PricePerUnit { get; set; }


    }
}
