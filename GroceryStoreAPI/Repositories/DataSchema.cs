using GroceryStoreAPI.Entities;
using System.Collections.Generic;

namespace GroceryStoreAPI.Repositories
{
    /// DataSchema class description the collecitons in the repository
    /// Collection name is the plural form of entity name.
    /// ex. Product -> Products
    public class DataSchema
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}