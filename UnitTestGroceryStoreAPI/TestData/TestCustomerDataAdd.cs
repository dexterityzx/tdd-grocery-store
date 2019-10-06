using GroceryStoreAPI.Entities;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestCustomerDataAdd : TheoryData<Customer, string>
    {
        public TestCustomerDataAdd()
        {
            Add(new Customer()
            {
                Name = "Bob2"
            },
            "Bob2");
            Add(new Customer()
            {
                Name = "Mary2"
            },
            "Mary2");
            Add(new Customer()
            {
                Name = "Joe2"
            },
            "Joe2");
        }
    }
}