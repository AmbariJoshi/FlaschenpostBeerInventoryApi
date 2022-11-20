using BeerData.Interfaces;

namespace BeerData.DataObjects
{
    public class BeerItem : IBeerItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? BrandName { get; set; }
        public string? DescriptionText { get; set; }
        public List<Article>? Articles { get; set; }

    }

}
