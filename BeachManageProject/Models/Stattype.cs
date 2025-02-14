using System;
using System.Collections.Generic;

namespace BeachManage.Models;

public partial class Stattype
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateDeleteFromApp { get; set; }

    public virtual ICollection<Playerstat> Playerstats { get; set; } = new List<Playerstat>();
}
