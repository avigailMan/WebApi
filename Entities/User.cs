using System;
using System.Collections.Generic;

namespace Entities;

public partial class User
{
    public int Userid { get; set; }

    public string? Email { get; set; }

    public string FirstName { get; set; } 

    public string LastName { get; set; }

    public string Password { get; set; } 

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
