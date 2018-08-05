using System;
using System.Collections.Generic;
using System.Text;

namespace ContactManagerApi.Models
{
    public class ContactRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
