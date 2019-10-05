using GroceryStoreAPI.Entities;
using System.Collections.Generic;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestOrderData : TheoryData<int, Order>
    {
        public TestOrderData()
        {
            Add(1, new Order()
            {
                Id = 1,
                CustomerId = 1,
                Date = "2019/05/01",
                Items = new List<OrederItem>()
                {
                    new OrederItem()
                    {
                        ProductId = 1,
                        Quantity = 2
                    },
                    new OrederItem()
                    {
                        ProductId = 2,
                        Quantity = 3
                    }
                }
            });
            Add(2, new Order()
            {
                Id = 2,
                CustomerId = 2,
                Date = "2019/05/02",
                Items = new List<OrederItem>()
                {
                    new OrederItem()
                    {
                        ProductId = 2,
                        Quantity = 3
                    },
                    new OrederItem()
                    {
                        ProductId = 3,
                        Quantity = 4
                    }
                }
            });
        }
    }
}