using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Entity_new;

namespace DAL
{
    public class AuthorizationDAL
    {

        private static SHA512 hashAlgo = SHA512.Create();

        private static byte[] GetStringHash(string s)
        {
            if (s == null)
                return null;
            byte[] hash = hashAlgo.ComputeHash(Encoding.Unicode.GetBytes(s));
            return hash;
        }

        public void AddOrUpdate(Entity_new.Authorization auth)
        {

            using (bakaContext data = new bakaContext())
            {
                var t = data.Users.FirstOrDefault(item => item.Name == auth.Name);
                if (t == null)
                {

                    data.Users.Add(new User()
                    {
                        Name = auth.Name,
                        Password = GetStringHash(auth.Password)
                    });
                }
                else
                {
                    t.Name = auth.Name;
                    t.Password = GetStringHash(auth.Password);
                }
                data.SaveChanges();
            }
        }

        public Entity_new.Authorization Get(string Name)
        {
            using (bakaContext data = new bakaContext())
            {
                var t = data.Users.FirstOrDefault(item => Name == item.Name);
                if (t == null)
                    return null;
                Authorization en = new Authorization();
                en.Id = t.Id;
                en.Name = t.Name;
                en.HashPassword = t.Password;

                return en;
            }

        }
    }
}
