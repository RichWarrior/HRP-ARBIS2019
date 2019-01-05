using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
     public class School :Base
    {
        private List<Contexts.Schools> ListBySync()
        {
            var result = new List<Contexts.Schools>();
            try
            {
                result = dbContext.Schools.ToList();                
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<List<Contexts.Schools>> ListByAsync()
        {
            return await Task.Run(() => this.ListBySync());
        }
        public async Task<bool> Delete(string id)
        {
            var result = false;
            try
            {
                var user = dbContext.Schools.FirstOrDefault(x => x.Id == id);
                if(user!=null)
                {                    
                    dbContext.Schools.Remove(user);
                    await dbContext.SaveChangesAsync();
                    result = true;
                }
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        private Contexts.Schools FindById(string id)
        {
            Contexts.Schools result = null;
            try
            {
                result = dbContext.Schools.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<Contexts.Schools> FindByIdAsync(string Id)
        {
            return await Task.Run(() => this.FindById(Id));
        }

        public async Task<bool> Add(Contexts.Schools model)
        {
            var result = false;
            try
            {
                dbContext.Schools.Add(model);
                await dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<bool> Update(string id,Contexts.Schools model)
        {
            var result = false;
            try
            {
                var user = dbContext.Schools.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    user.City_Id = model.City_Id;
                    user.District_Id = model.District_Id;
                    user.Phone_Number = model.Phone_Number;
                    user.Address = model.Address;
                    await dbContext.SaveChangesAsync();
                    result = true;
                }              
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        private List<Contexts.Schools> ListByDistrict(int dist_id)
        {
            var result = new List<Contexts.Schools>();
            try
            {
                result = dbContext.Schools.Where(x => x.District_Id == dist_id)
                    .OrderBy(x=>x.Name).ToList();
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<List<Contexts.Schools>> ListByDistrictAsync(int dist_id)
        {
            return await Task.Run(() => this.ListByDistrict(dist_id));
        }
    }
}
