using System;
using System.Collections.Generic;

namespace DBManager.Models
{
    public partial class User
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
