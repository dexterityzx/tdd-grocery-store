using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Newtonsoft.Json;
using UnitTestGroceryStoreAPI.TestData;
using Xunit;

namespace UnitTestGroceryStoreAPI
{
    public class UnitTestRepository
    {
        private CustomerRepository _customerRepository;
        private OrderRepository _orderRepository;

        public UnitTestRepository()
        {
            _customerRepository = new CustomerRepository(Constants.DB_FILE);
            _orderRepository = new OrderRepository(Constants.DB_FILE);
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

        [Theory]
        [ClassData(typeof(TestOrderData))]
        public void RepositoryCanReadDataIntoOrderObject(string key, Order expectedOrder)
        {
            Order actualOrder = _orderRepository.Key(key);
            var jsonActual = JsonConvert.SerializeObject(actualOrder);
            var jsonExpected = JsonConvert.SerializeObject(expectedOrder);
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Theory]
        [ClassData(typeof(TestProductData))]
        public void RepositoryCanReadDataIntoProductObject(string key, Product expectedOrder)
        {
        }
    }
}