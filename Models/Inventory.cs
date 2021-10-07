using System.Collections.Generic;

namespace Models
{
    public class Inventory
    {
        
        public int Id { get; set; }
        public int StoreFrontId { get; set; }
        
        public int ProductId { get; set; }
        private int _quantity;

        public int Quantity
                {
                    get
                    {
                        return _quantity;
                    } 
                    set
                    {
                        if (value<0)
                        {
                            throw new NegativeInventoryException("Inventory for a product cannot be negative.");
                        }
                        else{
                            _quantity = value;
                        }
                    }
                }

        public virtual Product Product { get; set; }
        public virtual StoreFront Store { get; set; }


        public Inventory() {}
        public Inventory(int StoreFrontId, int productId, int quantity){
            this.StoreFrontId = StoreFrontId;
            this.ProductId = productId;
            this.Quantity = quantity;
        }

        
    }

}