using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsBlazorApp.Server.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string ContactPhone { get; set; }
    }
}
