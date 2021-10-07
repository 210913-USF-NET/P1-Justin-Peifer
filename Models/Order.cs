using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Order
    {
        public Order() {}

        public Order (int OrderId, int UserId, List<LineItem> LineItems){
            this.Id = OrderId;
            this.UserId = UserId;
            this.DateOrdered = DateTime.Now;
            this.LineItems = LineItems;
        }
        [Key]
        public int Id { get; set; }
        //LineItem will use the OrderID , and then you will get OrderedItems by looking up all LineItems with the OrderId

        public int UserId { get; set; }

        public DateTime DateOrdered { get; set; }
        public List<LineItem> LineItems {get; set; }
    }
}