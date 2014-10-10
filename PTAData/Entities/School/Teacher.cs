using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace PTAData.Entities
{
    /// <summary>
    /// Summary description for Teacher
    /// </summary>
    /// 
    [Serializable]
    public class Teacher
    {
        public int TeacherId { get; set; }
        public PersonName Name { get; set; }
        public Grade Grade { get; set; }

        public Teacher()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string NameString
        {
            get
            {
                return Name.ToString();
            }
        }
    }
}