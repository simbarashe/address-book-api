using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    public class Contact
    {
        public Contact()
        {
            ContactTags = new List<ContactTag>();
            ContactCommunicationDetails = new List<ContactCommunicationDetail>();

        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public virtual ICollection<ContactTag> ContactTags { get; set; }
        public virtual ICollection<ContactCommunicationDetail> ContactCommunicationDetails { get; set; }
    }
}
