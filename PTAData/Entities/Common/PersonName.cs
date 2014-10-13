using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PTAData.Entities
{
    /// <summary>
    /// Summary description for PersonName
    /// </summary>
    /// 
    [Serializable]
    public class PersonName
    {
        [Display(Prompt = "First Name")]
        public string First { get; set; }
        [Display(Prompt = "Last Name")]
        public string Last { get; set; }

        public PersonName()
        {
            //
            // TODO: Add constructor logic here
            //
            First = "";
            Last = "";
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", First, Last);
        }
    }
}