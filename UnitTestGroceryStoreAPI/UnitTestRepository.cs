using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestGroceryStoreAPI.TestData;
using Xunit;

namespace UnitTestGroceryStoreAPI
{
    public class UnitTestRepository
    {
        private CustomerRepository _customerRepository;
        private OrderRepository _orderRepository;
        private ProductRepository _productRepository;

        public UnitTestRepository()
        {
            _customerRepository = new CustomerRepository(Constants.DB_FILE);
            _orderRepository = new OrderRepository(Constants.DB_FILE);
            _productRepository = new ProductRepository(Constants.DB_FILE);
        }

        [Theory]
        [ClassData(typeof(TestCustomerData))]
        public void CustomerRepositoryCanRetrieveCustomerByKey(int key, Customer expectedCustomer)
        {
            Customer actualCustomer = _customerRepository.Key(key);
            var jsonActual = JsonConvert.SerializeObject(actualCustomer);
            var jsonExpected = JsonConvert.SerializeObject(expectedCustomer);
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Theory]
        [ClassData(typeof(TestCustomerData))]
        public void CustomerRepositoryCanCheckExistenceByKey(int key, Customer expectedCustomer)
        {
            Assert.True(_customerRepository.Exist(key));
        }

        [Theory]
        [ClassData(typeof(TestCustomerDataAdd))]
        public void CustomerRepositoryCanAddCustomer(Customer customerToCreate, string customerName)
        {
            _customerRepository.Add(customerToCreate);
            _customerRepository.Save();
            var savedCustomer = _customerRepository.Key(customerToCreate.Id);
            Assert.Equal(customerName, savedCustomer.Name);
        }

        [Theory]
        [ClassData(typeof(TestCustomerDataUpdate))]
        public void CustomerRepositoryCanUpdateCustomer(Customer customerToUpdate, string customerName)
        {
            _customerRepository = new CustomerRepository(Constants.DB_FILE_UPDATE);
            _customerRepository.Update(customerToUpdate);
            _customerRepository.Save();
            var updatedCustomer = _customerRepository.Key(customerToUpdate.Id);
            Assert.Equal(customerName, updatedCustomer.Name);
        }

        [Theory]
        [ClassData(typeof(TestOrderData))]
        public void OrderRepositoryCanRetrieveOrderByKey(string key, Order expectedOrder)
        {
            Order actualOrder = _orderRepository.Key(key);
            var jsonActual = JsonConvert.SerializeObject(actualOrder);
            var jsonExpected = JsonConvert.SerializeObject(expectedOrder);
            Assert.Equal(jsonExpected, jsonActual);
        }

        [Theory]
        [ClassData(typeof(TestOrderDataByDate))]
        public void OrderRepositoryCanRetrieveOrderByDate(string inputDate, string expectedDate)
        {
            if (DateTime.TryParse(inputDate, out DateTime date))
            {
                List<Order> actualOrder = _orderRepository.GetByDate(inputDate).ToList();
                actualOrder.ForEach(order =>
                {
                    Assert.Equal(expectedDate, order.Date);
                });
            }
            else
            {
                // false to parse input date
                Assert.True(false);
            }
        }

        [Theory]
        [ClassData(typeof(TestOrderDataByCustomerIdData))]
        public void OrderRepositoryCanRetrieveOrderByCustomerId(int customerId, List<int> expectedOrderIds)
        {
            List<Order> actualOrders = _orderRepository.GetByCustomerId(customerId).ToList();
            actualOrders.ForEach(order =>
            {
                Assert.True(expectedOrderIds.Exists(id => id == order.Id));
            });
        }

        [Theory]
        [ClassData(typeof(TestProductData))]
        public void ProductRepositoryCanRetrieveProductByKey(string key, Product expectedProduct)
        {
            Product actualProduct = _productRepository.Key(key);
            var jsonActual = JsonConvert.SerializeObject(actualProduct);
            var jsonExpected = JsonConvert.SerializeObject(expectedProduct);
            Assert.Equal(jsonExpected, jsonActual);
        }
    }
}