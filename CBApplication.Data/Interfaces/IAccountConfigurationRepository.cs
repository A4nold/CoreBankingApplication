﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface IAccountConfigurationRepository : IRepository<SavingsConfig>
    {
        SavingsConfig Get(int? id);

        IEnumerable<SavingsConfig> GetAll();

        IEnumerable<SavingsConfig> Find(Expression<Func<GlAccountManagement, bool>> predicate);

        void Add(SavingsConfig entity);

        void Save(SavingsConfig entity);

        void Remove(SavingsConfig entity);

        void Update(SavingsConfig entity);

        SavingsConfig GetSavingsConfig();

        SavingsConfig GetFirst();
    }
}
