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
        public string TeacherId { get; set; }
        public PersonName Name { get; set; }
        public Grade Grade { get; set; }

        public Teacher()
        {
            //
            // TODO: Add constructor logic here
            //
            TeacherId = Guid.NewGuid().ToString();
            Name = new PersonName();
            Grade = new Grade();
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