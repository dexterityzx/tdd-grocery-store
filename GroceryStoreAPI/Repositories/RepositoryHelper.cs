using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

        public static void Save(DataSchema data, string file)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            });

            WriteFile(file, json);
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

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> ToCollection<TEntity>(string jsonData)
        {
            DataSchema data = ToData(jsonData);
            return (IEnumerable<TEntity>)data
                .GetType()
                .GetProperty(ToCollectionName(typeof(TEntity).Name))
                .GetValue(data);
        }

        /// <summary>
        /// Make the entity name plrual, and return it as a collection name
        /// </summary>
        /// <param name="entityName">entityName</param>
        /// <returns>Returns a collection name</returns>
        public static string ToCollectionName(string entityName)
        {
            return entityName + "s";
        }
    }
}