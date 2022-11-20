//This class represents Article from input json


using BeerInventoryHelper;
using BeerData.Interfaces;
using BeerInventoryApi.Logger;

namespace BeerData.DataObjects
{
    public class Article :IArticle
    {

        public int Id { get; set; }
        private string _shortDescription = String.Empty;

        //Short description has information of how many bottles per article like 20 x 0,5L (Glas)
        //We extract Quantity *here 20 and assign it to property 'Quantity'
        public string ShortDescription
        {
            get { return _shortDescription; }
            set
            {
                _shortDescription = value;
                try
                {
                    Quantity = Helper.GetBottleCount(_shortDescription);
                }
                catch(Exception ex)
                {
                    LogService.LogError("Article", "ShortDescription", $"Exception extracting Quantity from shortDescription. Setting Quantity by default to 0: {ex.Message}");
                    Quantity = 0;
                }
            }
        }
        public double Price { get; set; }

        public string? Unit { get; set; }

        private string _pricePerUnitText = String.Empty;

        //This field has value in format like  (1,80 €/Liter). We extract the price(1.8) value from text and assign to 'Price per unit' property
        public string PricePerUnitText

        {
            get { return _pricePerUnitText; }
            set
            {
                _pricePerUnitText = value;
                try
                {
                    PricePerUnit = Helper.GetUnitPrice(_pricePerUnitText);
                }
                catch (Exception ex)
                {
                    LogService.LogError("Article", "ShortDescription", $"Exception extracting Quantity from shortDescription. Setting Quantity by default to 0: {ex.Message}");
                    PricePerUnit = 0;
                }
            
            }
        }
        public string? Image { get; set; }

        public double Quantity { get; set; }
        public double PricePerUnit { get; set; }
    }
}
