using GroceryStoreAPI.Entities;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestCustomerData : TheoryData<string, Customer>
    {
        public TestCustomerData()
        {
            Add("1", new Customer()
            {
                Id = "1",
                Name = "Bob"
            });
            Add("2", new Customer()
            {
                Id = "2",
                Name = "Mary"
            });
            Add("3", new Customer()
            {
                Id = "3",
                Name = "Joe"
            });
        }
    }
}