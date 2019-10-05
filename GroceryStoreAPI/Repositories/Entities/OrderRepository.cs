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
            return _data.Where(order => order.Id == key).FirstOrDefault();
        }
    }
}