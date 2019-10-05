using System.Collections.Generic;

namespace GroceryStoreAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public IEnumerable<OrederItem> Items { get; set; }
    }

    public class OrederItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}