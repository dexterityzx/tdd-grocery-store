using GroceryStoreAPI.Entities;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestProductData : TheoryData<int, Product>
    {
        public TestProductData()
        {
            Add(1, new Product()
            {
                Id = 1,
                Description = "apple",
                Price = 0.5
            });
            Add(2, new Product()
            {
                Id = 2,
                Description = "orange",
                Price = 0.75
            });
            Add(3, new Product()
            {
                Id = 3,
                Description = "banana",
                Price = 0.85
            });
        }
    }
}