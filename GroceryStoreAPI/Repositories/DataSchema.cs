using GroceryStoreAPI.Entities;
using System.Collections.Generic;

/// DataSchema class description the collecitons in the repository
/// Collection name is the plural form of entity name.
/// ex. Product -> Products

namespace GroceryStoreAPI.Repositories
{
    public class DataSchema
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}