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
    public class CurrentConfigLogic
    {
        private readonly ICurrentRepository _current = new CurrentRepository(new ApplicationDbContext());
        private readonly IAccountConfigurationRepository _savings = new AccountConfigurationRepository(new ApplicationDbContext());
        private readonly ILoanRepository _loan = new LoanRepository(new ApplicationDbContext());

        public void SaveCurrentConfig(CurrentConfig config)
        {
            //Saving into the database
            _current.Add(config);
            _current.Save(config);
        }

        public void UpdateCurrentConfig(CurrentConfig config)
        {
            _current.Update(config);
        }

        public CurrentConfig GetCurrent(int? id)
        {
            var result = _current.GetCurrent(id);
            return result;
        }

        public bool checkConfig()
        {
            var savings = _savings.GetFirst();
            var loan = _loan.GetFirst();
            var current = _current.GetFirst();

            if (savings == null || loan == null || current == null)
            {
                return false;
            }

            return true;
        }

    }
}
