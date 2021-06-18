using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain
{
    public  abstract class EntityWithId : BaseEntity
    {
        public int Id { get; set; }
    }
}
