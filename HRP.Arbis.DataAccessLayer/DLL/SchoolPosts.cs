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
                result = dbContext.SchoolPosts.Include("Schools").Include("Category1").ToList();
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
                result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x=>x.Schools.District_Id==id).OrderBy(x=>x.Schools.Name).ToList();
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
                result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Schools.Id == id).OrderBy(x => x.Schools.Name).ToList();
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
        private List<Contexts.SchoolPosts> ListByCityId(int city_id)
        {
            var result = new List<Contexts.SchoolPosts>();
            try
            {                
                result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Schools.City_Id == city_id).ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public async Task<List<Contexts.SchoolPosts>> ListByCityIdAsync(int city_id)
        {
            return await Task.Run(() => this.ListByCityId(city_id));
        }
        private List<Contexts.SchoolPosts> ListByDistrictId(int dist_id,int city_id)
        {
            var result = new List<Contexts.SchoolPosts>();
            try
            {
                if (dist_id == 0)
                    result = this.ListByCityId(city_id);
                else
                    result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Schools.District_Id == dist_id).ToList();
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<List<Contexts.SchoolPosts>> ListByDistrictIdAsync(int dist_id,int city_id)
        {
            return await Task.Run(() => this.ListByDistrictId(dist_id, city_id));
        }
        private List<Contexts.SchoolPosts> ListByCategory(int city_id,int dist_id,string cat_id)
        {
            var result = new List<Contexts.SchoolPosts>();
            if (cat_id != "0")
            {
                if (dist_id != 0)
                {
                    if (city_id != 0)
                    {
                        result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Schools.City_Id == city_id && x.Category == cat_id).ToList();
                    }
                    else
                    {
                        result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Schools.City_Id == city_id && x.Schools.District_Id == dist_id && x.Category == cat_id).ToList();
                    }
                }
                if (city_id != 0)
                {
                    result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Schools.City_Id == city_id && x.Category == cat_id).ToList();
                }
                if (city_id == 0)
                {
                    result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Category == cat_id).ToList();
                }
                if(dist_id == 0)
                {
                    if (city_id != 0)
                    {
                        result = dbContext.SchoolPosts.Include("Schools").Include("Category1").Where(x => x.Schools.City_Id == city_id && x.Category == cat_id).ToList();
                    }                    
                }                
            }
            else
            {
                result = this.List();
            }
            return result;
        }
        public async Task<List<Contexts.SchoolPosts>> ListByCategoryAsync(int city,int dist,string cat)
        {
            return await Task.Run(() => this.ListByCategory(city, dist, cat));
        }
    }
}
