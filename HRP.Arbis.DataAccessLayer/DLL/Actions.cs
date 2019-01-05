using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class Actions : Base
    {
        private List<Contexts.SP_ACTION_Result> List()
        {
            var result = new List<Contexts.SP_ACTION_Result>();
            try
            {
                result = dbContext.SP_ACTION().ToList();
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<List<Contexts.SP_ACTION_Result>> ListAsync()
        {
            return await Task.Run(() => this.List());
        }
        public async Task<bool> Add(Contexts.Actions model)
        {
            var result = false;
            try
            {
                dbContext.Actions.Add(model);
                await dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
