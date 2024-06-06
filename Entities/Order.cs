using System;
using System.Collections.Generic;

namespace Entities;

public partial class Order
{
    public int Orderid { get; set; }

    public DateTime OrderDate { get; set; }

    public int  Price { get; set; }

    public int Userid { get; set; }

    public virtual User? User { get; set; } = null!;

    public virtual ICollection<OrederItem> OrederItems { get; set; } = new List<OrederItem>();

}