using System;
using System.Collections.Generic;

namespace Entities;

public class OrederItem
{
    public int OrderItemid { get; set; }

    public int Productid { get; set; }

    public int Orderid { get; set; }

    public int Quantity { get; set; }

    public virtual Order? Order { get; set; } = null!;

    public virtual Product? Product { get; set; } = null!;
}
