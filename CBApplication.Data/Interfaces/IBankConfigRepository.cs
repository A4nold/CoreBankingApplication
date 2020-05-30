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
    public interface IBankConfigRepository : IRepository<BankConfig>
    {
        BankConfig Get(int? id);

        //IEnumerable<SavingsConfig> GetAll();

        //IEnumerable<SavingsConfig> Find(Expression<Func<GlAccountManagement, bool>> predicate);

        void Add(BankConfig entity);

        void Save(BankConfig entity);

        void Remove(BankConfig entity);

        void Update(BankConfig entity);

        //SavingsConfig GetSavingsConfig();
    }
}
