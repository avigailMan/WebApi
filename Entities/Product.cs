﻿using System;
using System.Collections.Generic;

namespace Entities;

public partial class Product
{
    public int Productid { get; set; }

    public string ProductName { get; set; } = null!;

    public int? Price { get; set; }

    public int CategoryId { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrederItem> OrederItems { get; set; } = new List<OrederItem>();
}
