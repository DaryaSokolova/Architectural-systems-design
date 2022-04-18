using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Password { get; set; }
        public int Total { get; set; }
    }
}
