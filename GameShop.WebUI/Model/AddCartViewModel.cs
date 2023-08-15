namespace GameShop.WebUI.Model
{
    public class AddCartViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? UnitInPrice { get; set; }
        public string ImagePath { get; set; }
        public decimal? TotalPrice { get; set; }
        public string UserName { get; set; }
        public int UnitInStock { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }


    }
}
