using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class ContactCommunicationDetail
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public int CommunicationTypeId { get; set; }
        public string Detail { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual CommunicationType CommunicationType { get; set; }
    }
}
