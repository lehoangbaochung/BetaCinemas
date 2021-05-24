using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BetaCinemas.Models
{
    public partial class Member : IdentityUser
    {
        public Member()
        {
            Bills = new HashSet<Bill>();
            Contacts = new HashSet<Contact>();
            Tickets = new HashSet<Ticket>();
        }

        //public new int Id { get; set; }
        public string FullName { get; set; }
        public string Pass { get; set; }
        public DateTime Birthday { get; set; }
        public bool Gender { get; set; }
        public DateTime RegisterDate { get; set; }
        public string HomeAddress { get; set; }
        public string CardNumber { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
