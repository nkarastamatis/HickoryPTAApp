using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTAData.Entities
{
    public interface IAutoGenerateFields
    {
        DateTime LastModified { get; set; }
        DateTime CreatedOn { get; set; }
        string UserModified { get; set; }
    }
}
