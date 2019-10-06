using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GroceryStoreAPI.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected DataSchema _data;
        protected IEnumerable<TEntity> _dataSet;
        private readonly string _file;

        // staged added entities
        private List<TEntity> _addedEntities;

        // staged updated entities
        private List<Tuple<TEntity, string>> _updatedEntities;

        public BaseRepository(string file = Constants.DB_FILE)
        {
            _file = file;
            Load();
        }

        private void Load()
        {
            var json = RepositoryHelper.ReadFile(_file);
            _data = RepositoryHelper.ToData(json);
            _dataSet = RepositoryHelper.ToDataSet<TEntity>(json);
            _addedEntities = new List<TEntity>();
            _updatedEntities = new List<Tuple<TEntity, string>>();
        }

        public abstract TEntity Key(int key);

        public abstract TEntity Key(string key);

        public virtual void Add(TEntity entity)
        {
            _addedEntities.Add(entity);
        }

        public virtual void Update(TEntity entity, string key = "Id")
        {
            _updatedEntities.Add(Tuple.Create(entity, key));
        }

        //Save to json file
        private List<TEntity> GetEntities(Type type)
        {
            PropertyInfo propertyInfo = _data.GetType().GetProperty(type.Name + "s", BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null) return null;
            return (List<TEntity>)propertyInfo.GetValue(_data);
        }

        private int SaveStagedAddedEntities()
        {
            var addedNum = 0;
            _addedEntities.ForEach(entity =>
            {
                var entities = GetEntities(entity.GetType());
                if (entities == null) return;

                MethodInfo addMethod = entities.GetType().GetMethod("Add");
                addMethod.Invoke(entities, new object[] { entity });
                addedNum++;
            });
            return addedNum;
        }

        private int SaveStagedUpdatedEntities()
        {
            var updatedNum = 0;
            _updatedEntities.ForEach(tuple =>
            {
                var newEntity = tuple.Item1;
                var keyName = tuple.Item2;
                var key = (int)newEntity.GetType().GetProperty(keyName).GetValue(newEntity);

                var entities = GetEntities(newEntity.GetType());
                if (entities == null) return;

                entities.ForEach(entity =>
                {
                    Type entityType = entity.GetType();
                    PropertyInfo[] propertyInfos = entityType.GetProperties();
                    foreach (PropertyInfo propertyInfo in propertyInfos)
                    {
                        var property = entityType.GetProperty(propertyInfo.Name, BindingFlags.Public | BindingFlags.Instance);

                        if (property.Name == keyName && key != (int)property.GetValue(entity)) return;

                        if (property.Name != keyName && property.CanWrite)
                        {
                            var newValue = newEntity.GetType().GetProperty(propertyInfo.Name).GetValue(newEntity);
                            property.SetValue(entity, newValue);
                        }
                    }
                    updatedNum++;
                });
            });
            return updatedNum;
        }

        public virtual int Save()
        {
            try
            {
                var addedNum = SaveStagedAddedEntities();
                var updatedNum = SaveStagedUpdatedEntities();

                var json = JsonConvert.SerializeObject(_data, Formatting.Indented);
                RepositoryHelper.WriteFile(_file, json);

                Load();

                return addedNum + updatedNum;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<TEntity> All()
        {
            return _dataSet;
        }
    }
}