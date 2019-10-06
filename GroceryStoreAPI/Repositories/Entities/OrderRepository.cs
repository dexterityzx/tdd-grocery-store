using GroceryStoreAPI.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStoreAPI.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(string file) : base(file)
        {
        }

        public OrderRepository() : base()
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
            return _dataSet.Where(order => order.Id == key).FirstOrDefault();
        }

        public IEnumerable<Order> GetByDate(string date)
        {
            return _dataSet.Where(order => order.Date == date);
        }

        public IEnumerable<Order> GetByCustomerId(int customerId)
        {
            return _dataSet.Where(order => order.CustomerId == customerId);
        }

        public override void Add(Order entity)
        {
            throw new System.NotImplementedException();
        }
    }
}