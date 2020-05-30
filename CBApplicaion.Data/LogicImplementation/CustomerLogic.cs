using CBApplication.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;

namespace CBApplicaion.Data.LogicImplementation
{
    public class CustomerLogic
    {
        private readonly ICustomerRepository _context = new CustomerRepository(new ApplicationDbContext());

        public void Save(Customer cust)
        {
            //Saving into the database
            _context.Add(cust);
            _context.Save(cust);
        }

        public Customer Get(int? id)
        {
            //Getting a customer by ID
            var get = _context.Get(id);
            return get;
        }

        public void Update(Customer cust)
        {
            //Updating customer new info
            _context.Save(cust);
        }

        public IEnumerable<Customer> GetAll()
        {
            //Getting a list of all customers
            var getAll = _context.GetAll();
            return getAll;
        }

        public void Remove(Customer cust)
        {
            //deleting a customer
            _context.Remove(cust);
            _context.Save(cust);
        }

        public string GenerateCustomerId()
        {
            var custId = _context.GetAll().OrderByDescending(b => b.id);
            var newIdString = "0000001";
            if (custId.Count() > 1)
            {
                var lastCustomerId = custId.First().customerId;
                var Id = Convert.ToInt32(lastCustomerId);
                var newIdInt = Id + 1;
                newIdString = newIdInt.ToString().PadLeft(8, '0');
            }

            return newIdString;
        }

        public int NumberOfCustomers()
        {
            var customer = _context.NumberOfCustomers();
            return customer;
        }
    }
}
