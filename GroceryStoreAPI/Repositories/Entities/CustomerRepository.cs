using GroceryStoreAPI.Entities;
using System.Linq;

namespace GroceryStoreAPI.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(string file = Constants.DB_FILE) : base(file)
        {
        }

        public CustomerRepository() : base()
        {
        }

        public override Customer Key(string key)
        {
            if (int.TryParse(key, out int intKey))
            {
                return Key(intKey);
            }
            return null;
        }

        public override Customer Key(int key)
        {
            return _collection.Where(customer => customer.Id == key).FirstOrDefault();
        }

        public override void Add(Customer newCustomer)
        {
            var lastCustomer = _collection.OrderByDescending(customer => customer.Id).FirstOrDefault();
            var id = 0;
            if (lastCustomer != null)
            {
                id = lastCustomer.Id + 1;
            }
            newCustomer.Id = id;
            base.Add(newCustomer);
        }
    }
}