using UnitTestGroceryStoreAPI.Helpers;

namespace UnitTestGroceryStoreAPI.TestData
{
    internal class TestOrderDataByDate : TheoryData<string, string>
    {
        public TestOrderDataByDate()
        {
            Add("2019/05/02", "2019/05/02");
            Add("2019/05/01", "2019/05/01");
        }
    }
}