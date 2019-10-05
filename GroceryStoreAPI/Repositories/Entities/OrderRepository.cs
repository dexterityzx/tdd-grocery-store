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
                return _data.Where(order => order.Id == intKey).FirstOrDefault();
            }
            return null;
        }
    }
}