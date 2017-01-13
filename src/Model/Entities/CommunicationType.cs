using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class CommunicationType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ContactCommunicationDetail> ContactCommunicationDetails { get; set; }
    }
}
