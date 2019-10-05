using GroceryStoreAPI.Entities;
using System.Linq;

namespace GroceryStoreAPI.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(string file) : base(file)
        {
        }

        public override Product Key(string key)
        {
            if (int.TryParse(key, out int intKey))
            {
                return Key(intKey);
            }
            return null;
        }

        public override Product Key(int key)
        {
            return _data.Where(product => product.Id == key).FirstOrDefault();
        }
    }
}