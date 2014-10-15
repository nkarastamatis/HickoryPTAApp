using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTAData.Entities
{
    public class SitePost : IAutoGenerateFields
    {
        [Key]
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        [DataType(DataType.MultilineText)]
        public string PostBody { get; set; }

        public virtual ICollection<PostFile> Files { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserModified { get; set; }
    }
}
