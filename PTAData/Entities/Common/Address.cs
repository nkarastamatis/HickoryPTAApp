using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    /// <summary>
    /// Summary description for Address
    /// </summary>
    public class Address
    {
        [Display(Prompt = "Street Address")]
        public string StreetAddress { get; set; }
        [Display(Prompt = "City")]
        public string City { get; set; }
        [Display(Prompt = "State")]
        public string State { get; set; }
        [Display(Prompt = "Zip")]
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