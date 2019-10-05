using System.Collections.Generic;

namespace GroceryStoreAPI.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly IEnumerable<TEntity> _data;

        public BaseRepository(string file = Constants.DB_FILE)
        {
            var json = RepositoryHelper.ReadFile(file);
            _data = RepositoryHelper.ToEnumerable<TEntity>(json);
        }

        public abstract TEntity Key(int key);

        public abstract TEntity Key(string key);

        public IEnumerable<TEntity> All()
        {
            return _data;
        }
    }
}