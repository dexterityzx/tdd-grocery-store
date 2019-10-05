using GroceryStoreAPI.Entities;
using GroceryStoreAPI.Repositories;
using System.Collections.Generic;
using Xunit;

namespace UnitTestGroceryStoreAPI
{
    public class UnitTestRepository
    {
        private CustomerRepository _customerRepository;

        public UnitTestRepository()
        {
            _customerRepository = new CustomerRepository();
        }

        //[Theory]
        //[MemberData(nameof(CustomerData))]
        //public void RepositoryCanReadDataIntoCustomerObject(Customer expectedCustomer)
        //{
        //    var customer = _customerRepository.Id(1);
        //    Assert.Equal(customer.Id, expectedCustomer.Id);
        //    Assert.Equal(customer.Name, expectedCustomer.Name);
        //}

        public static IEnumerable<Customer> CustomerData =>
        new List<Customer>
        {
            new Customer() {
                Name = "Bob",
                Id = "1"
            },
        };

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