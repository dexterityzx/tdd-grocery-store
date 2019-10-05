using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Newtonsoft.Json;
using Xunit;

namespace UnitTestGroceryStoreAPI
{
    public class TestCustomerData : TheoryData<string, Customer>
    {
        public TestCustomerData()
        {
            Add("1", new Customer()
            {
                Id = "1",
                Name = "Bob"
            });
            Add("2", new Customer()
            {
                Id = "2",
                Name = "Mary"
            });
            Add("3", new Customer()
            {
                Id = "3",
                Name = "Joe"
            });
        }
    }

    public class UnitTestRepository
    {
        private CustomerRepository _customerRepository;

        public UnitTestRepository()
        {
            _customerRepository = new CustomerRepository(Constants.DB_FILE);
        }

        [Theory]
        [ClassData(typeof(TestCustomerData))]
        public void RepositoryCanReadDataIntoCustomerObject(string key, Customer expectedCustomer)
        {
            Customer actualCustomer = _customerRepository.Key(key);
            var jsonActual = JsonConvert.SerializeObject(actualCustomer);
            var jsonExpected = JsonConvert.SerializeObject(expectedCustomer);
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Fact]
        public void RepositoryCanReadDataIntoOrderObject()
        {
        }

        [Fact]
        public void RepositoryCanReadDataIntoProductObject()
        {
        }
    }
}