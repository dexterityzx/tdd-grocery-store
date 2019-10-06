using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using System.Linq;
using System.Threading.Tasks;
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
        public async void TestToCollection()
        {
            var jsonStr = await RepositoryHelper.ReadFileAsync(Constants.DB_FILE);
            var customers = RepositoryHelper.ToCollection<Customer>(jsonStr);
            Assert.True(customers.Count() > 0);

            var customer = customers.Where(c => c.Id == 1).FirstOrDefault();
            Assert.Equal(1, customer.Id);
        }

        [Fact]
        public async void TestWriteFile()
        {
            var expectedJsonText = await TestFileReader.ReadFileAsync(Constants.DB_FILE_UPDATE_HELPER);

            RepositoryHelper.WriteFile(Constants.DB_FILE_UPDATE_HELPER, expectedJsonText);
            var actualJsonText = RepositoryHelper.ReadFile(Constants.DB_FILE_UPDATE_HELPER);

            Assert.Equal(expectedJsonText, actualJsonText);
        }

        [Fact]
        public async void TestWriteFileMutipleThread()
        {
            var expectedJsonText = await TestFileReader.ReadFileAsync(Constants.DB_FILE);

            var tasks = new Task[10];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => RepositoryHelper.WriteFile(Constants.DB_FILE_UPDATE, expectedJsonText));
            }
            Task.WaitAll(tasks);

            var actualJsonText = RepositoryHelper.ReadFile(Constants.DB_FILE_UPDATE);
            Assert.Equal(expectedJsonText, actualJsonText);
        }
    }
}