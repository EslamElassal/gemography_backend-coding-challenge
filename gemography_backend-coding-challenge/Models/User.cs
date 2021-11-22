using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gemography_backend_coding_challenge.Models
{
    public class User
    {
        public long ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }

    }
}
