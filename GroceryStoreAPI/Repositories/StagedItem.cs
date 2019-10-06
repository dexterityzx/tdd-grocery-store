namespace GroceryStoreAPI.Repositories
{
    public enum ActionType
    {
        ADD = 0,
        UPDATE = 1
    }

    internal class StagedItem<TEntity>
    {
        public TEntity Entity { get; set; }
        public string PrimaryKeyName { get; set; }
        public ActionType Action { get; set; }

        public StagedItem(TEntity entity, string primaryKeyName, ActionType action)
        {
            Entity = entity;
            PrimaryKeyName = primaryKeyName;
            Action = action;
        }
    }
}