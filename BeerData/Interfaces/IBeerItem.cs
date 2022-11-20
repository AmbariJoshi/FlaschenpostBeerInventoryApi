//Contract for Beer item from input json

using BeerData.DataObjects;

namespace BeerData.Interfaces
{
    public interface IBeerItem
    {
        int Id { get; set; }
        string? Name { get; set; }
        string? BrandName { get; set; }
        string? DescriptionText { get; set; }
        List<Article>? Articles { get; set; }

    }
}
