//Contract for Article from input json
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerData.Interfaces
{
    public interface IArticle
    {

        int Id { get; set; }
        string ShortDescription { get; set; }
        double Price { get; set; }
        string? Unit { get; set; }
        string PricePerUnitText { get; set; }
        string? Image { get; set; }
        double Quantity { get; set; }
        double PricePerUnit { get; set; }
    }
}
