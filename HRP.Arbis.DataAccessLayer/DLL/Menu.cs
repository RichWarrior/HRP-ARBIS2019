using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class Menu :Base
    {
        private List<Contexts.SP_GETMENU_Result> ListByRole(string role_id)
        {
            var result = new List<Contexts.SP_GETMENU_Result>();
            try
            {
                result = dbContext.SP_GETMENU(role_id).OrderBy(x=>x.Rank).ToList();
            }
            catch (Exception ex)
            {
                
            }
            return result;
        }
        public async Task<List<Contexts.SP_GETMENU_Result>> ListByRoleAsync(string role_id)
        {
            return await Task.Run(() => this.ListByRole(role_id));
        }
    }
}
