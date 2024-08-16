namespace Book_Store_MVC.Models
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        public string?  imgfile {  get; set; }
    }
}
