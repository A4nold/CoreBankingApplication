using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Migrations;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class GlAccountRepository : Repository<GlAccountManagement>, IGlAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public GlAccountRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<GlAccountManagement> GetExpenseAccount()
        {
            var result = _context.GlAccount.Include(a => a.MainAccountCategory)
                .Where(a => a.MainAccountCategoryId == "Expenses");

            return result;
        }


        public IEnumerable<GlAccountManagement> GetPayableAccount()
        {
            var result = _context.GlAccount.Include(a => a.MainAccountCategory)
                .Where(a => a.MainAccountCategoryId == "Liability");

            return result;
        }
        public IEnumerable<GlAccountManagement> GetIncomeAccount()
        {
            var result = _context.GlAccount.Include(a => a.MainAccountCategory)
                .Where(a => a.MainAccountCategoryId == "Income");

            return result;
        }

        public IEnumerable<GlAccountManagement> GetMainAccountCat(string type)
        {
            return _context.GlAccount.Include(a => a.MainAccountCategory)
                .Where(a => a.MainAccountCategoryId == type).ToList();
        }

        //public IEnumerable<GlAccountManagement> GetLiabilityAccount()
        //{
        //    return _context.GlAccount.Include(a => a.MainAccountCategory)
        //        .Where(a => a.MainAccountCategoryId == "Liability").ToList();
        //}

        //public IEnumerable<GlAccountManagement> GetCapitalAccount()
        //{
        //    return _context.GlAccount.Include(a => a.MainAccountCategory)
        //        .Where(a => a.MainAccountCategoryId == "Capital").ToList();
        //}

        public IEnumerable<Branch> GetBranches()
        {
            return _context.Set<Branch>().ToList();
        }

        public IEnumerable<GlCategoryManagement> GetAllGlCategories()
        {
            return _context.GlCategory.ToList();
        }


        public IEnumerable<MainAccountCategory> GetAllMainAccountCategories()
        {
            return _context.MainAccountCategories.ToList();
        }

        public GlAccountManagement GetCatName(string entity)
        {
            var result = _context.GlAccount.FirstOrDefault(f => f.CategoryManagementId == entity);
            return result;
        }

        public GlAccountManagement GetTillAccount(int? id)
        {
            var result = _context.GlAccount.FirstOrDefault(a => a.Id == id);
            return result;
        }

        public IEnumerable<GlAccountManagement> GetAllUnassignedUsers()
        {
            var result = _context.GlAccount.Where(u => u.AccountAssigned == false).ToList();
            return result;
        }

        public GlAccountManagement GetCatOrder(string entity)
        {
            var result = _context.GlAccount.Where(f => f.MainAccountCategoryId == entity).OrderByDescending(a => a.Id)
                .FirstOrDefault();


            return result;
        }

        public int NumberOfGlAccounts()
        {
            var gl = _context.GlAccount.Count();
            return gl;
        }

        public GlAccountManagement GetVault()
        {
            var vaultCode = Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["Vault"]);
            var result = _context.GlAccount.FirstOrDefault(m => m.AccCode == vaultCode);
            return result;
        }
    }
}
