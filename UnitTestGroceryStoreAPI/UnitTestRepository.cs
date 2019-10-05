using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
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

    public class TestOrderData : TheoryData<string, Order>
    {
        public TestOrderData()
        {
            Add("1", new Order()
            {
                Id = "1",
                CustomerId = "1",
                Items = new List<OrederItem>()
                {
                    new OrederItem()
                    {
                        ProductId = "1",
                        Quantity = 2
                    },
                    new OrederItem()
                    {
                        ProductId = "2",
                        Quantity = 3
                    }
                }
            });
        }
    }

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

        [Fact]
        public void RepositoryCanReadDataIntoProductObject()
        {
        }
    }
}