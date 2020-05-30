using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface ICurrentRepository : IRepository<CurrentConfig>
    {
        CurrentConfig Get(int? id);

        CurrentConfig GetFirst();

        void Add(CurrentConfig entity);

        void Save(CurrentConfig entity);

        void Remove(CurrentConfig entity);

        void Update(CurrentConfig entity);

        IEnumerable<CurrentConfig> GetAll();

        CurrentConfig GetCurrentConfig();

        CurrentConfig GetCurrent(int? id);


    }
}
