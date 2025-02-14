using System;
using System.Collections.Generic;

namespace BeachManage.Models;

public partial class Playerstat
{
    public int Id { get; set; }

    public int MatchId { get; set; }

    public int PlayerId { get; set; }

    public int StatTypeId { get; set; }

    public int StatValue { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateDeleteFromApp { get; set; }

    public virtual Match Match { get; set; } = null!;

    public virtual Player Player { get; set; } = null!;

    public virtual Stattype StatType { get; set; } = null!;
}
