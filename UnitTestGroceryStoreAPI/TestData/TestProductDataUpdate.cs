using GroceryStoreAPI.Entities;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestProductDataUpdate : TheoryData<Product, Product>
    {
        public TestProductDataUpdate()
        {
            Add(new Product()
            {
                Id = 1,
                Description = "apple1",
                Price = 1
            },
            new Product()
            {
                Id = 1,
                Description = "apple1",
                Price = 1
            });
            Add(new Product()
            {
                Id = 2,
                Description = "orange",
                Price = 1.2
            },
            new Product()
            {
                Id = 2,
                Description = "orange",
                Price = 1.2
            });
            Add(new Product()
            {
                Id = 3,
                Description = "bananaaa",
                Price = 11
            },
            new Product()
            {
                Id = 3,
                Description = "bananaaa",
                Price = 11
            });
        }
    }
}