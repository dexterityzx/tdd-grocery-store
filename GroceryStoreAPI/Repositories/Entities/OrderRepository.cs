using GroceryStoreAPI.Entities;
using System.Linq;

namespace GroceryStoreAPI.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(string file) : base(file)
        {
        }

        public override Order Key(string key)
        {
            if (int.TryParse(key, out int intKey))
            {
                return Key(intKey);
            }
            return null;
        }

        public override Order Key(int key)
        {
            return _data.Where(order => order.Id == key).FirstOrDefault();
        }
    }
}