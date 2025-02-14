using System;
using System.Collections.Generic;

namespace BeachManage.Models;

public partial class Tournament
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateDeleteFromApp { get; set; }

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
