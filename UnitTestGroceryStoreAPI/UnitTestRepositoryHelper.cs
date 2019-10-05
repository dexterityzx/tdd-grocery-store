using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using System.IO;
using System.Linq;
using Xunit;

namespace UnitTestGroceryStoreAPI
{
    public class UnitTestRepositoryHelper
    {
        private readonly string FILE_PATH;
        private readonly string FILE_NAME;

        public UnitTestRepositoryHelper()
        {
            FILE_NAME = $"database.json";
            FILE_PATH = "TestFiles";
        }

        [Fact]
        public async void TestReadFileAsync()
        {
            var jsonStr = await RepositoryHelper.ReadFileAsync(Path.Combine(FILE_PATH, FILE_NAME));
            var expectedJsonText = await TestFileReader.ReadFileAsync(FILE_NAME);
            Assert.Equal(expectedJsonText, jsonStr);
        }

        [Fact]
        public async void TestReadFile()
        {
            var jsonStr = RepositoryHelper.ReadFile(Path.Combine(FILE_PATH, FILE_NAME));
            var expectedJsonText = await TestFileReader.ReadFileAsync(FILE_NAME);
            Assert.Equal(expectedJsonText, jsonStr);
        }

        [Fact]
        public async void TestToEnumerable()
        {
            var jsonStr = await RepositoryHelper.ReadFileAsync(Path.Combine(FILE_PATH, FILE_NAME));
            var customers = RepositoryHelper.ToEnumerable<Customer>(jsonStr);
            Assert.Equal(3, customers.Count());

            var key = "1";
            var customer = customers.Where(c => c.Id == key).FirstOrDefault();
            Assert.Equal(customer.Id, key);
        }
    }
}