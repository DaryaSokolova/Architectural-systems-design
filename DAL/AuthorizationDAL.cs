﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public void Registration(Entity_new.Authorization auth)
        {

            using (bakaContext data = new bakaContext())
            {
                data.Users.Add(new User()
                {
                    Name = auth.Name,
                    Password = GetStringHash(auth.Password)
                });
                data.SaveChanges();
            }
        }
    }
}
