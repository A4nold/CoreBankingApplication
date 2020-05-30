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
    public class GlAccountLogic
    {
        private readonly IGlAccountRepository _context = new GlAccountRepository(new ApplicationDbContext());

        public long GenerateGlAccountCode(string Category)
        {

            long code = 0;
                var lastAcct = _context.GetCatOrder(Category);

                if (lastAcct != null)
                {
                    
                    code = lastAcct.AccCode + 1;
                }
                else                //this is going to be the first act in this category
                {
                    switch (Category)     //these codes are assumed at author's descretion
                    {
                        case "Asset":
                            code = 1100102119;
                            break;
                        case "Capital":
                            code = 3100102119;
                            break;
                        case "Expenses":
                            code = 5100102119;

                            break;
                        case "Income":
                            code = 4100102119;
                            break;
                        case "Liability":
                            code = 2100102119;
                            break;
                        default:
                            break;
                    }
                //end if
            }
            
            return code;

        }

        public bool IsNameExist(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var b = db.GlAccount.Where(a => a.Name == name).FirstOrDefault();
                return b != null;
            }
        }

        public int NumberOfGlAccounts()
        {
            var gl = _context.NumberOfGlAccounts();
            return gl;
        }

    }
}
