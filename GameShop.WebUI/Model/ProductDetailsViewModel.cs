namespace GameShop.WebUI.Model
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? UnitInPrice { get; set; }
        public int UnitInStock { get; set; }
        public string ImagePath { get; set; }
    }
}
