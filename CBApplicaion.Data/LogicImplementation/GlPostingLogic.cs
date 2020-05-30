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
    public class GlPostingLogic
    {
        public bool CheckAccount(int debitAccount, int creditAccount)
        {
            if (debitAccount == creditAccount)
            {
                return true;
            }
            return false;
        }

        private readonly IGlPostingRepository _loan = new GlPostingRepository(new ApplicationDbContext());

        public void SavePost(GlPosting savePost)
        {
            //saving into the database
            _loan.Add(savePost);
            _loan.Save(savePost);
        }

        public void UpdatePost(GlPosting savePost)
        {
            //Updating database values
            _loan.Save(savePost);
        }

        public bool CreditPost(decimal amount, GlAccountManagement account, string acctcode)
        {
            switch (acctcode)
            {
                case "1":
                case "5":
                    account.AccountBalance -= amount;
                    break;
                case "2":
                case "3":
                case "4":
                    account.AccountBalance += amount;
                    break;
            }

            return true;
        }

        public bool DebitPost(decimal amount, GlAccountManagement account, string acctcode)
        {
            switch (acctcode)
            {
                case "1":
                case "5":
                    account.AccountBalance += amount;
                    break;
                case "2":
                case "3":
                case "4":
                    account.AccountBalance -= amount;
                    break;
            }

            return true;
        }
    }
}
