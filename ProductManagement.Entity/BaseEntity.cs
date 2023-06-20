using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Entity
{
    public class BaseEntity
    {
        public DateTime CreationDate { set; get; }
        public DateTime UpdateDate { set; get; }
        public DateTime DeleteDate { set; get; }
    }
}
