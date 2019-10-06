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
            return _collection.Where(product => product.Id == key).FirstOrDefault();
        }

        public override void Add(Product newProduct)
        {
            //find the max primary key and increase by 1
            var lastProduct = _collection.OrderByDescending(product => product.Id).FirstOrDefault();
            var id = 0;
            if (lastProduct != null)
            {
                id = lastProduct.Id + 1;
            }
            newProduct.Id = id;
            base.Add(newProduct);
        }
    }
}