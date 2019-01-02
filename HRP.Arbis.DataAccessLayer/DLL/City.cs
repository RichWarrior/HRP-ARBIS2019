using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class City :Base
    {
        private List<Contexts.City> List()
        {
            var result = new List<Contexts.City>();
            try
            {
                result = dbContext.City.Include("District")
                    .OrderBy(x=>x.Id).ToList();
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<List<Contexts.City>> ListByAsync()
        {
            return await Task.Run(() => this.List());
        }       
    }
}
