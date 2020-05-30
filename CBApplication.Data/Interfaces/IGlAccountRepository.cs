using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface IGlAccountRepository : IRepository<GlAccountManagement>
    {
        GlAccountManagement Get(int? id);

        GlAccountManagement GetFirst();

        IEnumerable<GlAccountManagement> GetAll();

        IEnumerable<GlAccountManagement> GetExpenseAccount();

        IEnumerable<GlAccountManagement> GetMainAccountCat(string type);

        //IEnumerable<GlAccountManagement> GetCapitalAccount();

        //IEnumerable<GlAccountManagement> GetLiabilityAccount();

        IEnumerable<GlAccountManagement> GetIncomeAccount();

        IEnumerable<Branch> GetBranches();

        IEnumerable<GlCategoryManagement> GetAllGlCategories();

        IEnumerable<MainAccountCategory> GetAllMainAccountCategories();

        GlAccountManagement GetVault();

        void Add(GlAccountManagement entity);

        void Save(GlAccountManagement entity);

        GlAccountManagement GetCatName(string entity);

        GlAccountManagement GetTillAccount(int? id);

        GlAccountManagement GetCatOrder(string entity);

        int NumberOfGlAccounts();
    }
}
