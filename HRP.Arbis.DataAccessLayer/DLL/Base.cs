using HRP.Arbis.DataAccessLayer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class Base
    {
        public HRP_ARBIS2019Entities dbContext { get; set; }

        public Base()
        {
            dbContext = new HRP_ARBIS2019Entities();
            dbContext.Configuration.LazyLoadingEnabled = false;
        }
    }
}
