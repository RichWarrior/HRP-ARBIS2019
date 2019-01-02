using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class SchoolPosts : Base
    {
        private List<Contexts.SchoolPosts> List()
        {
            var result = new List<Contexts.SchoolPosts>();
            try
            {
                result = dbContext.SchoolPosts.Include("School").ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public async Task<List<Contexts.SchoolPosts>> ListAsync()
        {
            return await Task.Run(()=>this.List());
        }
        private List<Contexts.SchoolPosts> ListByDistrictId(int id)
        {
            var result = new List<Contexts.SchoolPosts>();
            try
            {
                result = dbContext.SchoolPosts.Include("Schools").Where(x=>x.Schools.District_Id==id).OrderBy(x=>x.Schools.Name).ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public async Task<List<Contexts.SchoolPosts>> ListByDistrictIdAsync(int id)
        {
            return await Task.Run(() => this.ListByDistrictId(id));
        }
        public async Task<bool> Add(Contexts.SchoolPosts model)
        {
            var result = false;
            try
            {
                dbContext.SchoolPosts.Add(model);
                await dbContext.SaveChangesAsync();
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }
        private List<Contexts.SchoolPosts> ListById(string id)
        {
            var result = new List<Contexts.SchoolPosts>();
            try
            {
                result = dbContext.SchoolPosts.Include("Schools").Where(x => x.Schools.Id == id).OrderBy(x => x.Schools.Name).ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public async Task<List<Contexts.SchoolPosts>> ListByIdAsync(string id)
        {
            return await Task.Run(() => this.ListById(id));
        }
        public async Task<bool> Delete(string id)
        {
            var result = false;
            try
            {
                var value = dbContext.SchoolPosts.FirstOrDefault(x => x.Id == id);
                if(value!=null)
                {
                    dbContext.SchoolPosts.Remove(value);
                    await dbContext.SaveChangesAsync();
                    result = true;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
