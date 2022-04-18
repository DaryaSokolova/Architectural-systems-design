using System;

namespace BL
{
    public class AuthorizationBL
    {
        public void Registration(Entity_new.Authorization auth)
        {
            new DAL.AuthorizationDAL().Registration(auth);

        }
    }
}
