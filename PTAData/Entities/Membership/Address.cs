using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTAData.Entities
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public Address()
        {
            //
            // TODO: Add constructor logic here
            //
            City = "Bel Air";
            State = "MD";
        }
    }
}