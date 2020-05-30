using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Implementations;
using CBApplication.Data.Interfaces;

namespace CBApplicaion.Data.LogicImplementation
{
    public class GlCategoryLogic
    {
        private readonly IGlCategoryRepository db = new GlCategoryRepository(new ApplicationDbContext());


        public bool IsGlNameExist(string name)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var b = db.GlCategory.FirstOrDefault(m => m.Name == name);
                return b != null;
            }
        }
    }
}
