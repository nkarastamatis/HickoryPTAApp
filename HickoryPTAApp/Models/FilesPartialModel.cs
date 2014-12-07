using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HickoryPTAApp.Models
{
    public class FilesPartialModel
    {
        public string Title { get; set; }
        public IEnumerable<PTAData.Entities.CommitteeFile> Files { get; set; }
    }
}