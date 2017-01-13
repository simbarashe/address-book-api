using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBookApi.Dtos
{
    public class ContactTagDto
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public string Description { get; set; }
    }
}
