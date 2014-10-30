using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PTAData.Entities
{
    public class ServerFile : IAutoGenerateFields
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PostedFile { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserModified { get; set; }
    }
}
