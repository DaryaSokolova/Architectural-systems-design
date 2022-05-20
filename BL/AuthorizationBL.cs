using DAL;
using System;

namespace BL
{
    public class AuthorizationBL
    {
        public static void Registration(Entity_new.Authorization auth)
        {
            new DAL.AuthorizationDAL().AddOrUpdate(auth);

        }

        //public static bool Login(Entity_new.Authorization auth)
        //{
        //    AuthorizationDAL t = new AuthorizationDAL();

        //    if (t.Get(auth.Name) != null && t.Get(auth.Name).Password == auth.Password)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public static bool Login(string Name, string Password)
        {
            AuthorizationDAL t = new AuthorizationDAL();

            if (t.Get(Name) != null && t.Get(Name).Name == Name)
            {
                return t.Get(Name).Password == Password;
            }
            else
            {
                return false;
            }
        }
    }
}
