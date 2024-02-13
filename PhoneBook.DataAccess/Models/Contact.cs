using System.Collections;
using System.Collections.Generic;

namespace PhoneBook.DataAccess.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string? MiddleName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
    }
} 