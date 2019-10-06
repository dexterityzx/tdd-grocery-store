using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GroceryStoreAPI.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Defautl key name is "Id"
        /// </summary>
        protected const string DEFAULT_KEY_NAME = "Id";

        /// <summary>
        /// The super set of collections. It contains all the collections.
        /// </summary>
        protected DataSchema _data;

        /// <summary>
        /// The sub set of data, it contains one type of entity.
        /// </summary>
        protected IEnumerable<TEntity> _collection;

        /// <summary>
        /// The location of the json file to read and write.
        /// </summary>
        private readonly string _file;

        /// <summary>
        /// Store the info about the staging. Will be clear after saving to file.
        /// </summary>
        private List<StagedItem<TEntity>> _stagedItems;

        public BaseRepository(string file = Constants.DB_FILE)
        {
            _file = file;
            LoadCollection();
            InitializeStagedEntities();
        }

        /// <summary>
        /// Loads collection from json file.
        /// </summary>
        private void LoadCollection()
        {
            var json = RepositoryHelper.ReadFile(_file);
            _data = RepositoryHelper.ToData(json);
            _collection = RepositoryHelper.ToCollection<TEntity>(json);
        }

        /// <summary>
        /// Resets all staged entities.
        /// </summary>
        private void InitializeStagedEntities()
        {
            _stagedItems = new List<StagedItem<TEntity>>();
        }

        /// <summary>
        /// Gets an entity by integer type of primary key.
        /// </summary>
        /// <param name="key">Primary key</param>
        /// <returns>Returns an entity</returns>
        public abstract TEntity Key(int key);

        /// <summary>
        /// Gets an entity by string type of primary key.
        /// </summary>
        /// <param name="key">Primary key</param>
        /// <returns>Returns an entity</returns>
        public abstract TEntity Key(string key);

        /// <summary>
        /// Stages an entity to add.
        /// </summary>
        /// <param name="entity">Entity to stage to save</param>
        /// <param name="keyName">Name of the key of the collection</param>
        public virtual void Add(TEntity entity, string keyName = DEFAULT_KEY_NAME)
        {
            _stagedItems.Add(new StagedItem<TEntity>(entity, keyName, ActionType.ADD));
        }

        /// <summary>
        /// Stages an entity to add.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(TEntity entity)
        {
            _stagedItems.Add(new StagedItem<TEntity>(entity, DEFAULT_KEY_NAME, ActionType.ADD));
        }

        /// <summary>
        /// Checks if the entity with the primary key exists in collection.
        /// </summary>
        /// <param name="key">Primary key</param>
        /// <returns>Returns true or false</returns>
        public virtual bool Exist(int key)
        {
            return Key(key) != null;
        }

        /// <summary>
        /// Stages an entity to update.
        /// </summary>
        /// <param name="entity">Entity to stage to save</param>
        /// <param name="keyName">Name of the key of the collection</param>
        public virtual void Update(TEntity entity, string keyName = DEFAULT_KEY_NAME)
        {
            _stagedItems.Add(new StagedItem<TEntity>(entity, keyName, ActionType.UPDATE));
        }

        /// <summary>
        /// Stages an entity to update.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(TEntity entity)
        {
            _stagedItems.Add(new StagedItem<TEntity>(entity, DEFAULT_KEY_NAME, ActionType.UPDATE));
        }

        /// <summary>
        /// Get collection by type
        /// </summary>
        /// <param name="type">Type of collection</param>
        /// <returns>A Collection of a type of entity</returns>
        private List<TEntity> GetCollection(Type type)
        {
            var collectionName = RepositoryHelper.ToCollectionName(type.Name);

            PropertyInfo propertyInfo = _data.GetType()
                .GetProperty(collectionName, BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null) return null;

            return (List<TEntity>)propertyInfo.GetValue(_data);
        }

        /// <summary>
        /// Process staged items
        /// </summary>
        /// <returns></returns>
        private int ProcessStagedItems()
        {
            var n = 0;
            _stagedItems.ForEach(stagedItem =>
            {
                if (stagedItem.Action == ActionType.ADD)
                {
                    n += AddToCollection(stagedItem);
                }
                if (stagedItem.Action == ActionType.UPDATE)
                {
                    n += UpdateCollection(stagedItem);
                }
            });
            return n;
        }

        /// <summary>
        /// Add an entity to Collection from staged item
        /// </summary>
        /// <param name="stagedItem"></param>
        /// <returns> if success return 1, else 0</returns>
        private int AddToCollection(StagedItem<TEntity> stagedItem)
        {
            var entity = stagedItem.Entity;

            var key = (int)entity.GetType().GetProperty(stagedItem.PrimaryKeyName).GetValue(entity);
            if (Exist(key)) return 0;

            var collection = GetCollection(entity.GetType());
            if (collection == null) return 0;

            MethodInfo addMethod = collection.GetType().GetMethod("Add");
            addMethod.Invoke(collection, new object[] { entity });

            return 1;
        }

        /// <summary>
        /// Update an entity to Collection from staged item
        /// </summary>
        /// <param name="stagedItem"></param>
        /// <returns>if success return 1, else 0</returns>
        private int UpdateCollection(StagedItem<TEntity> stagedItem)
        {
            var updatedEntity = stagedItem.Entity;
            var updatedEntityKey = (int)updatedEntity.GetType().GetProperty(stagedItem.PrimaryKeyName).GetValue(updatedEntity);
            if (!Exist(updatedEntityKey)) return 0;

            var collection = GetCollection(updatedEntity.GetType());
            if (collection == null) return 0;

            // loop through collection to find the entity. Stop when we find first one.
            var stop = false;
            collection.ForEach(entity =>
            {
                if (stop) return;

                var currentEntitykey = (int)entity.GetType().GetProperty(stagedItem.PrimaryKeyName).GetValue(entity);
                if (currentEntitykey != updatedEntityKey) return;

                //loop through properties to copy values from new entity
                Type entityType = entity.GetType();
                PropertyInfo[] properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != stagedItem.PrimaryKeyName && property.CanWrite)
                    {
                        var newValue = updatedEntity.GetType().GetProperty(property.Name).GetValue(updatedEntity);
                        property.SetValue(entity, newValue);
                    }
                }

                stop = true;
            });
            return 1;
        }

        /// <summary>
        /// Write staged items to json file
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            try
            {
                LoadCollection();

                var NumberOfProcessedItem = ProcessStagedItems();

                var json = JsonConvert.SerializeObject(_data, Formatting.Indented);
                RepositoryHelper.WriteFile(_file, json);

                LoadCollection();
                InitializeStagedEntities();

                return NumberOfProcessedItem;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<TEntity> All()
        {
            return _collection;
        }
    }
}