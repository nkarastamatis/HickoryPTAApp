﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTAData.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
