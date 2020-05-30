using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;

namespace CBApplication.Data.Implementations
{
    public class BranchRepository:Repository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
