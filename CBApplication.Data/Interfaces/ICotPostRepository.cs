using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CBApplication.Core.Interfaces;
using CBApplication.Core.Models;

namespace CBApplication.Data.Interfaces
{
    public interface ICotPostRepository : IRepository<COTPost>
    {
        COTPost Get(int? id);

        IEnumerable<COTPost> GetAll();


        void Add(COTPost entity);

        void Save(COTPost entity);

        void Remove(COTPost entity);

        void Update(COTPost entity);

    }
}
