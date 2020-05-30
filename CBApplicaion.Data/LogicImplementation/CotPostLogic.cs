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
    public class CotPostLogic
    {
        private readonly ICotPostRepository _cot = new CotPostRepository(new ApplicationDbContext());

        public void SavePost(COTPost savePost)
        {
            //saving into the database
            _cot.Add(savePost);
            _cot.Save(savePost);
        }
    }
}
