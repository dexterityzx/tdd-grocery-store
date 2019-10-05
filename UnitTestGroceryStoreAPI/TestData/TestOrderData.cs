using GroceryStoreAPI.Entities;
using System.Collections.Generic;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestOrderData : TheoryData<string, Order>
    {
        public TestOrderData()
        {
            Add("1", new Order()
            {
                Id = "1",
                CustomerId = "1",
                Items = new List<OrederItem>()
                {
                    new OrederItem()
                    {
                        ProductId = "1",
                        Quantity = 2
                    },
                    new OrederItem()
                    {
                        ProductId = "2",
                        Quantity = 3
                    }
                }
            });
        }
    }
}