﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DAL
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Password { get; set; }
        public double Total { get; set; }
    }
}
