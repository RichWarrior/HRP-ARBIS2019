using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer.DLL
{
    public class Districts : Base
    {
        private List<Contexts.District> ListByCity(int city_Id)
        {
            var result = new List<Contexts.District>();
            try
            {
                result = dbContext.District.Where(x => x.City_Id == city_Id)
                    .OrderBy(x=>x.Id).ToList();
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<List<Contexts.District>> ListByCityIdAsync(int city_ıd)
        {
            return await Task.Run(() => this.ListByCity(city_ıd));
        }
    }
}
