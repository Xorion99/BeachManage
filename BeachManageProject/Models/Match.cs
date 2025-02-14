using System;
using System.Collections.Generic;

namespace BeachManage.Models;

public partial class Match
{
    public int Id { get; set; }

    public int TournamentId { get; set; }

    public int TeamAid { get; set; }

    public int TeamBid { get; set; }

    public int ScoreA { get; set; }

    public int ScoreB { get; set; }

    public DateTime MatchDate { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateDeleteFromApp { get; set; }

    public virtual ICollection<Playerstat> Playerstats { get; set; } = new List<Playerstat>();

    public virtual Team TeamA { get; set; } = null!;

    public virtual Team TeamB { get; set; } = null!;

    public virtual Tournament Tournament { get; set; } = null!;
}
