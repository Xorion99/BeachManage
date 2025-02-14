using System;
using System.Collections.Generic;

namespace BeachManage.Models;

public partial class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateDeleteFromApp { get; set; }

    public virtual ICollection<Match> MatchTeamAs { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchTeamBs { get; set; } = new List<Match>();

    public virtual ICollection<Teamplayer> Teamplayers { get; set; } = new List<Teamplayer>();
}
