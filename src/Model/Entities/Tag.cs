using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Tag
    {
        public Tag()
        {
            ContactTags = new List<ContactTag>();
        }
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ContactTag> ContactTags { get; set; }
    }
}
