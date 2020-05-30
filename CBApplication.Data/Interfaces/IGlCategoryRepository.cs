using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
   public interface IGlCategoryRepository : IRepository<GlCategoryManagement>
    {
        //GlAccountManagement Get(int? id);

        IEnumerable<GlCategoryManagement> GetAll();

        IEnumerable<MainAccountCategory> GetAllMainAccountCategories();

        void Add(GlCategoryManagement entity);

        void Save(GlCategoryManagement entity);


        //GlCategoryManagement any(string category);
        //void Remove(GlAccountManagement entity);
    }
}
