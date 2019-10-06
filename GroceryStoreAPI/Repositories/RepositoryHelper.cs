using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Repositories
{
    public static class RepositoryHelper
    {
        private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public static string ReadFile(string file)
        {
            using (var reader = File.OpenText(file))
            {
                var fileText = reader.ReadToEnd();
                return fileText;
            }
        }

        public static async Task<string> ReadFileAsync(string file)
        {
            using (var reader = File.OpenText(file))
            {
                var fileText = await reader.ReadToEndAsync();
                return fileText;
            }
        }

        public static void Save(DataSchema data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            WriteFile(Constants.DB_FILE, json);
        }

        public static void WriteFile(string file, string json)
        {
            _lock.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(file, false))
                {
                    writer.Write(json);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public static DataSchema ToData(string jsonData)
        {
            return JsonConvert.DeserializeObject<DataSchema>(jsonData);
        }

        public static IEnumerable<TEntity> ToDataSet<TEntity>(string jsonData)
        {
            DataSchema data = ToData(jsonData);

            var entityName = typeof(TEntity).Name;
            switch (entityName)
            {
                case Constants.CUSTOMER:
                    return (IEnumerable<TEntity>)data.Customers;

                case Constants.ORDER:
                    return (IEnumerable<TEntity>)data.Orders;

                case Constants.PRODUCT:
                    return (IEnumerable<TEntity>)data.Products;

                default:
                    return null;
            }
        }
    }
}