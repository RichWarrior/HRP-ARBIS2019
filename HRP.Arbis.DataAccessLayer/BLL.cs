using HRP.Arbis.DataAccessLayer.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HRP.Arbis.DataAccessLayer
{
    public class BLL
    {
        private ServerInfo _serverInfo { get; set; }
        private Menu _Menu { get; set; }
        private School _school { get; set; }
        private City _city { get; set; }
        private Districts _districts { get; set; }
        private SchoolType _type { get; set; }
        private SchoolPosts _posts { get; set; }
        private Actions _actions { get; set; }

        public ServerInfo ServerInfo()
        {
            if (_serverInfo == null)
                _serverInfo = new ServerInfo();
            return _serverInfo;
        }

        public Menu Menu()
        {
            if (_Menu == null)
                _Menu = new Menu();
            return _Menu;
        }

        public School School()
        {
            if (_school == null)
                _school = new School();
            return _school;
        }

        public City City()
        {
            if (_city == null)
                _city = new City();
            return _city;
        }

        public Districts Districts()
        {
            if (_districts == null)
                _districts = new Districts();
            return _districts;
        }

        public SchoolType SType()
        {
            if (_type == null)
                _type = new SchoolType();
            return _type;
        }

        public SchoolPosts SchoolPosts()
        {
            if (_posts == null)
                _posts = new SchoolPosts();
            return _posts;
        }

        public Actions Action()
        {
            if (_actions == null)
                _actions = new Actions();
            return _actions;
        }
       

        public async Task<T> GetServerParameter<T>()
        {
            var result = (T)Activator.CreateInstance(typeof(T));
            try
            {
                var _properties = result.GetType().GetProperties();
                var serverInfo = await this.ServerInfo().ListByAsync();
                var _instance = (T)Activator.CreateInstance(typeof(T));
                foreach (var item in serverInfo)
                {
                    foreach (var sb in _properties)
                    {
                        if(item.key_str == sb.Name)
                        {
                            if (sb.PropertyType == typeof(int))
                                sb.SetValue(_instance, Convert.ToInt32(item.value_str), null);
                            else
                                sb.SetValue(_instance, item.value_str, null);
                        }
                    }
                }
                result = _instance;
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        //public string HashPassword(string hash)
        //{
        //    byte[] salt;
        //    byte[] bytes;
        //    if (hash == null)
        //    {
        //        throw new ArgumentNullException("password");
        //    }
        //    using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(hash, 16, 1000))
        //    {
        //        salt = rfc2898DeriveByte.Salt;
        //        bytes = rfc2898DeriveByte.GetBytes(32);
        //    }
        //    byte[] numArray = new byte[49];
        //    Buffer.BlockCopy(salt, 0, numArray, 1, 16);
        //    Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
        //    return Convert.ToBase64String(numArray);
        //}
    }
}
