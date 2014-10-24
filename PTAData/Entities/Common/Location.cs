using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        [Display(Prompt = "Location Name")]
        public string Name { get; set; }
        public Address Address { get; set; }
    }
}
