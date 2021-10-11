namespace Models
{
    public class LineItem
    {
        public int Id { get; set;  }
        public int OrderId { get; set; }
        public int StoreFrontId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }

        public LineItem() {}

        public LineItem(int StoreFrontId, int ProductId, int Quantity)
        {
            this.StoreFrontId = StoreFrontId;
            this.ProductId = ProductId;
            this.Quantity = Quantity;
        }

        public LineItem (int OrderId, int StoreFrontId, int ProductId, int Quantity){
            this.OrderId = OrderId;
            this.StoreFrontId = StoreFrontId;
            this.ProductId = ProductId;
            this.Quantity = Quantity;
        }
    }
}