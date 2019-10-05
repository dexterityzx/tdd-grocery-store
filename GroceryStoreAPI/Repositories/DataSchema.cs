using GroceryStoreAPI.Entities;
using System.Collections.Generic;

namespace GroceryStoreAPI.Repositories
{
    public class DataSchema
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}