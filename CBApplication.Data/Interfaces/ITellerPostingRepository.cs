using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface ITellerPostingRepository : IRepository<TellerPosting>
    {
        TellerPosting Get(int? id);

        void Add(TellerPosting entity);

        void Save(TellerPosting entity);

        void Remove(TellerPosting entity);

        void Update(TellerPosting entity);

        IEnumerable<TellerPosting> GetAll();

        IEnumerable<TellerPosting> GetTellerPosting(int? id);

        TellerManagement GetTillUserId(int? id);

        int NumberOfTillAccounts();
    }
}
