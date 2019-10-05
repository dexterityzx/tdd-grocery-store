using Xunit;

namespace UnitTestGroceryStoreAPI
{
    public class TheoryData<T1, T2, T3> : TheoryData
    {
        public void Add(T1 p1, T2 p2, T3 p3)
        {
            AddRow(p1, p2, p3);
        }
    }

    public class TheoryData<T1, T2> : TheoryData
    {
        public void Add(T1 p1, T2 p2)
        {
            AddRow(p1, p2);
        }
    }
}