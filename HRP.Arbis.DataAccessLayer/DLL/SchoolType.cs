using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class SchoolType :Base
    {
        private List<Contexts.Type> List()
        {
            var result = new List<Contexts.Type>();
            try
            {
                result = dbContext.Type.ToList();
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<List<Contexts.Type>> ListAsync()
        {
            return await Task.Run(() => this.List());
        }
    }
}
