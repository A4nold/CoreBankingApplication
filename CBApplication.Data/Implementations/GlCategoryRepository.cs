using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Models;
using CBApplication.Data.Interfaces;
using System.Data.Entity;

namespace CBApplication.Data.Implementations
{
    public class GlCategoryRepository : Repository<GlCategoryManagement>, IGlCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public GlCategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<MainAccountCategory> GetAllMainAccountCategories()
        {
            return _context.Set<MainAccountCategory>().ToList();
        }

        public GlCategoryManagement any(string category)
        {
            var result = _context.GlCategory.FirstOrDefault(f => f.Name == category);
            return result;
        }

        public string GetMainCat(int entity)
        {
            var result = _context.GlCategory.FirstOrDefault(f => f.Id == entity);
            var value = result.MainAccountCategoryId;
            return value;
        }

        public string GetCategoryName(int entity)
        {
            var result = _context.GlCategory.FirstOrDefault(f => f.Id == entity);
            var value = result.Name;
            return value;
        }

        
    }
}
