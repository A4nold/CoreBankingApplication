using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;

namespace CBApplicaion.Data.LogicImplementation
{
    public class TransactionLogLogic
    {
        private readonly TransactionLogRepository _context = new TransactionLogRepository(new ApplicationDbContext());


        public void Log(GlAccountManagement gl, decimal amount, TransactionType type)
        {
            var transaction = new TransactionsLog
            {
                Name = gl.Name,
                Amount = amount,
                Type = type,
                AccountCode = gl.AccCode,
                date = DateTime.Now,
                MainAccountCategory = GetCategory(gl)
            };

            _context.Add(transaction);
            _context.Save(transaction);
        }

        public string GetCategory(GlAccountManagement gl)
        {
            var acctcode = gl.AccCode.ToString();

            if (acctcode.StartsWith("1"))
            {
                return "Asset";
            }else if (acctcode.StartsWith("2"))
            {
                return "Liability";
            }
            else if (acctcode.StartsWith("3"))
            {
                return "Capital";
            }
            else if (acctcode.StartsWith("4"))
            {
                return "Income";
            }
            else if (acctcode.StartsWith("5"))
            {
                return "Expenses";
            }
            else
            {
                return "";
            }

        }

        public void LogCustomer(CustomerAccount customer, decimal amount, TransactionType type)
        {
            if (customer.AccountType == AccountType.Loan)
            {
                var transaction = new TransactionsLog
                {
                    Name = customer.AccountName,
                    Amount = amount,
                    AccountCode = customer.AccountNumber,
                    Type = type,
                    date = DateTime.Now,
                    MainAccountCategory = "Asset"
                };

                _context.Add(transaction);
                _context.Save(transaction);
            }
            else
            {
                var transaction = new TransactionsLog
                {
                    Name = customer.AccountName,
                    Amount = amount,
                    Type = type,
                    date = DateTime.Now,
                    MainAccountCategory = "Liability"
                };

                _context.Add(transaction);
                _context.Save(transaction);
            }

        }

        public List<TransactionsLog> GetDebitLog()
        {
            var values = _context.GetDebitLogs();
            return values;
        }
    }
}
