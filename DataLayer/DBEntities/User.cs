using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.DBEntities
{
    public partial class User
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
