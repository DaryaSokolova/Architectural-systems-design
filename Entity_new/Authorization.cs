using System;

namespace Entity_new
{
    public class Authorization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public byte[] HashPassword { get; set; }
    }

}
