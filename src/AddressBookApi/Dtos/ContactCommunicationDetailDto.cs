using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Dtos
{
    public class ContactCommunicationDetailDto
    {
        public int Id { get; set; }
        public string Detail { get; set; }
        public string Type { get; set; }
    }
}
