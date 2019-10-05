using GroceryStoreAPI.Entities;
using System.Linq;

namespace GroceryStoreAPI.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(string file) : base(file)
        {
        }

        public override Customer Key(string key)
        {
            if (int.TryParse(key, out int intKey))
            {
                return _data.Where(customer => customer.Id == intKey).FirstOrDefault();
            }
            return null;
        }
    }
}