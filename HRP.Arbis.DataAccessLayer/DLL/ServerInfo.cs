using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class ServerInfo : Base
    {
        private List<Contexts.ServerInfo> ListSync()
        {
            var result = new List<Contexts.ServerInfo>();
            try
            {
                result = dbContext.ServerInfo.ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public async Task<List<Contexts.ServerInfo>> ListByAsync()
        {
            return await Task.Run(() => this.ListSync());
        }
        public async Task<bool> Delete(string ıd)
        {
            var result = false;
            try
            {
                var value = dbContext.ServerInfo.FirstOrDefault(x => x.Id == ıd);
                if (value != null)
                {
                    dbContext.ServerInfo.Remove(value);
                    await dbContext.SaveChangesAsync();
                    result = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        private Contexts.ServerInfo FindById(string id)
        {
            Contexts.ServerInfo result = null;
            try
            {
                result = dbContext.ServerInfo.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<Contexts.ServerInfo> FindByIdAsync(string id)
        {
            return await Task.Run(() => this.FindById(id));
        }
        public async Task<bool> Update(string id,Contexts.ServerInfo model)
        {
            var result = false;
            try
            {
                var value = dbContext.ServerInfo.FirstOrDefault(x => x.Id == id);
                if(value!=null)
                {
                    value.key_str = model.key_str;
                    value.value_str = model.value_str;
                    await dbContext.SaveChangesAsync();
                    result = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<bool> Add(Contexts.ServerInfo model)
        {
            var result = false;
            try
            {
                dbContext.ServerInfo.Add(model);
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
