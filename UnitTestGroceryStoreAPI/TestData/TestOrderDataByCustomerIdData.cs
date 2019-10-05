using System.Collections.Generic;
using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestOrderDataByCustomerIdData : TheoryData<int, IEnumerable<int>>
    {
        public TestOrderDataByCustomerIdData()
        {
            Add(1, new List<int>() { 1 });
            Add(2, new List<int>() { 2, 3 });
        }
    }
}