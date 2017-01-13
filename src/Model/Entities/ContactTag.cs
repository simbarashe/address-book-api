using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class ContactTag
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int TagId { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
