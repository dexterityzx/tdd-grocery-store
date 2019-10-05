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
            return _data.Where(customer => customer.Id == key).FirstOrDefault();
        }
    }
}