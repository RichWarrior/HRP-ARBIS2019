using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class Category : Base
    {
        private List<Contexts.Category> List()
        {
            var result = new List<Contexts.Category>();
            try
            {
                result = dbContext.Category.ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public async Task<List<Contexts.Category>> ListAsync()
        {
            return await Task.Run(() => this.List());
        }
    }
}
