using System.Collections.Generic;

namespace GroceryStoreAPI.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected readonly IEnumerable<TEntity> _data;

        public BaseRepository()
        {
            var json = RepositoryHelper.ReadFile(Constants.DB_FILE);
            _data = RepositoryHelper.ToEnumerable<TEntity>(json);
        }

        public abstract TEntity Key(string key);
    }
}