using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Dtos
{
    public class ContactDto
    {
        public ContactDto()
        {
            Tags = new List<ContactTagDto>();
            CommunicationDetails = new List<ContactCommunicationDetailDto>();

        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public IEnumerable<ContactTagDto> Tags { get; set; }
        public IEnumerable<ContactCommunicationDetailDto> CommunicationDetails { get; set; }
    }
}
