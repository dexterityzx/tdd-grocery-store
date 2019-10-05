using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Repositories
{
    public static class RepositoryHelper
    {
        public static async Task<string> ReadFileAsync(string file)
        {
            using (var reader = File.OpenText(file))
            {
                var fileText = await reader.ReadToEndAsync();
                return fileText;
            }
        }

        public static string ReadFile(string file)
        {
            using (var reader = File.OpenText(file))
            {
                var fileText = reader.ReadToEnd();
                return fileText;
            }
        }

        public static IEnumerable<TEntity> ToEnumerable<TEntity>(string jsonData)
        {
            DataSchema data = JsonConvert.DeserializeObject<DataSchema>(jsonData);

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