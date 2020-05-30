using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;
using CBApplication.Data.Interfaces;

namespace CBApplicaion.Data.LogicImplementation
{
    public class CustomerAccountLogic
    {
        private readonly ICustomerAccountRepository _customer = new CustomerAccountRepository(new ApplicationDbContext());

        public long generateAccountNumber(string type, string customerid)
        {
            var firstnumber = 0;
            switch (type)
            {
                case "Savings":
                    firstnumber = 1;
                    break;
                case "Current":
                    firstnumber = 2;
                    break;
                case "Loan":
                    firstnumber = 3;
                    break;
                default:
                    firstnumber = 4;
                    break;
            }

            var randomNumber = new Random().Next(10, 99);
            return Convert.ToInt64(firstnumber + customerid + randomNumber);
        }

        public void SavePost(CustomerAccount savePost)
        {
            //saving into the database
            _customer.Add(savePost);
            _customer.Save(savePost);
        }
        public void UpdatePost(CustomerAccount savePost)
        {
            //updating the database
            _customer.Update(savePost);
        }

        public int NumberOfCustomerAccounts()
        {
            var customer = _customer.NumberOfCustomerAccounts();
            return customer;
        }
    }
}
