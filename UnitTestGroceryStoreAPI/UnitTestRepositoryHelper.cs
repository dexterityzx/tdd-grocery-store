using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using System.Linq;
using UnitTestGroceryStoreAPI.Helpers;
using Xunit;

namespace UnitTestGroceryStoreAPI
{
    public class UnitTestRepositoryHelper
    {
        public UnitTestRepositoryHelper()
        {
        }

        [Fact]
        public async void TestReadFileAsync()
        {
            var jsonStr = await RepositoryHelper.ReadFileAsync(Constants.DB_FILE);
            var expectedJsonText = await TestFileReader.ReadFileAsync(Constants.DB_FILE);
            Assert.Equal(expectedJsonText, jsonStr);
        }

        [Fact]
        public async void TestReadFile()
        {
            var jsonStr = RepositoryHelper.ReadFile(Constants.DB_FILE);
            var expectedJsonText = await TestFileReader.ReadFileAsync(Constants.DB_FILE);
            Assert.Equal(expectedJsonText, jsonStr);
        }

        [Fact]
        public async void TestToEnumerable()
        {
            var jsonStr = await RepositoryHelper.ReadFileAsync(Constants.DB_FILE);
            var customers = RepositoryHelper.ToEnumerable<Customer>(jsonStr);
            Assert.Equal(3, customers.Count());

            var key = "1";
            var customer = customers.Where(c => c.Id == key).FirstOrDefault();
            Assert.Equal(customer.Id, key);
        }
    }
}