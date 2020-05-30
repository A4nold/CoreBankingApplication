using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface IGlPostingRepository : IRepository<GlPosting>
    {
        GlPosting Get(int? id);

        void Add(GlPosting entity);

        void Save(GlPosting entity);

        void Remove(GlPosting entity);

        void Update(GlPosting entity);

        IEnumerable<GlPosting> GetAll();

        IEnumerable<GlPosting> GetAllPosting();

    }
}
