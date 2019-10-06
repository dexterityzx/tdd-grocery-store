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

        /// <summary>
        /// Reads a file into string
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns>string of the file</returns>
        public static string ReadFile(string file)
        {
            using (var reader = File.OpenText(file))
            {
                var fileText = reader.ReadToEnd();
                return fileText;
            }
        }

        /// <summary>
        /// Reads a file into string async
        /// </summary>
        /// <param name="file">file path</param>
        /// <returns>string of the file</returns>
        public static async Task<string> ReadFileAsync(string file)
        {
            using (var reader = File.OpenText(file))
            {
                var fileText = await reader.ReadToEndAsync();
                return fileText;
            }
        }

        /// <summary>
        /// Saves the data into the file in JSON
        /// </summary>
        /// <param name="data">data to save</param>
        /// <param name="file">file path</param>
        public static void Save(DataSchema data, string file)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            string json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = contractResolver, // make output camel case
                Formatting = Formatting.Indented //format the json file
            });

            WriteFile(file, json);
        }

        /// <summary>
        /// Thread safe function to write a string into a file.
        /// </summary>
        /// <param name="file">file path</param>
        /// <param name="str">string to write to the file</param>
        public static void WriteFile(string file, string str)
        {
            _lock.EnterWriteLock();
            try
            {
                using (StreamWriter writer = new StreamWriter(file, false))
                {
                    writer.Write(str);
                }
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// Transfers json string into DataSchema object
        /// </summary>
        /// <param name="jsonData">json string</param>
        /// <returns></returns>
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
        /// Makes the entity name plrual, and return it as a collection name
        /// </summary>
        /// <param name="entityName">entityName</param>
        /// <returns>Returns a collection name</returns>
        public static string ToCollectionName(string entityName)
        {
            return entityName + "s";
        }
    }
}