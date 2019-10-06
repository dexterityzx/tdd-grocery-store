using GroceryStoreAPI.Entities;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestProductDataAdd : TheoryData<Product, Product>
    {
        public TestProductDataAdd()
        {
            Add(new Product()
            {
                Description = "a",
                Price = 10
            },
            new Product()
            {
                Description = "a",
                Price = 10
            });
            Add(new Product()
            {
                Description = "aa",
                Price = 11
            },
            new Product()
            {
                Description = "aa",
                Price = 11
            });
            Add(new Product()
            {
                Description = "aaa",
                Price = 12
            },
            new Product()
            {
                Description = "aaa",
                Price = 12
            });
        }
    }
}