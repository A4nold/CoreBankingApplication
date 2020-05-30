using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;

namespace CBApplicaion.Data.LogicImplementation
{
    public class BankConfigLogic
    {
        private readonly BankConfigRepository _contextConfig = new BankConfigRepository(new ApplicationDbContext());

        public void CloseBusiness()
        {
            var GetbusinessConfig = _contextConfig.GetIsOpenFalse();
            var businessConfig = _contextConfig.Get(GetbusinessConfig.Id);

            businessConfig.IsOpen = false;

            _contextConfig.Update(businessConfig);
        }

    }
}
