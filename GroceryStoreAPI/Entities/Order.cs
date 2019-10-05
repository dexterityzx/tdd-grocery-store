using System.Collections.Generic;

namespace GroceryStoreAPI.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public IEnumerable<OrederItem> Items { get; set; }
    }

    public class OrederItem
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
    }
}