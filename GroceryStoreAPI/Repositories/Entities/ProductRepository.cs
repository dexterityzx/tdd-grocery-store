using GroceryStoreAPI.Entities;
using System.Linq;

namespace GroceryStoreAPI.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(string file) : base(file)
        {
        }

        public ProductRepository() : base()
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
            return _dataSet.Where(product => product.Id == key).FirstOrDefault();
        }

        public override void Add(Product entity)
        {
            throw new System.NotImplementedException();
        }
    }
}