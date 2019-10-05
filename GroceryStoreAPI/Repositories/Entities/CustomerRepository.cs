using GroceryStoreAPI.Entities;
using System.Linq;

namespace GroceryStoreAPI.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(string file) : base(file)
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
            return _data.Where(customer => customer.Id == key).FirstOrDefault();
        }
    }
}