using GroceryStoreAPI.Entities;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestCustomerDataUpdate : TheoryData<Customer, string>
    {
        public TestCustomerDataUpdate()
        {
            Add(new Customer()
            {
                Id = 1,
                Name = "Bob22"
            },
            "Bob22");
            Add(new Customer()
            {
                Id = 2,
                Name = "Mary22"
            },
            "Mary22");
            Add(new Customer()
            {
                Id = 3,
                Name = "Joe22"
            },
            "Joe22");
        }
    }
}